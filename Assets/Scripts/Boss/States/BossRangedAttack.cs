using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRangedAttack : StateMachineBehaviour
{

    [SerializeField] private ShootingMode shootingMode;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int numberOfBullets;
    [SerializeField] private float intervalBetweenShots;
    private float secondsSinceLastShot;
    private float shotsFired;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        secondsSinceLastShot = 0;
        shotsFired = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (BossController.bossTakeDamage.currentHealth <= BossController.Stats.BossHealth / 2)
        {
            animator.SetBool("HalfHealth", true);
            return;
        }

        secondsSinceLastShot += Time.deltaTime / BossController.Stats.BossAttackRate;

        if (shotsFired >= numberOfBullets)
        {
            animator.SetBool("Ranged", false);
            return;
        }

        if (secondsSinceLastShot >= intervalBetweenShots)
        {
            Attacking(animator);
            shotsFired++;
            secondsSinceLastShot = 0;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Ranged", false);
    }

    private void Attacking(Animator animator)
    {
        Vector3 destination = BossController.Player.position;
        destination += new Vector3(
            Random.Range(shootingMode.BulletAccuracyOffset, shootingMode.BulletAccuracyOffset),
            Random.Range(shootingMode.BulletAccuracyOffset, shootingMode.BulletAccuracyOffset),
            Random.Range(-shootingMode.BulletAccuracyOffset, shootingMode.BulletAccuracyOffset));
        Vector3 direction = destination - animator.transform.position;
        GameObject bullet = Instantiate(bulletPrefab, animator.transform.position, Quaternion.identity);
        bullet.transform.forward = direction.normalized;
        Bullet bulletObject = bullet.GetComponent<Bullet>();
        bulletObject.damageAmount = shootingMode.BulletDamage;
        bulletObject.bulletForce = shootingMode.BulletSpeed;
        bulletObject.OnBulletShoot(direction);

        PublicEvents.OnEnemyShoot.Invoke();

    }

}
