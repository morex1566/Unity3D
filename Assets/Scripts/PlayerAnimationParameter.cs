using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAnimationParameter
{
    public static readonly int MoveInputX = Animator.StringToHash("MoveInputX");
    public static readonly int MoveInputY = Animator.StringToHash("MoveInputY");

    public static readonly int MoveDirectionX = Animator.StringToHash("MoveDirectionX");
    public static readonly int MoveDirectionY = Animator.StringToHash("MoveDirectionY");

    public static readonly int MoveRelativeDirectionX = Animator.StringToHash("MoveRelativeDirectionX");
    public static readonly int MoveRelativeDirectionY = Animator.StringToHash("MoveRelativeDirectionY");

    public static readonly int MoveVelocityX = Animator.StringToHash("MoveVelocityX");
    public static readonly int MoveVelocityY = Animator.StringToHash("MoveVelocityY");

    public static readonly int MoveRelativeVelocityX = Animator.StringToHash("MoveRelativeVelocityX");
    public static readonly int MoveRelativeVelocityY = Animator.StringToHash("MoveRelativeVelocityY");

    public static readonly int RotationAngle = Animator.StringToHash("RotationAngle");

    public static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");

    public static readonly int IsInCombat = Animator.StringToHash("IsInCombat");
}
