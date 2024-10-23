using System;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerCharacterController controller;



    protected PlayerState(PlayerCharacterController controller) 
    {
        this.controller = controller;
    }

    public abstract void FixedUpdate();

    public abstract void Update();

    protected virtual void Move()
    {
        controller.Character.Move(controller.MoveDirection * controller.MoveDistance * Time.fixedDeltaTime);
    }

    /// <summary>
    /// rotating by keyboard input(character)
    /// </summary>
    protected virtual void Rotate()
    {
        // no inputs?
        if (Mathf.Approximately(controller.InputDirection.magnitude, 0f) == true)
        {
            return;
        }

        Vector3 forward3D = controller.transform.forward;
        Vector2 forward2D = new Vector2(forward3D.x, forward3D.z);
        Vector2 calibratedforward2D = Vector2.zero;

        // prevent rotating 180 degree problem
        if (Mathf.Approximately(Vector2.Dot(forward2D, controller.InputDirection), 0f))
        {
            calibratedforward2D = new Vector2(forward3D.x + 0.01f, forward3D.z);
        }
        else
        {
            calibratedforward2D = forward2D;
        }

        Vector2 lerpForwardAndInputDir = Vector2.Lerp(calibratedforward2D, controller.InputDirection, Time.fixedDeltaTime * 20f).normalized;

        float angle = Vector2.SignedAngle(forward2D, lerpForwardAndInputDir);

        controller.transform.Rotate(Vector3.down, angle);
    }

    /// <summary>
    /// rotating by mouse input(camera)
    /// </summary>
    /// <param name="lookAt">camera transform.forward</param>
    protected virtual void Rotate(Vector3 lookAt)
    {
        Vector3 forward3D = controller.transform.forward;
        Vector2 lookAt2D = new Vector2(lookAt.x, lookAt.z);
        Vector2 forward2D = new Vector2(forward3D.x, forward3D.z);

        Vector2 lerpForwardAndLookAt = Vector3.Lerp(forward2D, lookAt2D, Time.fixedDeltaTime * 40f).normalized;

        float angle = Vector2.SignedAngle(forward2D, lerpForwardAndLookAt);

        controller.transform.Rotate(Vector3.down, angle);
    }

    protected virtual void SetMovement() { }

    protected virtual void SetAnimParameters() 
    {
        controller.Animator.SetBool(PlayerAnimatorParameter.IsInCombat, controller.IsInCombat);
        controller.Animator.SetBool(PlayerAnimatorParameter.IsWeaponDrawn, controller.IsWeaponDrawn);
        controller.Animator.SetFloat(PlayerAnimatorParameter.MoveDirectionX, controller.MoveDirection.x);
        controller.Animator.SetFloat(PlayerAnimatorParameter.MoveDirectionY, controller.MoveDirection.z);
        controller.Animator.SetFloat(PlayerAnimatorParameter.MoveDistance, controller.MoveDistance);
        controller.Animator.SetFloat(PlayerAnimatorParameter.InputDirectionX, controller.InputDirection.x);
        controller.Animator.SetFloat(PlayerAnimatorParameter.InputDirectionY, controller.InputDirection.y);

        if (controller.OnWeaponDraw == true)
        {
            controller.Animator.SetTrigger(PlayerAnimatorParameter.OnWeaponDraw);
        }
    }
}
