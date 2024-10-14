using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("i did it! i saved the town!");
        BossController.Invincible = true;
        //animation plays that shows the hydra is dead
        Destroy(animator.gameObject);
        //transition to win stuff laters
    }
}
