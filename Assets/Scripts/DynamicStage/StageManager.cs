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

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageStats[] stages;
    private EnemySpawner enemySpawner;
    private int stageIndex = 0;
    

    [HideInInspector] public int currentEnemies;
    [HideInInspector] public StageStats currentStage;

    private void Start()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        currentEnemies = stages[stageIndex].NumberOfEnemiesForLevel;
        currentStage = stages[stageIndex];
    }

    private void Update()
    {
        CheckIfCanChangeStage();
    }

    private void CheckIfCanChangeStage()
    {
        if (currentEnemies <= 0)
        {
            ChangeStage();
        }
    }

    private void ChangeStage()
    {
        if(stageIndex < stages.Length-1) 
        {
            ++stageIndex;
            currentStage = stages[stageIndex];
        }

        enemySpawner.changeStage = true;
        currentEnemies = stages[stageIndex].NumberOfEnemiesForLevel;

        //add the dynamic stage code here to actually change stage 
    }
}
