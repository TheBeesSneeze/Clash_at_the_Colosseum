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
    [System.Serializable]
    private class EnemySpawnPointEntry
    {
        public GameObject EnemyType;
        public Transform EnemySpawnPoint;
    }

    [SerializeField] EnemySpawnPointEntry[] enemySpawnPoints;

    private void Update()
    {
        InputEvents.Instance.EnemySpawnStarted.AddListener(SpawnEnemies);
    }

    private void SpawnEnemies()
    {
        for(int i = 0; i < enemySpawnPoints.Length; ++i)
        {
            Instantiate(enemySpawnPoints[i].EnemyType, enemySpawnPoints[i].EnemySpawnPoint);
        }
    }
}
