using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] public float speed = 7f;
    [SerializeField] public float jumpSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask objectsLayerMask;

    private bool isWalking;
    private bool isJumping;

    Vector3 movementVelocity;
    Vector3 turnVelocity;
    private Vector3 lastInteractDirection; 

    public float rotationSpeed = 100f;
    private float gravityValue = -9.81f;

    CharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsJumping()
    {
        return isJumping;
    }

    private void HandleInteractions()
    {
        Vector3 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 direction = new Vector3(inputVector.x, inputVector.y, inputVector.z).normalized;

        float interactDistance = 2f;

        if(direction != Vector3.zero)
        {
            lastInteractDirection = direction;
        }

       if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance, objectsLayerMask))
        {
          if(raycastHit.transform.TryGetComponent(out Sofa sofa))
            {
                //has sofa
                sofa.Interact();
            }
        }
    }

    private void HandleMovement()
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
        characterController.Move(movementVelocity * Time.deltaTime);
        transform.Rotate(turnVelocity * Time.deltaTime);

        isWalking = direction != Vector3.zero;

    }
}
