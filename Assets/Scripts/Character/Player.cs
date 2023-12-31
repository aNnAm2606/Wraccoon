using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{

    public static Player Instance
    {
        get; private set;
    }

    public event EventHandler<OnSelectedJarChangedEventArgs> OnSelectedJarChanged;
    public class OnSelectedJarChangedEventArgs : EventArgs
    {
        public MashmallowJar selectedJar;
    }

    [SerializeField] public float speed = 7f;
    [SerializeField] public float jumpSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask objectsLayerMask;

    private bool isWalking;
    private bool isJumping;

    Vector3 movementVelocity;
    Vector3 turnVelocity;
    private Vector3 lastInteractDirection;
    private MashmallowJar selectedJar;

    public float rotationSpeed = 100f;
    private float gravityValue = -9.81f;

    CharacterController characterController;

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedJar != null)
        {
            selectedJar.Interact();
        }
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (Instance != null)
        {
            Debug.LogError("There is more than one player Instance");
        }
        Instance = this;
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

        float interactDistance = 3f;

        if(direction != Vector3.zero)
        {
            lastInteractDirection = direction;
        }

       if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance, objectsLayerMask))
        {
          if(raycastHit.transform.TryGetComponent(out Sofa sofa))
            {
                //has sofa
                //sofa.Interact();
            }
            if (raycastHit.transform.TryGetComponent(out MashmallowJar jar))
            {
                //has jar
                if (jar != selectedJar)
                {
                    SetSelectedJar(jar);
                }
            }
            else
            {
                SetSelectedJar(null);
            }
        }
        else
        {
            SetSelectedJar(null);
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

    private void SetSelectedJar(MashmallowJar selectedJar)
    {
        this.selectedJar = selectedJar;

        OnSelectedJarChanged?.Invoke(this, new OnSelectedJarChangedEventArgs
        {
            selectedJar = selectedJar
        });
    }
}
