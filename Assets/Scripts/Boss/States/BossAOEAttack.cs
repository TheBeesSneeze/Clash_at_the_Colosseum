using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAOEAttack : StateMachineBehaviour
{
    public GameObject Fireball;
    public void LaunchFireball(Transform animator)
    {
        GameObject fireball = Instantiate(Fireball, animator.position, Quaternion.identity);
        BossFireBall fb = fireball.GetComponent<BossFireBall>();
        fb.Launch(animator.transform.position, BossController.playerBehaviour.GetGroundPosition());
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LaunchFireball(animator.transform);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("AOE", false);

        if (!animator.GetBool("HalfHealth"))
        {
            if (BossController.bossTakeDamage.currentHealth <= BossController.Stats.BossHealth / 2)
            {
                animator.SetBool("HalfHealth", true);
                return;
            }
        }

        if (BossController.bossTakeDamage.currentHealth <= 0)
        {
            animator.SetBool("BossDeath", true);
            return;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("AOE", false);
    }
}
