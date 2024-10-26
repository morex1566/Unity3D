using UnityEngine;

public static class PlayerAnimatorParameter
{
    // boolean
    public static readonly int IsInCombat = Animator.StringToHash("IsInCombat");
    public static readonly int IsRunning = Animator.StringToHash("IsRunning");
    public static readonly int IsWeaponDrawn = Animator.StringToHash("IsWeaponDrawn");

    // constant
    public static readonly int MoveDirectionX = Animator.StringToHash("MoveDirectionX");
    public static readonly int MoveDirectionY = Animator.StringToHash("MoveDirectionY");
    public static readonly int MoveDistance = Animator.StringToHash("MoveDistance");
    public static readonly int InputDirectionX = Animator.StringToHash("InputDirectionX");
    public static readonly int InputDirectionY = Animator.StringToHash("InputDirectionY");

    // trigger
    public static readonly int OnWeaponDraw = Animator.StringToHash("OnWeaponDraw");
}
