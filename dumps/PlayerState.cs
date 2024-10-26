using System;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerBaseController baseController;



    protected PlayerState(PlayerBaseController baseController) 
    {
        this.baseController = baseController;
    }

    public abstract void FixedUpdate();

    public abstract void Update();

    protected virtual void Move()
    {
        baseController.CharacterController.Move(baseController.MoveDirection.normalized * baseController.MoveDistance * Time.fixedDeltaTime);
    }

    protected virtual void Rotate()
    {
        baseController.transform.Rotate(Vector3.down, baseController.Rotation, Space.World);
    }

    protected virtual void SetRotation(Vector2 inputDirection)
    {
        // 입력 저장
        baseController.InputDirection = inputDirection;

        // 입력 방향으로 보간할 벡터 생성
        Vector3 lerpTo = Vector3.zero;
        Vector3 inputDirection3D = new Vector3(inputDirection.x, 0f, inputDirection.y);

        if (inputDirection3D.x > 0.5f)
        {
            lerpTo += new Vector3(baseController.CameraController.transform.right.x, 0, baseController.CameraController.transform.right.z);
        }
        else
        if (inputDirection3D.x < -0.5f)
        {
            lerpTo += new Vector3(baseController.CameraController.transform.right.x, 0, baseController.CameraController.transform.right.z) * -1;
        }

        if (inputDirection3D.z > 0.5f)
        {
            lerpTo += new Vector3(baseController.CameraController.transform.forward.x, 0, baseController.CameraController.transform.forward.z);
        }
        else
        if (inputDirection3D.z < -0.5f)
        {
            lerpTo += new Vector3(baseController.CameraController.transform.forward.x, 0, baseController.CameraController.transform.forward.z) * -1;
        }

        lerpTo = lerpTo.normalized;

        Vector3 rotDir = Vector3.Lerp(baseController.transform.forward, lerpTo, Time.deltaTime * 55);

        baseController.Rotation = Vector3.SignedAngle(baseController.transform.forward, rotDir, Vector3.down);
    }

    protected virtual void SetMovement(Vector2 inputDirection, float lerpWeight, float speed)
    {
        // 입력 저장
        baseController.InputDirection = inputDirection;

        // 입력 방향으로 보간할 벡터 생성
        Vector3 lerpTo = Vector3.zero;
        Vector3 inputDirection3D = new Vector3(inputDirection.x, 0f, inputDirection.y);

        if (inputDirection3D.x > 0.5f)
        {
            lerpTo += new Vector3(baseController.CameraController.transform.right.x, 0, baseController.CameraController.transform.right.z);
        }
        else
        if (inputDirection3D.x < -0.5f)
        {
            lerpTo += new Vector3(baseController.CameraController.transform.right.x, 0, baseController.CameraController.transform.right.z) * -1;
        }

        if (inputDirection3D.z > 0.5f)
        {
            lerpTo += new Vector3(baseController.CameraController.transform.forward.x, 0, baseController.CameraController.transform.forward.z);
        }
        else
        if (inputDirection3D.z < -0.5f)
        {
            lerpTo += new Vector3(baseController.CameraController.transform.forward.x, 0, baseController.CameraController.transform.forward.z) * -1;
        }

        lerpTo = lerpTo.normalized;

        // 이동할 방향 저장
        baseController.MoveDirection = Vector3.Lerp(baseController.MoveDirection, lerpTo, Time.deltaTime * lerpWeight);

        // 입력 방향으로 이동할 거리 보간 생성
        baseController.MoveDistance = Mathf.Abs(Mathf.Lerp(baseController.MoveDistance, speed, Time.deltaTime * lerpWeight));
    }

    protected virtual void SetAnimParameters() 
    {
        baseController.Animator.SetBool(PlayerAnimatorParameter.IsInCombat, baseController.IsInCombat);
        baseController.Animator.SetBool(PlayerAnimatorParameter.IsWeaponDrawn, baseController.IsWeaponDrawn);
        baseController.Animator.SetFloat(PlayerAnimatorParameter.MoveDirectionX, baseController.MoveDirection.x);
        baseController.Animator.SetFloat(PlayerAnimatorParameter.MoveDirectionY, baseController.MoveDirection.z);
        baseController.Animator.SetFloat(PlayerAnimatorParameter.MoveDistance, baseController.MoveDistance);
        baseController.Animator.SetFloat(PlayerAnimatorParameter.InputDirectionX, baseController.InputDirection.x);
        baseController.Animator.SetFloat(PlayerAnimatorParameter.InputDirectionY, baseController.InputDirection.y);

        if (baseController.OnWeaponDraw == true)
        {
            baseController.Animator.SetTrigger(PlayerAnimatorParameter.OnWeaponDraw);
        }
    }
}
