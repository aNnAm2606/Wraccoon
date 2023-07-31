using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector3 GetMovementVectorNormalized()
    {
        float jump = 0;
        Vector2 moveInputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        jump = playerInputActions.Player.Jump.ReadValue<float>();
        
        Vector3 inputVector = new Vector3(moveInputVector.x, jump, moveInputVector.y);

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
