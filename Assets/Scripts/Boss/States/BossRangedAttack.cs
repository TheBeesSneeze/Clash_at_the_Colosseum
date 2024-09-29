using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRangedAttack : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        animator.SetBool("Ranged", false);
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Ranged", false);

        if (BossController.bossTakeDamage.currentHealth <= BossController.Stats.BossHealth / 2)
        {
            animator.SetBool("HalfHealth", true);
            return;
        }
    }
}
