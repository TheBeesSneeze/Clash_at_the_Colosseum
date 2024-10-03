using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase2Idle : StateMachineBehaviour
{
    private int nextState;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        nextState = Random.Range(1, 3);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (BossController.bossTakeDamage.currentHealth <= 0)
        {
            animator.SetBool("BossDeath", true);
            BossController.Invincible = true;
            return;
        }

        if (nextState == 1)
        {
            animator.SetBool("AOE", true);
        }
        else if (nextState == 2)
        {
            animator.SetBool("BulletHell", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
