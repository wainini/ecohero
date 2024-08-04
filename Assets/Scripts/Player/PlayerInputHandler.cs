using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public bool InteractInput { get; private set; }

    public void SetMovementInput(InputAction.CallbackContext ctx)
    {
        MovementInput = ctx.ReadValue<Vector2>().normalized;
    }

    public void SetInteractInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            InteractInput = true;
        }
        else if (ctx.canceled)
        {
            InteractInput = false;  
        }
    }

    public void InteractPerformed() => InteractInput = false;
}
