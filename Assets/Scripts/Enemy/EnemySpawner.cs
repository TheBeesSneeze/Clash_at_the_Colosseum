/*******************************************************************************
* File Name :         EnemySpawner
* Author(s) :         Clare Grady
* Creation Date :     9/9/2024
*
* Brief Description : 
* OMG yall its the system that spawns the enemeies  :O 
* Note: code in comments is old button push spawn code
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private StageManager stageManager;

    private bool hasSpawnedEnemies;
    private float timeTillEnemiesSpawn;
    private EnemySpawnPointEntry[] enemySpawnPoints;
    private StageStats stageStats;

    [HideInInspector] public bool changeStage;

    [System.Serializable]
    public class EnemySpawnPointEntry
    {
        public GameObject EnemyType;
        public Transform EnemySpawnPoint;
    }

    private void Start()
    {
        hasSpawnedEnemies = false;
        stageManager = GetComponent<StageManager>();
        stageStats = stageManager.currentStage;
        timeTillEnemiesSpawn = stageStats.timeTillEnemiesSpawn;
        enemySpawnPoints = stageStats.spawnPoints;
    }

    private void Update()
    {
        //InputEvents.Instance.EnemySpawnStarted.AddListener(SpawnEnemies);
        timeTillEnemiesSpawn -= Time.deltaTime;
        
        if(!hasSpawnedEnemies && timeTillEnemiesSpawn <= 0)
        {
            SpawnEnemies();
        }

        if (changeStage)
        {
            stageStats = stageManager.currentStage;
            timeTillEnemiesSpawn = stageStats.timeTillEnemiesSpawn;
            enemySpawnPoints = stageStats.spawnPoints;
            hasSpawnedEnemies = false;
        }
    }

    public void SpawnEnemies()
    {
        /*if (!InputEvents.EnemySpawnPressed)
            return;
        for(int i = 0; i < enemySpawnPoints.Length; ++i)
        {
            Instantiate(enemySpawnPoints[i].EnemyType, enemySpawnPoints[i].EnemySpawnPoint);
        }
        InputEvents.EnemySpawnPressed = false;*/
        
        for(int i = 0; i < enemySpawnPoints.Length; i++)
        {
            Instantiate(enemySpawnPoints[i].EnemyType, enemySpawnPoints[i].EnemySpawnPoint);
        }
        hasSpawnedEnemies=true;
    }
}
