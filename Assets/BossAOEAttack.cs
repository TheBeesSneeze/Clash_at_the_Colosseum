using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAOEAttack : StateMachineBehaviour
{
    private BossController controller;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*
        if (controller.Stats.BossHealth == controller.Stats.BossHealth / 2)
        {
            animator.SetBool("HalfHealth", true);
            return;
        }
        */

        animator.SetBool("AOE", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("AOE", false);
    }
}
