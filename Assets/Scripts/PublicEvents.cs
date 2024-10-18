///
/// So many unity events used by like every script ever.
/// Initialized in GameManager
/// Toby
/// 



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PublicEvents
{
    public static UnityEvent OnPlayerShoot = new UnityEvent();
    public static UnityEvent OnPlayerReload = new UnityEvent();
    public static UnityEvent OnPlayerHeal = new UnityEvent();
    public static UnityEvent OnSensitivitySliderChanged = new UnityEvent();
    public static UnityEvent OnPlayerDamage = new UnityEvent();
    public static UnityEvent OnEnemyDamage = new UnityEvent();
    public static UnityEvent OnAnyEnemyDeath = new UnityEvent();
    public static UnityEvent OnPlayerDeath = new UnityEvent();
    public static UnityEvent OnEnemyShoot = new UnityEvent();
    public static UnityEvent OnMeleeEnemyAttack = new UnityEvent();
    public static UnityEvent OnGrapple = new UnityEvent();
    public static UnityEvent OnDash = new UnityEvent();
    public static UnityEvent OnStageTransition = new UnityEvent();
    public static UnityEvent OnStageTransitionFinish = new UnityEvent();
    public static UnityEvent OnBossSpawn = new UnityEvent();
    public static UnityEvent HydraFireAttack = new UnityEvent();
    public static UnityEvent OnBossPhaseThreeStart = new UnityEvent();
    public static UnityEvent HydraDeath = new UnityEvent();
    public static UnityEvent CyclopsAttack = new UnityEvent();
    public static UnityEvent CyclopsDeath = new UnityEvent();
    public static UnityEvent HarpyDeath = new UnityEvent();
    public static UnityEvent MinoutarDeath = new UnityEvent();
    public static UnityEvent Reloading = new UnityEvent();
 

    // actions???? what the fuck!!!
    public static Action<BulletEffect> OnUpgradeReceived;
}
