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

public class EnemySpawner : Singleton<EnemySpawner>
{
    private bool hasSpawnedEnemies;
    private float timeTillEnemiesSpawn;
    private EnemySpawnPoint[] enemySpawnPoints;

    private static int _currentEnemiesAlive;

    public static int ReturnEnemyCount()
    {
        return _currentEnemiesAlive;
    }

    // Moved this to its own script
    /*
    [System.Serializable]
    public class EnemySpawnPointEntry
    {
        public Transform EnemySpawnPoint;
    }
    */

    private void Start()
    {
        hasSpawnedEnemies = false;
        timeTillEnemiesSpawn = StageManager.timeTillEnemiesSpawn;
        enemySpawnPoints = GameObject.FindObjectsOfType<EnemySpawnPoint>();
    }

    private void Update()
    {
        //InputEvents.Instance.EnemySpawnStarted.AddListener(SpawnEnemies);
        timeTillEnemiesSpawn -= Time.deltaTime;

        if (timeTillEnemiesSpawn > 0)
            return;

        if(!hasSpawnedEnemies)
        {
            SpawnEnemies();
            return;
        }

        if (_currentEnemiesAlive <= 0)
        {
            StageManager.ChangeStage();
            timeTillEnemiesSpawn = StageManager.timeTillEnemiesSpawn;
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

        if(enemySpawnPoints == null)
            return;

        for(int i = 0; i < enemySpawnPoints.Length && i< StageManager.enemiesToSpawn; i++)
        {
            GameObject enemyType = StageManager.enemyPool[Random.Range(0, StageManager.enemyPool.Length - 1)];
            GameObject.Instantiate(enemyType, enemySpawnPoints[i].transform.position, Quaternion.identity);
            _currentEnemiesAlive++;
        }
        hasSpawnedEnemies=true;
    }

    //make this a unity event
    public static void OnEnemyDeath()
    {
        _currentEnemiesAlive--;
    }
}

