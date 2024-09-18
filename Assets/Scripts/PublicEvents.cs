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

}
