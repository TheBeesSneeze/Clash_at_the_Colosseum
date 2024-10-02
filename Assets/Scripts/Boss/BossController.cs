using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Singleton<BossController>
{
    [HideInInspector] public static BossStats Stats;
    [HideInInspector] public static Transform Player;
    [HideInInspector] public static PlayerBehaviour playerBehaviour;
    [HideInInspector] public static BossTakeDamage bossTakeDamage;
    [HideInInspector] public static bool Invincible = false;
    [HideInInspector] public static EnemySpawner enemySpawner;

    private void Start()
    {
        Stats = GetComponent<BossStats>();
        Player = GameObject.FindObjectOfType<PlayerController>().transform;
        bossTakeDamage = GetComponent<BossTakeDamage>();
        playerBehaviour = GameObject.FindObjectOfType<PlayerBehaviour>();
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        PublicEvents.OnPlayerDeath.AddListener(OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        Invincible = false;

        bossTakeDamage.currentHealth = Stats.BossHealth;
    }
}
