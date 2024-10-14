using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTailThwap : StateMachineBehaviour
{
    public float PlayerKnockBackForce = 10f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BossController.PlayerRB.AddForce(-BossController.Boss.transform.forward * PlayerKnockBackForce, ForceMode.Impulse); 
        animator.SetBool("TailThwap", false); 
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("TailThwap", false);
    }
}