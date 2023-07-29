using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] public float speed = 7f;

    private bool isWalking;
    private bool isJumping;

    Vector3 movementVelocity;
    Vector3 turnVelocity;

    
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
        isJumping = false;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        
        if (characterController.isGrounded) 
        {
            movementVelocity = transform.forward * speed * vertical;
            turnVelocity = transform.up * rotationSpeed * horizontal;

            if (Input.GetButtonDown("Jump"))
            {
                movementVelocity.y = jumpSpeed;
                isJumping = true;
            }
        }

        //Add gravity
        movementVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(movementVelocity*Time.deltaTime);
        transform.Rotate(turnVelocity * Time.deltaTime);

 
        isWalking = direction != Vector3.zero;
     

    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsJumping()
    {
        return isJumping;
    }
}
