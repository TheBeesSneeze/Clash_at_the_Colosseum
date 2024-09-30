using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargeAttack : StateMachineBehaviour
{
    [SerializeField] private float launchForce;
    [SerializeField] private float launchForceHeight;
    [SerializeField] private GameObject box;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject boxInstatiated = Instantiate(box, animator.gameObject.transform.position, Quaternion.identity);
        Rigidbody boxRB = boxInstatiated.GetComponent<Rigidbody>();
        Vector3 direction = (BossController.Player.transform.position - animator.transform.position);
        direction.y = 0;
        direction = direction.normalized;
        direction *= launchForce;
        direction.y = launchForceHeight;
        boxRB.AddForce(direction);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Charge", false);
        if (BossController.bossTakeDamage.currentHealth <= BossController.Stats.BossHealth / 2)
        {
            animator.SetBool("HalfHealth", true);
            return;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Charge", false);
    }
}
