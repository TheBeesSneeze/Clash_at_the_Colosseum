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
using UnityEngine;
using Player;

namespace Enemy
{
public class EnemyMelee : MonoBehaviour
{

    private EnemyStats stats;
    private static PlayerBehaviour player;
    private GameObject playerObject;
    private float coolDown;
    private float timeBetweenAttacks;
    private EnemyAnimator animator;
    private EnemyTakeDamage damage;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        if (player == null)
            player = GameObject.FindObjectOfType<PlayerBehaviour>();
        playerObject = player.gameObject;
        timeBetweenAttacks = stats.AttackRate;
        animator = GetComponent<EnemyAnimator>();
        damage = GetComponent<EnemyTakeDamage>();
        coolDown = timeBetweenAttacks;
    }
    private void Update()
    {
        coolDown -= Time.deltaTime;
        AttemptAttack();
    }

    private void AttemptAttack()
    {
        if (damage.IsDead)
            return;

        if (coolDown <= 0f)
        {
            float distanceToPlayer = GetDistanceFromPlayer();

            if (distanceToPlayer <= stats.EnemyAttackRange)
            {
                player.TakeDamage(stats.EnemyDamage);

                PublicEvents.OnMeleeEnemyAttack.Invoke();

                coolDown = stats.TimeBetweenAttacks;
                if (animator != null)
                    animator.OnAttackStart();
            };
        }

    }

    private float GetDistanceFromPlayer()
    {
        //var distance = (transform.position - playerObject.transform.position);
        //float distanceFrom = distance.magnitude;
        float distanceFrom = Vector3.Distance(transform.position, player.transform.position);

        return distanceFrom;
    }
}
}

