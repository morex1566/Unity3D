using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerNonCombatIdleState : PlayerIdleState
{
    public PlayerNonCombatIdleState(PlayerBaseController baseController) : base(baseController) { }

    protected override void OnTarget(InputAction.CallbackContext context)
    {
        baseController.IsInCombat = true;
        baseController.State = CreateState<PlayerCombatIdleState>(baseController);
    }

    protected override void OnMove(InputAction.CallbackContext context)
    {
        baseController.MoveInput = context.ReadValue<Vector2>();

        Vector3 targetDirection = Vector3.zero;
        if (baseController.MoveInput.x > 0.5f)
        {
            targetDirection += new Vector3(baseController.CameraController.transform.right.x, 0, baseController.CameraController.transform.right.z);
        }
        else
        if (baseController.MoveInput.x < -0.5f)
        {
            targetDirection += new Vector3(baseController.CameraController.transform.right.x, 0, baseController.CameraController.transform.right.z) * -1;
        }

        if (baseController.MoveInput.y > 0.5f)
        {
            targetDirection += new Vector3(baseController.CameraController.transform.forward.x, 0, baseController.CameraController.transform.forward.z);
        }
        else
        if (baseController.MoveInput.y < -0.5f)
        {
            targetDirection += new Vector3(baseController.CameraController.transform.forward.x, 0, baseController.CameraController.transform.forward.z) * -1;
        }

        baseController.MoveDirection = targetDirection.normalized;

        baseController.State = CreateState<PlayerNonCombatWalkingState>(baseController);
    }
}
