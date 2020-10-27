using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    private float jumpForce;
    private float gravityScale;
    private CharacterController controller;
    private Vector3 moveDirection;

    //This creates the Animator
    //public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //anim = GetComponent<Animator>();
        jumpForce = moveSpeed * 0.5f;
        gravityScale = moveSpeed * 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (controller.isGrounded)
        {
            moveDirection.y = 0f;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

        controller.Move(moveDirection * Time.deltaTime);

        //Send info to Animator
        //anim.SetBool("isGrounded", controller.isGrounded);
        //anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Vertical"))));

    }
}
