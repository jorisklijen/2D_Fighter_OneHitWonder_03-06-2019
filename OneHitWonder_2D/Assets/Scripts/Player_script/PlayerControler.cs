using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [Header("inputs")]
    public KeyCode havyAtt;
    public KeyCode lightAtt;
    public KeyCode right;
    public KeyCode left;
    [Header("Speed")]
    
    public float moveSpeed;
    float dirX;
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
        //----------[ in de "" de naam van de anim ]----------
        if (dirX != 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Smal_punch"))
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        //----------[ Dit stuk werkt 100% ]----------
        //----------[ in de "" de naam van de anim ]----------
        if (Input.GetKeyDown(lightAtt) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Special_4(ThunderKick)")) 
        {
            anim.SetBool("isWalking", false);
            //----------[ de naam van de trigger ]----------
            anim.SetTrigger("Special_4(ThunderKick)"); 
        }
    }
    
}
