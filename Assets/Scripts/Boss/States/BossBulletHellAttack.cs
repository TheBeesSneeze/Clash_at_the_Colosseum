using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBulletHellAttack : StateMachineBehaviour
{
    public GameObject bulletHellTarget;
    public float bulletSpread;
    public float bulletHellSpeed;

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
        secondsSinceLastShot += Time.deltaTime;

        if (shotsFired >= numberOfBullets)
        {
            animator.SetBool("BulletHell", false);
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
        
    }

    private void TargetMove()
    {
        bulletHellTarget.transform.position = new Vector3(Mathf.Lerp(-bulletSpread, bulletSpread, bulletHellSpeed), bulletHellTarget.transform.position.y, bulletHellTarget.transform.position.z);
    }



    private void Attacking(Animator animator)
    {
        Vector3 destination = bulletHellTarget.transform.position;
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
        bulletObject.Initialize(direction);

        PublicEvents.OnEnemyShoot.Invoke();

    }
}
