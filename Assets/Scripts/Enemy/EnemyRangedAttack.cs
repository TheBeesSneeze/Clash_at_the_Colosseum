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
        playerObject = GameObject.FindObjectOfType<PlayerBehaviour>().gameObject;
        fireRate = stats.EnemyAttackRate;
        print(fireRate);
        slowFireRate = fireRate * 2;
        coolDown = stats.EnemyAttackRate;
        
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
                coolDown = stats.EnemyAttackRate;
            }
        }
    }

    private float GetDistanceFromPlayer()
    {
        if (playerObject == null)
        {
            playerObject = FindObjectOfType<PlayerBehaviour>().gameObject;
        }
        var distance = (transform.position - playerObject.transform.position).normalized;
        distance.y = 0f;
        float distanceFrom = distance.magnitude;
        return distanceFrom;
    }
    
    private void Attacking()
    {
        Debug.Log("attack god damnit");
        if (playerObject == null)
        {
            playerObject = FindObjectOfType<PlayerBehaviour>().gameObject;
        }
        
        if (shootingMode == null)
        {
            return;
        }
        if(playerObject == null)
        {
            return;
        }
        nextFireTime += Time.deltaTime;
        Vector3 destination = playerObject.transform.position;
        destination += new Vector3(
            Random.Range(shootingMode.BulletAccuracyOffset, shootingMode.BulletAccuracyOffset),
            Random.Range(shootingMode.BulletAccuracyOffset, shootingMode.BulletAccuracyOffset),
            Random.Range(-shootingMode.BulletAccuracyOffset, shootingMode.BulletAccuracyOffset));
        Vector3 direction = destination - transform.position;
        GameObject bullet = BulletPoolManager.InstantiateEnemyBullet(transform.position);
        bullet.transform.forward = direction.normalized;
        Bullet bulletObject = bullet.GetComponent<Bullet>();
        bulletObject.damageAmount = shootingMode.BulletDamage;
        bulletObject.bulletForce = shootingMode.BulletSpeed;
        bulletObject.Initialize(bulletEffect1, bulletEffect2, direction);

        PublicEvents.OnEnemyShoot.Invoke();

    }

}
