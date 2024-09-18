///
/// So many unity events used by like every script ever.
/// Initialized in GameManager
/// Toby
/// 



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PublicEvents
{
    public static UnityEvent OnPlayerShoot = new UnityEvent();
    public static UnityEvent OnSettingsSliderChanged = new UnityEvent();
    public static UnityEvent OnPlayerDamage = new UnityEvent();
    public static UnityEvent OnEnemyDamage = new UnityEvent();
    public static UnityEvent OnEnemyDeath = new UnityEvent();
    public static UnityEvent OnPlayerDeath = new UnityEvent();
    public static UnityEvent OnEnemyShoot = new UnityEvent();
    public static UnityEvent OnMeleeEnemyAttack = new UnityEvent();
    public static UnityEvent OnGrapple = new UnityEvent();
    public static UnityEvent OnDash = new UnityEvent();
    public static UnityEvent OnUpgradeReceived = new UnityEvent(); //@TODO
    public static UnityEvent OnStageTransition = new UnityEvent();

}
