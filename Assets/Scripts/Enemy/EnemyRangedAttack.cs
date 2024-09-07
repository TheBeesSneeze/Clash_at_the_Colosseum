/*******************************************************************************
* File Name :         EnemyRangedAttack
* Author(s) :         Clare Grady
* Creation Date :     9/2/2024
*
* Brief Description : 
* For RANGED Enemies ONLY
* Checks if player is in the attack range 
* If is shoot bullet
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyRangedAttack : MonoBehaviour
{
    private EnemyStats stats;
    private GunController gunController;
    private GameObject playerObject;
    private float coolDown; 
    private float fireRate;
    private float slowFireRate;
    private float nextFireTime;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        gunController = GetComponent<GunController>(); 
        playerObject = stats.playerObject;
        fireRate = stats.EnemyAttackRate;
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
            float distanceFromPlayer = GetDistanceFromPlayer();
            if (distanceFromPlayer <= stats.EnemyAttackRange)
            {
                Attacking();
                coolDown = fireRate;
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
        nextFireTime += Time.deltaTime;
        Vector3 destination = playerObject.transform.position;
        destination += new Vector3(
            Random.Range(-gunController.shootingMode.BulletAccuracyOffset, gunController.shootingMode.BulletAccuracyOffset),
            Random.Range(-gunController.shootingMode.BulletAccuracyOffset, gunController.shootingMode.BulletAccuracyOffset),
            Random.Range(-gunController.shootingMode.BulletAccuracyOffset, gunController.shootingMode.BulletAccuracyOffset));
        Vector3 direction = destination - gunController.bulletSpawnPoint.position;
        var bullet = Instantiate(gunController.BulletPrefab, gunController.bulletSpawnPoint.position, Quaternion.identity);
        bullet.transform.forward = direction.normalized;
        var bulletObject = bullet.GetComponent<Bullet>();
        bulletObject.damageAmount = gunController.shootingMode.BulletDamage;
        bulletObject.bulletForce = gunController.shootingMode.BulletSpeed;
        bulletObject.Initialize(gunController.bulletEffect1, gunController.bulletEffect2, direction);

    }

}
