using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAnimationParameter
{
    public static readonly int IsInCombat = Animator.StringToHash("IsInCombat");
    public static readonly int MoveInputX = Animator.StringToHash("MoveInputX");
    public static readonly int MoveInputY = Animator.StringToHash("MoveInputY");
    public static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
    public static readonly int MoveDirectionX = Animator.StringToHash("MoveDirectionX");
    public static readonly int MoveDirectionY = Animator.StringToHash("MoveDirectionY");
    public static readonly int RotationAngle = Animator.StringToHash("RotationAngle");
}
