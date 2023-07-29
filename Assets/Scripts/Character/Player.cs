using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{

    Vector3 movementVelocity;
    Vector3 turnVelocity;

    public float speed = 6f;
    public float rotationSpeed = 90f;
    private float jumpSpeed = 10f;
    private float gravityValue = -9.81f;

    CharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        if (characterController.isGrounded) 
        {
            movementVelocity = transform.forward * speed * vertical;
            turnVelocity = transform.up * rotationSpeed * horizontal;

            if(Input.GetButtonDown("Jump"))
            {
                movementVelocity.y = jumpSpeed;
            }
        }

        //Add gravity
        movementVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(movementVelocity*Time.deltaTime);
        transform.Rotate(turnVelocity * Time.deltaTime);
    }

    //private CharacterController controller;
    //private Vector3 playerVelocity;
    //private bool groundedPlayer;
    //private float playerSpeed = 2.0f;
    //private float jumpHeight = 1.0f;
    //private float gravityValue = -9.81f;

    //private void Start()
    //{
    //    controller = gameObject.AddComponent<CharacterController>();
    //    controller.center = new Vector3(0, 1, 0);
    //}

    //void Update()
    //{
    //    groundedPlayer = controller.isGrounded;
    //    if (groundedPlayer && playerVelocity.y < 0)
    //    {
    //        playerVelocity.y = 0f;
    //    }

    //    Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    //    controller.Move(move * Time.deltaTime * playerSpeed);

    //    if (move != Vector3.zero)
    //    {
    //        //gameObject.transform.forward = move;
    //        gameObject.transform.localPosition = move;
    //    }

    //    // Changes the height position of the player..
    //    if (Input.GetButtonDown("Jump") && groundedPlayer)
    //    {
    //        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    //    }

    //    playerVelocity.y += gravityValue * Time.deltaTime;
    //    controller.Move(playerVelocity * Time.deltaTime);
    //}
}
