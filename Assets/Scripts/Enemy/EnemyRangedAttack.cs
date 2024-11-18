/*******************************************************************************
* File Name :         EnemyRangedAttack
* Author(s) :         Clare Grady
* Creation Date :     9/2/2024
*
* Brief Description : 
* For RANGED Enemies ONLY
* Checks if player is in the attack range 
* If is shoot enemyBullet
 *****************************************************************************/
using BulletEffects;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using Random = UnityEngine.Random;
using Player;

namespace Enemy
{
public class EnemyRangedAttack : MonoBehaviour
{
    private EnemyStats stats;
    private static GameObject playerObject;
    private float coolDown;
    private float fireRate;
    private float slowFireRate;
    private float nextFireTime;
    private bool canMultiShoot;
    private int shotsFired;
    private EnemyAnimator animator;
    private EnemyTakeDamage damage;

    [SerializeField] private ShootingMode shootingMode;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private BulletEffect bulletEffect1;
    [SerializeField] private BulletEffect bulletEffect2;
    [SerializeField] private GameObject bulletPrefab;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        if (playerObject == null)
            playerObject = GameObject.FindObjectOfType<PlayerBehaviour>().gameObject;
        animator = GetComponent<EnemyAnimator>();
        damage = GetComponent<EnemyTakeDamage>();
        fireRate = stats.AttackRate;
        slowFireRate = fireRate * 2;
        coolDown = stats.AttackRate + Random.Range(0, 4); // random offset
        canMultiShoot = stats.canConsecutiveShoot;
        shotsFired = 0;

    }

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position + getDirection());
        AttemptAttack();
        coolDown -= Time.deltaTime;
    }

    private void AttemptAttack()
    {
        if (damage.IsDead)
            return;

        if (coolDown <= 0f)
        {
            float distanceFromPlayer = GetDistanceFromPlayer();

            if (distanceFromPlayer <= stats.EnemyAttackRange)
            {
                Attacking();
            }
        }
    }

    private float GetDistanceFromPlayer()
    {
        var distance = (transform.position - playerObject.transform.position).normalized;
        distance.y = 0f;
        float distanceFrom = distance.magnitude;
        return distanceFrom;
    }

    private void Attacking()
    {
        if (shootingMode == null)
        {
            return;
        }
        if (playerObject == null)
        {
            return;
        }

        if (canMultiShoot)
        {
            if (shotsFired < stats.numberOfConsecutiveShots)
            {
                Fire();
                ++shotsFired;
                coolDown = stats.timeBetweenContinousShots;
            }
            else
            {
                shotsFired = 0;
                coolDown = stats.TimeBetweenAttacks;
            }
        }
        else
        {
            Fire();
            coolDown = stats.TimeBetweenAttacks;
        }

    }

    private void Fire()
    {
        nextFireTime += Time.deltaTime;

        Vector3 direction = getDirection();

        //GameObject bullet = InstantiateBullet(stats.bulletType);

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.forward = direction.normalized;
        Bullet bulletObject = bullet.GetComponent<Bullet>();
        bulletObject.damageAmount = shootingMode.BulletDamage;
        bulletObject.bulletForce = shootingMode.BulletSpeed;
        bulletObject.OnBulletShoot(direction);

        if (animator != null) { animator.OnAttackStart(); }
    }

    private Vector3 getDirection()
    {
        Vector3 destination = playerObject.transform.position;
        destination += new Vector3(
            Random.Range(-shootingMode.BulletAccuracyOffset, shootingMode.BulletAccuracyOffset),
            Random.Range(-shootingMode.BulletAccuracyOffset, shootingMode.BulletAccuracyOffset),
            Random.Range(-shootingMode.BulletAccuracyOffset, shootingMode.BulletAccuracyOffset));
        Vector3 direction = destination - transform.position;
        return direction;
    }

    /*private GameObject InstantiateBullet(EnemyType type)
    {
        if(type == EnemyType.Cyclops)
        {
            PublicEvents.CyclopsAttack.Invoke();
            return BulletPoolManager.InstantiateCyclopsEnemyBullet(transform.position);
        }
        else if(type == EnemyType.Harpy)
        {
            PublicEvents.OnEnemyShoot.Invoke();
            return BulletPoolManager.InstantiateHarpyEnemyBullet(transform.position);
        }
        else
        {
            return BulletPoolManager.InstantiateBasicEnemyBullet(transform.position);
        }

    }*/

    private void OnDisable()
    {
        Destroy(this);
    }
}
}

