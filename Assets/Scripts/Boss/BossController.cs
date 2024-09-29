using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Singleton<BossController>
{
    [HideInInspector] public BossStats Stats;
    [HideInInspector] public Transform Player;
}
