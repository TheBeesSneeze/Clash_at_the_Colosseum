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
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(EnemyTakeDamage))]
public class EnemyStats : MonoBehaviour
{
    [Header("Combat Variables")]
    [SerializeField][Min(0)] public float EnemyHealth;
    [SerializeField][Min(0)] public float EnemyDamage;
    [Tooltip("time between enemy attacks")]
    [SerializeField] public float AttackRate;
    [SerializeField][Min(0)] public float EnemyAttackRange;
    [SerializeField] public EnemyType bulletType = EnemyType.Melee;
    [SerializeField][Min(0)] public float healCharge; 

    [Header("Continous Shots Variables")]
    [SerializeField] public bool canConsecutiveShoot = false;
    [SerializeField][Min(0)] public int numberOfConsecutiveShots;
    [SerializeField][Min(0)] public float timeBetweenContinousShots;
    //[SerializeField] public GameObject playerObject;

    [Header("Movement Variables")]
    [SerializeField][Min(0)] private float _enemyMovementSpeed;
    [SerializeField] public float gravity;
    [HideInInspector] public float MoveSpeed { 
        get {
            if (slowedDownCountdown <= 0)
                return _enemyMovementSpeed;
            else return slowedSpeed;
        }
    }

    [HideInInspector] public float TimeBetweenAttacks
    {
        get
        {
            if (slowedDownCountdown <= 0)
                return AttackRate;
            else return AttackRate * slowedAttackMultiplier;
        }
    }
    private float slowedDownCountdown;
    private float slowedSpeed;
    private float slowedAttackMultiplier;
    [SerializeField][Min(0)] public float TurningSpeed;
    [SerializeField][Min(0)] public float JumpForce;
    [SerializeField][Min(0)] public float StopSpeed;
    [SerializeField][Min(0)] public float StopDistanceToPlayer;
    [SerializeField][Min(0)] public float SightDistance;
    [Tooltip("If enemy can pathfind down")]
    [SerializeField] public bool CanPathfindDown=true; 

    [Header("Only Effect Flyers")]
    [SerializeField][Min(0)] public float VerticalSpeed;
    [SerializeField][Min(0)] public float HeightAboveGround;

    private void Start()
    {
        PublicEvents.OnEnemySpawned.Invoke(this);
    }

    #region affectors

    public void OnEnemyDeath()
    {
        if (GetComponent<EnemyAnimator>() == null)
        {
            GetComponent<EnemyTakeDamage>().Die();
            return;
        }
        //VerticalSpeed = 0;
        _enemyMovementSpeed = 0;
        slowedSpeed = 0;
        numberOfConsecutiveShots = 0;
        EnemyDamage = 0;
        HeightAboveGround = 0;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity=false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gravity = 0;
            
    }    

    //updating countdown timer
    private void Update()
    {
        slowedDownCountdown -= Time.deltaTime;
    }

    /// <summary>
    /// slows the enemy and starts the countdown timer
    /// </summary>
    /// <param name="slowedAmount"></param>
    /// <param name="slowedTime"></param>
    public void SlowEnemy(float slowedAmount, float slowedTime, float slowedRate)
    {
        slowedSpeed = slowedAmount;
        slowedDownCountdown = slowedTime;
        slowedAttackMultiplier = slowedRate;
    }
    #endregion
   
}

public enum EnemyType
{
    Melee,
    Harpy,
    Cyclops,
    Boss
}