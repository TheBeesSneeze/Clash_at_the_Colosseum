using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour
{

    [Header("Combat Variables")]
    [SerializeField] [Min(0)] public float BossHealth;
    [SerializeField] [Min(0)] public float BossDamage;

    [Header("Movement Variables")]
    [SerializeField] [Min(0)] private float bossAttackRate;
    [HideInInspector]
    public float BossAttackRate
    {
        get
        {
            if (slowedDownCountdown <= 0)
                return bossAttackRate;
            else return slowedRate;
        }
    }
    private float slowedDownCountdown;
    private float slowedRate;


    #region affectors

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
    public void SlowBoss(float slowedAmount, float slowedTime)
    {
        slowedRate = slowedAmount;
        slowedDownCountdown = slowedTime;
    }
    #endregion

}
