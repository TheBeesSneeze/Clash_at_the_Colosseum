using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("i did it! i saved the town!");
        Destroy(animator.gameObject);
        //transition to win stuff laters
    }
}
