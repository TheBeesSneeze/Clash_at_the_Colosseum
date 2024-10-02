/*******************************************************************************
* File Name :         StageStats
* Author(s) :         Clare Grady
* Creation Date :     9/14/2024
*
* Brief Description : 
* Scriptable Object to hold all the Stage Stats 
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StageStats", menuName = "StageStats")]
public class StageStats : ScriptableObject
{
    public TextAsset StageLayout;
    public int NumberOfEnemiesForLevel;
    //public GameObject[] EnemyPrefabs;
    public float timeTillEnemiesSpawn;
    public bool BulletEffectOnClear=true;
}
