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
}
