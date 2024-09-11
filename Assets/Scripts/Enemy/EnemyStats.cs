/*******************************************************************************
* File Name :         EnemyStats
* Author(s) :         Clare Grady
* Creation Date :     8/28/2024
*
* Brief Description : 
* ALL of the Enemy Stats NO IMPLENTATION 
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Combat Variables")]
    [SerializeField][Min(0)] public float EnemyHealth;
    [SerializeField][Min(0)] public float EnemyDamage;
    [SerializeField] public float EnemyAttackRate;
    [SerializeField][Min(0)] public float EnemyAttackRange;
    [SerializeField] public GameObject playerObject;

    [Header("Movement Variables")]
    [SerializeField][Min(0)] public float EnemyMovementSpeed;
    [SerializeField][Min(0)] public float TurningSpeed;
    [SerializeField][Min(0)] public float StopSpeed;
    [SerializeField][Min(0)] public float StopDistanceToPlayer;
    [SerializeField][Min(0)] public float SightDistance; 

    [Header("Only Effect Flyers")]
    [SerializeField][Min(0)] public float VerticalSpeed;
    [SerializeField][Min(0)] public float HeightAboveGround;

    /// <summary>
    /// slows the enemy and starts the countdown timer
    /// </summary>
    /// <param name="slowedAmount"></param>
    /// <param name="slowedTime"></param>
    public void SlowEnemy(float slowedAmount, float slowedTime)
    {
        EnemyMovementSpeed -= slowedAmount;
        StartCoroutine(SlowTimer(slowedAmount, slowedTime));
    }

    /// <summary>
    /// counts down and resets enemy speed
    /// probs not the best way to do this
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator SlowTimer(float amount, float time)
    {
        yield return new WaitForSeconds(time);
        EnemyMovementSpeed += amount;
    }
}
