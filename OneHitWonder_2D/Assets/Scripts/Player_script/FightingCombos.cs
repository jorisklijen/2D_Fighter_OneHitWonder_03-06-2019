using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum AttackType { heavy = 0, light = 1, special = 2, block = 3 };
public class FightingCombos : MonoBehaviour
{
    [Header("InPuts")]
    public KeyCode heavyKey;
    public KeyCode lightKey;
    public KeyCode spacialKey;
    public KeyCode blockKey;

    [Header("InPuts")]
    public KeyCode heavyControler;
    public KeyCode lightControler;
    public KeyCode spacialControler;
    public KeyCode blockControler;


    [Header("Attacks")]
    public Attack heavyAttack;
    public Attack lightAttack;
    public Attack spacialAttack;
    public Attack blockAttack;
    public List<Combo> combos;
    public float comboLeeway = 0.2f;

    [Header("Componetns")]
    public Animator ani;

    Attack curAttack = null;
    ComboInput lastInput = null;
    List<int> currentCombos = new List<int>();

    float timer = 0;
    float leeway = 0;
    bool skip = false;

    void Start()
    {
        ani = GetComponent<Animator>();
        PrimeCombos();
    }

    void PrimeCombos()
    {
        for (int i = 0; i < combos.Count; i++)
        {
            Combo c = combos[i];
            c.onInputted.AddListener(() =>
            {
                //Call attak  fucktion with the combo's attak 
                skip = true;
                Attack(c.comboAttack);
                ResetCombos();
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (curAttack != null)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                curAttack = null;
            }
            return;
        }

        if (currentCombos.Count > 0)
        {
            leeway += Time.deltaTime;
            if (leeway >= comboLeeway)
            {
                if (lastInput != null)
                {
                    Attack(getAttackFromType(lastInput.type));
                    lastInput = null;
                }
                ResetCombos();
            }
        }
        else
        {
            leeway = 0;
        }

        ComboInput input = null;

        if (Input.GetKeyDown(heavyKey) || Input.GetKeyDown(heavyControler))
        {
            input = new ComboInput(AttackType.heavy);
        }
        if (Input.GetKeyDown(lightKey) || Input.GetKeyDown(lightControler))
        {
            input = new ComboInput(AttackType.light);
        }
        if (Input.GetKeyDown(spacialKey)|| Input.GetKeyDown(spacialControler))
        {
            input = new ComboInput(AttackType.special);
        }
        if (Input.GetKeyDown(blockKey)|| Input.GetKeyDown(blockControler))
        {
            input = new ComboInput(AttackType.block);
        }
        //hier staan de inputs
        /* ----Mocht er ooit een nieuwe key bij koemn voeg dan toe ----
         * 
         *   if (Input.GetKeyDown(NONE))
         *   {
         *      input = new ComboInput(AttackType.NONE);
         *   }
         */

        if (input == null)
        {
            return;
        }
        lastInput = input;

        List<int> remove = new List<int>();
        for (int i = 0; i < currentCombos.Count; i++)
        {
            Combo c = combos[currentCombos[i]];
            if (c.continueCombo(input))
            {
                //Doe some thing 
                leeway = 0;
            }
            else
            {
                remove.Add(i);
            }
        }

        if (skip)
        {
            skip = false;
            return;
        }

        for (int i = 0; i < combos.Count; i++)
        {
            if (currentCombos.Contains(i)) continue;
            if (combos[i].continueCombo(input))
            {
                currentCombos.Add(i);
                leeway = 0;
            }
        }

        foreach (int i in remove)
        {
            currentCombos.RemoveAt(i);
        }

        if (currentCombos.Count <= 0)
        {
            Attack(getAttackFromType(input.type));
        }
    }

    void ResetCombos()
    {
        leeway = 0;
        for (int i = 0; i < currentCombos.Count; i++)
        {
            Combo c = combos[currentCombos[i]];
            c.ResetCombo();
        }

        currentCombos.Clear();
    }

    void Attack(Attack att)
    {
        curAttack = att;
        timer = att.length;
        ani.Play(att.name, -1, 0);
    }

    Attack getAttackFromType(AttackType t)
    {
        if (t == AttackType.heavy)
        {
            return heavyAttack;
        }
        if (t == AttackType.light)
        {
            return lightAttack;
        }
        if (t == AttackType.special)
        {
            return spacialAttack;
        }
        if (t == AttackType.block)
        {
            return blockAttack;
        }

        else
        {
            return null;
        }

    }
}



[System.Serializable]
public class Attack
{
    public string name;
    public float length;
}

[System.Serializable]
public class ComboInput
{
    public AttackType type;
    //movement input voor meer presiese combos

    public ComboInput(AttackType t)
    {
        type = t;
    }

    public bool isSameAs(ComboInput test)
    {
        return (type == test.type);// add && movement == inputs[curInput].movement
    }
}

[System.Serializable]
public class Combo
{
    public string name;
    public List<ComboInput> inputs;
    public Attack comboAttack;
    public UnityEvent onInputted;
    int curInput = 0;

    public bool continueCombo(ComboInput i)
    {
        if (i.type == inputs[curInput].type)
        {
            curInput++;
            if (curInput >= inputs.Count) // Finished the inputs and the attack shoud play
            {
                onInputted.Invoke();
                curInput = 0;
            }
            return true;
        }
        else
        {
            curInput = 0;
            return false;
        }
    }

    public ComboInput currentComboInput()
    {
        if (curInput >= inputs.Count)
        {
            return null;
        }
        return inputs[curInput];
    }

    public void ResetCombo()
    {
        curInput = 0;
    }
}
