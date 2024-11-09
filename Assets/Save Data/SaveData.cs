///
/// Toby
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "Player Save Data")]

public class SaveData : ScriptableObject
{
    public static ShootingMode SelectedGun;

    public static int CurrentStageIndex=0;
    public static List<BulletEffect> gotBulletEffects = new List<BulletEffect>();
    public static List<BulletEffect> bulletEffectPool = new List<BulletEffect>();

    public static void ResetData()
    {
        // SelectedGun = _gunTypes[currentGunIndex];
        bulletEffectPool = new List<BulletEffect>();
        gotBulletEffects = new List<BulletEffect>();
        CurrentStageIndex = 0;
    }
}
