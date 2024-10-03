using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBulletHellAttack : StateMachineBehaviour
{
    public int amountOfBoxes;
    [SerializeField] private float launchForce;
    [SerializeField] private float launchForceHeight;
    [SerializeField] private GameObject phase2Box;
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*
        for (int i = 0; i < amountOfBoxes; i++)
        {
            float t = ((float)i) / (((float)amountOfBoxes) + 1f);
            Vector3 position = Vector3.Lerp(target1.position, target2.position, t);
            Vector3 direction = animator.transform.position - position;
            Debug.Log("ghsgugusgus " + t);
            Debug.Log("HEREEEEEE" + direction);

            GameObject boxInstatiated = Instantiate(phase2Box, animator.gameObject.transform.position, Quaternion.identity);
            Rigidbody boxRB = boxInstatiated.GetComponent<Rigidbody>();
            direction.y = 0;
            direction = direction.normalized;
            direction *= launchForce;
            direction.y = launchForceHeight;
            boxRB.AddForce(direction, ForceMode.Impulse);
        }

        I HATE MATH GRRRRRRRRRRRRRR
        */


        GameObject boxInstatiated = Instantiate(phase2Box, animator.gameObject.transform.position, Quaternion.identity);
        Rigidbody boxRB = boxInstatiated.GetComponent<Rigidbody>();
        Vector3 direction = (BossController.Player.transform.position - animator.transform.position);
        direction.y = 0;
        direction = direction.normalized;
        direction *= launchForce;
        direction.y = launchForceHeight;
        boxRB.AddForce(direction, ForceMode.Impulse);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.SetBool("BulletHell", false);

        if (BossController.bossTakeDamage.currentHealth <= 0)
        {
            animator.SetBool("BossDeath", true);
            BossController.Invincible = true;
            return;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.SetBool("BulletHell", false);
    }
}
