using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [Header("inputs")]
    public KeyCode havy;
    public KeyCode licht;
    public KeyCode richt;
    public KeyCode left;

    float dirX; 
    public float moveSpeed;

    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;

        transform.position = new Vector2(transform.position.x + dirX, transform.position.y);

        if (dirX != 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Smal_punch")) //in de "" de naam van de anim
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        
        //Dit stuk werkt 100%
        if (Input.GetKeyDown(licht) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Smal_punch")) //in de "" de naam van de anim
        {
            anim.SetBool("isWalking", false);
            anim.SetTrigger("smalPunch"); // de naam van de trigger 
        }
    }
    
}
