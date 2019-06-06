using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerControler2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;

    public Animator anim;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        //Debug.Log(Input.GetAxisRaw("Horizontal"));

        if (horizontalMove != 0)
            
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
        controller.Move(horizontalMove * Time.fixedDeltaTime);
    }
}
