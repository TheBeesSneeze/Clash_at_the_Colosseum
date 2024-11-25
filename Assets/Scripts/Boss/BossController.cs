///
/// Sky
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Player;

namespace Enemy.Boss
{
    public class BossController : Singleton<BossController>
    {
        [HideInInspector] public static BossStats Stats;
        [HideInInspector] public static Transform Player;
        [HideInInspector] public static Rigidbody PlayerRB;
        [HideInInspector] public static PlayerBehaviour playerBehaviour;
        [HideInInspector] public static BossTakeDamage bossTakeDamage;
        [HideInInspector] public static bool Invincible = true;
        [HideInInspector] public static EnemySpawner enemySpawner;
        [HideInInspector] public static bool bossActive = false;
        [HideInInspector] public static Transform Boss;
        [HideInInspector] public static Animator animator;
        [HideInInspector] public static SpriteRenderer BossSR;

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
            bossActive = true;
            BossSR = Boss.GetComponentInChildren<SpriteRenderer>();
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

}


