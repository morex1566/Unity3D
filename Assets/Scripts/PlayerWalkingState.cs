using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerWalkingState : PlayerState
{
    protected InputAction targetAction;

    protected InputAction movingAction;



    protected PlayerWalkingState(PlayerBaseController baseController) : base(baseController) 
    {
        targetAction = InputManager.InputMappingContext.Player.Target;
        targetAction.performed += OnTarget;

        movingAction = InputManager.InputMappingContext.Player.Move;
        movingAction.performed += OnMovePerformed;
        movingAction.performed += OnRotatePerformed;
        movingAction.canceled += OnMoveCanceled;
        movingAction.canceled += OnRotateCanceled;
    }

    public override void Dispose()
    {
        targetAction.performed -= OnTarget;
        movingAction.performed -= OnMovePerformed;
        movingAction.performed -= OnRotatePerformed;
        movingAction.canceled -= OnMoveCanceled;
        movingAction.canceled -= OnRotateCanceled;
    }

    protected virtual void OnTarget(InputAction.CallbackContext context) { }

    protected virtual void OnMovePerformed(InputAction.CallbackContext context) { }

    protected virtual void OnMoveCanceled(InputAction.CallbackContext context) { }

    protected virtual void OnRotatePerformed(InputAction.CallbackContext context) { }

    protected virtual void OnRotateCanceled(InputAction.CallbackContext context) { }

}
