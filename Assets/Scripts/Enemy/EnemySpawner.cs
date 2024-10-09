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
    [SerializeField] private EnemyPrefab[] enemyPrefabs;
    public Dictionary<EnemySpawn, GameObject> _enemyPrefabs = new Dictionary<EnemySpawn, GameObject>();
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
        PublicEvents.OnStageTransitionFinish.AddListener(OnStageChangeFinish);
        SetDictionary();
    }

    private void Update()
    {
        if (GameManager.Instance.pausedForUI) return;

        //InputEvents.Instance.EnemySpawnStarted.AddListener(SpawnEnemies);
        timeTillEnemiesSpawn -= Time.deltaTime;

        if (timeTillEnemiesSpawn > 0)
            return;

        if(!hasSpawnedEnemies)
        {
            SpawnEnemies();
            return;
        }

        
    }

    private void SetDictionary()
    {
        foreach(EnemyPrefab enemy in enemyPrefabs)
        {
            _enemyPrefabs[enemy.enemyTag] = enemy.prefab;
        }
    }

    /// <summary>
    /// Called in StageTransitionManager
    /// </summary>
    public void OnStageChangeFinish()
    {
        timeTillEnemiesSpawn = StageManager.timeTillEnemiesSpawn;
        hasSpawnedEnemies = false;
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

        for(int i = 0; i < enemySpawnPoints.Length; i++)
        {
            Debug.Log("spawning " + enemySpawnPoints[i].enemyToSpawn);
            if (_enemyPrefabs.TryGetValue(enemySpawnPoints[i].enemyToSpawn, out GameObject enemyType))
            {
                GameObject.Instantiate(enemyType, enemySpawnPoints[i].transform.position, Quaternion.identity);
                _currentEnemiesAlive++;
            }
        }
        hasSpawnedEnemies=true;
    }

    //make this a unity event
    public static void OnEnemyDeath()
    {
        //Debug.Log("boss active"+BossController.bossActive);
        if(BossController.bossActive) return;

        _currentEnemiesAlive--;

        if (_currentEnemiesAlive <= 0)
        {
            //StageManager.ChangeStage();
            StageManager.OnStageEnd();
        }
    }

    private void OnDisable()
    {
        _currentEnemiesAlive = 0;
    }
}

[System.Serializable]
public class EnemyPrefab
{
    [Tooltip("Its not actually a tag")]
    public EnemySpawn enemyTag;
    public GameObject prefab;
}

