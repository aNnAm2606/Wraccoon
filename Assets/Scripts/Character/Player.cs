using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] public float speed = 7f;
    [SerializeField] public float jumpSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;
    private bool isJumping;

    Vector3 movementVelocity;
    Vector3 turnVelocity;

    
    public float rotationSpeed = 100f;
    private float gravityValue = -9.81f;

    CharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        isJumping = false;

        Vector3 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 direction = new Vector3(inputVector.x, inputVector.y, inputVector.z).normalized;
        
        if (characterController.isGrounded) 
        {
            movementVelocity = transform.forward * speed * inputVector.z;
            turnVelocity = transform.up * rotationSpeed * inputVector.x;

            if (inputVector.y != 0)
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
