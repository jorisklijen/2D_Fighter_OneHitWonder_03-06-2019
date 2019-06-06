using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerControler2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;

    public Animator anim;

    private void Start()
    {
       // anim = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        //Debug.Log(Input.GetAxisRaw("Horizontal"));

        if (horizontalMove != 0 && 
            (!anim.GetCurrentAnimatorStateInfo(0).IsName("Punch")        || (!anim.GetCurrentAnimatorStateInfo(0).IsName("Kick")) 
            || (!anim.GetCurrentAnimatorStateInfo(0).IsName("Baret"))    || (!anim.GetCurrentAnimatorStateInfo(0).IsName("HeatWave")) 
            || (!anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))    || (!anim.GetCurrentAnimatorStateInfo(0).IsName("SelestialPunch")) 
            || (!anim.GetCurrentAnimatorStateInfo(0).IsName("HavyPunch"))|| (!anim.GetCurrentAnimatorStateInfo(0).IsName("GetHit")) 
            ))
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }
}
