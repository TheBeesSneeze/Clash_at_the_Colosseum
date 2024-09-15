/*******************************************************************************
* File Name :         StageManager
* Author(s) :         Clare Grady
* Creation Date :     9/14/2024
*
* Brief Description : 
* Holds Array of the StageStats
* Keeps track of which stage we are on
* Once all enemies are killed transition next stage
* Wait amount of time then spawn in enemies
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager
{
    private static StageStats[] _stages;
    private static int stageIndex = 0;
    private static StageStats currentStage;

    public static GameObject[] enemyPool { get { return currentStage.EnemyPrefabs; } }
    public static int enemiesToSpawn { get { return currentStage.NumberOfEnemiesForLevel; } }
    public static float timeTillEnemiesSpawn { get { return currentStage.timeTillEnemiesSpawn; } }

    public StageManager(StageStats[] stages)
    {
        _stages = stages;
        currentStage = _stages[stageIndex];
    }

    /// <summary>
    /// Called in enemy spawner
    /// </summary>
    public static void ChangeStage()
    {
        Debug.Log("changing stage");
        if(stageIndex+1 == _stages.Length) 
        {
            Debug.LogWarning("I think the players supposed to beat the game here");
            return;
        }
        stageIndex++;

        currentStage = _stages[stageIndex];

        //add the dynamic stage code here to actually change stage 
    }
}
