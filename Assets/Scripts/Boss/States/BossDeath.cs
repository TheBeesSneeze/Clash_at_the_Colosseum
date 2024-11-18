using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Boss
{
    public class BossDeath : StateMachineBehaviour
    {
        public GameObject Roses;
        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            BossController.Invincible = true;

            Instantiate(Roses);
        }

    }
}