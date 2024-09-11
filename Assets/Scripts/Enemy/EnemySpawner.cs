/*******************************************************************************
* File Name :         EnemySpawner
* Author(s) :         Clare Grady
* Creation Date :     9/9/2024
*
* Brief Description : 
* OMG yall its the system that spawns the enemeies  :O 
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float timeTillEnemiesSpawn;
    [Tooltip("Make array size the amount of spawn points you want")]
    [SerializeField] EnemySpawnPointEntry[] enemySpawnPoints;

    private bool hasSpawnedEnemies;

    [System.Serializable]
    private class EnemySpawnPointEntry
    {
        public GameObject EnemyType;
        public Transform EnemySpawnPoint;
    }

    private void Start()
    {
        hasSpawnedEnemies = false;
    }

    private void Update()
    {
        //InputEvents.Instance.EnemySpawnStarted.AddListener(SpawnEnemies);
        timeTillEnemiesSpawn -= Time.deltaTime;
        
        if(!hasSpawnedEnemies && timeTillEnemiesSpawn <= 0)
        {
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        /*if (!InputEvents.EnemySpawnPressed)
            return;
        for(int i = 0; i < enemySpawnPoints.Length; ++i)
        {
            Instantiate(enemySpawnPoints[i].EnemyType, enemySpawnPoints[i].EnemySpawnPoint);
        }
        InputEvents.EnemySpawnPressed = false;
        */
        for(int i = 0; i < enemySpawnPoints.Length; i++)
        {
            Instantiate(enemySpawnPoints[i].EnemyType, enemySpawnPoints[i].EnemySpawnPoint);
        }
        hasSpawnedEnemies=true;
    }
}
