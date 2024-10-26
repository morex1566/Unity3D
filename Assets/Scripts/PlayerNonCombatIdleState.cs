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

        // 입력에 따른 방향 정하기
        Vector3 targetDirection = Vector3.zero;
        Vector3 targetMoveRelativeDirection = Vector3.zero;
        if (baseController.MoveInput.x > 0.5f)
        {
            targetDirection += new Vector3(baseController.CameraController.transform.right.x, 0, baseController.CameraController.transform.right.z);
            targetMoveRelativeDirection += Vector3.right;
        }
        else
        if (baseController.MoveInput.x < -0.5f)
        {
            targetDirection += new Vector3(baseController.CameraController.transform.right.x, 0, baseController.CameraController.transform.right.z) * -1;
            targetMoveRelativeDirection += Vector3.left;
        }

        if (baseController.MoveInput.y > 0.5f)
        {
            targetDirection += new Vector3(baseController.CameraController.transform.forward.x, 0, baseController.CameraController.transform.forward.z);
            targetMoveRelativeDirection += Vector3.forward;
        }
        else
        if (baseController.MoveInput.y < -0.5f)
        {
            targetDirection += new Vector3(baseController.CameraController.transform.forward.x, 0, baseController.CameraController.transform.forward.z) * -1;
            targetMoveRelativeDirection += Vector3.back;
        }
        targetDirection = targetDirection.normalized;
        targetMoveRelativeDirection = targetMoveRelativeDirection.normalized;

        baseController.MoveDirection = Vector3.Lerp(baseController.MoveDirection, targetDirection, Time.deltaTime * 10f);
        baseController.MoveRelativeDirection = Vector3.Lerp(baseController.MoveRelativeDirection, targetMoveRelativeDirection, Time.deltaTime * 10f);

        baseController.State = CreateState<PlayerNonCombatWalkingState>(baseController);
    }
}
