using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleBehaviour : StateMachineBehaviour
{
    private int nextState;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        nextState = Random.Range(1, 4);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (BossController.bossTakeDamage.currentHealth <= BossController.Stats.BossHealth / 2)
        {
            animator.SetBool("HalfHealth", true);
            return;
        }

        if (nextState == 1)
        {
            animator.SetBool("AOE", true);
        }
        else if (nextState == 2)
        {
            animator.SetBool("Ranged", true);
        }
        else if (nextState == 3)
        {
            animator.SetBool("Charge", true);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
