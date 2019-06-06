using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerControler2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal-P1") * runSpeed;
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }
}
