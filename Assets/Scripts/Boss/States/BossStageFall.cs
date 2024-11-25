using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Boss
{
    public class BossStageFall : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PublicEvents.OnBossPhaseThreeStart.Invoke();
        }
    }
}
