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
}
