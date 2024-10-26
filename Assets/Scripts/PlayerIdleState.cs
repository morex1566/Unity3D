using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerIdleState : PlayerState
{
    protected InputAction targetAction;

    protected InputAction movingAction;



    protected PlayerIdleState(PlayerBaseController baseController) : base(baseController)
    {
        targetAction = InputManager.InputMappingContext.Player.Target;
        targetAction.performed += OnTarget;

        movingAction = InputManager.InputMappingContext.Player.Move;
        movingAction.performed += OnMove;
    }

    public override void Update()
    {
        baseController.MoveSpeed = Mathf.Lerp(baseController.MoveSpeed, 0f, Time.deltaTime * 15f);
        baseController.MoveVelocity = baseController.MoveDirection * baseController.MoveSpeed;
        baseController.MoveRelativeVelocity = baseController.MoveRelativeDirection * baseController.MoveSpeed;
    }

    public override void Dispose()
    {
        targetAction.performed -= OnTarget;
        movingAction.performed -= OnMove;
    }

    protected virtual void OnTarget(InputAction.CallbackContext context) { }

    protected virtual void OnMove(InputAction.CallbackContext context) { }
}
