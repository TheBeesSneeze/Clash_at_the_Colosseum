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
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyRangedAttack : MonoBehaviour
{
    private EnemyStats stats;
    private GameObject playerObject;
    private float coolDown; 
    private float fireRate;
    private float slowFireRate;
    private float nextFireTime;
    

    [SerializeField] private ShootingMode shootingMode;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField]private BulletEffect bulletEffect1;
    [SerializeField] private BulletEffect bulletEffect2;
    

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        playerObject = FindObjectOfType<PlayerBehaviour>().gameObject;
        fireRate = stats.EnemyAttackRate;
        print(fireRate);
        slowFireRate = fireRate * 2;
        coolDown = 0f;
        
    }

    private void Update()
    {
        AttemptAttack();
        coolDown -= Time.deltaTime;
        
    }

    private void AttemptAttack()
    {
        if (coolDown <= 0f)
        {
            print("attemptt attack with cool down");
            float distanceFromPlayer = GetDistanceFromPlayer();
            print(distanceFromPlayer);
            if (distanceFromPlayer <= stats.EnemyAttackRange)
            {
                Attacking();
                coolDown = stats.EnemyAttackRate;
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
        nextFireTime += Time.deltaTime;
        Vector3 destination = playerObject.transform.position;
        destination += new Vector3(
            Random.Range(shootingMode.BulletAccuracyOffset, shootingMode.BulletAccuracyOffset),
            Random.Range(shootingMode.BulletAccuracyOffset, shootingMode.BulletAccuracyOffset),
            Random.Range(-shootingMode.BulletAccuracyOffset, shootingMode.BulletAccuracyOffset));
        Vector3 direction = destination - bulletSpawnPoint.position;
        var bullet = BulletPoolManager.InstantiateEnemyBullet(bulletSpawnPoint.position);
        bullet.transform.forward = direction.normalized;
        var bulletObject = bullet.GetComponent<Bullet>();
        bulletObject.damageAmount = shootingMode.BulletDamage;
        bulletObject.bulletForce = shootingMode.BulletSpeed;
        bulletObject.Initialize(bulletEffect1, bulletEffect2, direction);

        PublicEvents.OnEnemyShoot.Invoke();

    }

}
