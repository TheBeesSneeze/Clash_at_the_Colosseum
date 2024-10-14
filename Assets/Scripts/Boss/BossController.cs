///
/// Sky
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Singleton<BossController>
{

    public float thwapDistance;
    [HideInInspector] public static BossStats Stats;
    [HideInInspector] public static Transform Player;
    [HideInInspector] public static Rigidbody PlayerRB;
    [HideInInspector] public static PlayerBehaviour playerBehaviour;
    [HideInInspector] public static BossTakeDamage bossTakeDamage;
    [HideInInspector] public static bool Invincible = true;
    [HideInInspector] public static EnemySpawner enemySpawner;
    [HideInInspector] public static bool bossActive= false;
    [HideInInspector] public static Transform Boss;
    [HideInInspector] public static Animator animator;

    private void Start()
    {
        Stats = GetComponent<BossStats>();
        Player = GameObject.FindObjectOfType<PlayerController>().transform;
        PlayerRB = Player.GetComponent<Rigidbody>();
        Boss = GameObject.FindObjectOfType<BossController>().transform;
        animator = Boss.GetComponent<Animator>();
        bossTakeDamage = GetComponent<BossTakeDamage>();
        playerBehaviour = GameObject.FindObjectOfType<PlayerBehaviour>();
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        PublicEvents.OnPlayerDeath.AddListener(OnPlayerDeath);
    }

    private void Update()
    {
        if (Vector3.Distance(Player.position, Boss.position) <= thwapDistance)
        {
            animator.SetBool("TailThwap", true);
        }
    }

    private void OnPlayerDeath()
    {
        Invincible = false;

        bossTakeDamage.currentHealth = Stats.BossHealth;
        bossActive = false;
    }

    private void OnEnable()
    {

        bossActive = true;
    }

    private void OnDisable()
    {
        bossActive = false;
    }

}
