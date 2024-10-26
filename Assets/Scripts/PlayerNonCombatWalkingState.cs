using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerNonCombatWalkingState : PlayerWalkingState
{
    public PlayerNonCombatWalkingState(PlayerBaseController baseController) : base(baseController) { }

    public override void Update()
    {
        baseController.MoveSpeed = Mathf.Lerp(baseController.MoveSpeed, baseController.DataInstance.NonCombatWalkingSpeed, Time.deltaTime * 5f);

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

        baseController.MoveVelocity = baseController.MoveDirection * baseController.MoveSpeed;
        baseController.MoveRelativeVelocity = baseController.MoveRelativeDirection * baseController.MoveSpeed;

        // 입력에 따른 회전 정하기
        Vector3 lerped = Vector3.Slerp(baseController.transform.forward, baseController.MoveDirection, Time.deltaTime * 30f);
        baseController.RotationAngle = Vector3.SignedAngle(baseController.transform.forward, lerped, Vector3.down);
    }

    protected override void OnTarget(InputAction.CallbackContext context)
    {
        baseController.IsInCombat = true;
        baseController.State = CreateState<PlayerCombatWalkingState>(baseController);
    }

    protected override void OnMovePerformed(InputAction.CallbackContext context)
    {
        baseController.MoveInput = context.ReadValue<Vector2>();
    }

    protected override void OnMoveCanceled(InputAction.CallbackContext context)
    {
        baseController.MoveInput = context.ReadValue<Vector2>();
        baseController.State = CreateState<PlayerNonCombatIdleState>(baseController);
        baseController.RotationAngle = 0f;
    }

    protected override void OnRotatePerformed(InputAction.CallbackContext context)
    {

    }

    protected override void OnRotateCanceled(InputAction.CallbackContext context)
    {

    }
}
