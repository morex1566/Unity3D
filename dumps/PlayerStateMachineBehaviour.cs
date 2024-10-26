using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachineBehaviour : StateMachineBehaviour
{
    protected PlayerBaseController controller;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.gameObject.GetComponentInParent<PlayerBaseController>();
    }
}
