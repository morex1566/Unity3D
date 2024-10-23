using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponDrawToSheathStateBehaviour : PlayerStateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (controller.InputDirection.magnitude > 0f)
        {
            animator.CrossFadeInFixedTime(stateInfo.fullPathHash, 0f);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.CrossFadeInFixedTime(stateInfo.fullPathHash, 0.1f);
    }
}
