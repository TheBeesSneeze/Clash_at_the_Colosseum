/*******************************************************************************
* File Name :         EnemyMelee
* Author(s) :         Clare Grady
* Creation Date :     9/2/2024
*
* Brief Description : 
* For MELEE enemies only 
* Calculates distance between self and player 
* If player in range attack
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMelee : MonoBehaviour
{
    
    private EnemyStats stats;
    private PlayerBehaviour player;
    private GameObject playerObject;
    private float coolDown;
    private float timeBetweenAttacks;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        player = playerObject.GetComponent<PlayerBehaviour>();
        timeBetweenAttacks = stats.EnemyAttackCooldownSpeed;
        playerObject = stats.playerObject; 
        coolDown = 0f;
    }
    private void Update()
    {
        coolDown -= Time.deltaTime;
        AttemptAttack();
    }

    private void AttemptAttack()
    {
        if (coolDown <= 0f)
        {
            float distanceToPlayer = GetDistanceFromPlayer();
            if (distanceToPlayer <= stats.EnemyAttackRange)
            {
                player.TakeDamage(stats.EnemyDamage);
                coolDown = timeBetweenAttacks; 
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

   
}
