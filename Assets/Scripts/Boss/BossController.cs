using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Singleton<BossController>
{
    [HideInInspector] public static BossStats Stats;
    [HideInInspector] public static Transform Player;
    [HideInInspector] public static BossTakeDamage bossTakeDamage;

    private void Start()
    {
        Stats = GetComponent<BossStats>();
        Player = GameObject.FindObjectOfType<PlayerController>().transform;
        bossTakeDamage = GetComponent<BossTakeDamage>();
    }
}
