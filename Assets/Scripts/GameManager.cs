/*******************************************************************************
 * File Name :         GameManager.cs
 * Author(s) :         Toby, Sky
 *
 * Brief Description : Initializes other manager type classes
 * Do not put any non-initalization methods here  
 * make a new manager class or something
 * 
 *****************************************************************************/

using PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //Manager references
    public static CellManager cellManager;
    public static PathManager pathManager;
    public static BulletPoolManager bulletPoolManager;
    public static EnemySpawner enemyManager;
    public static StageManager stageManager;

    [Header("Stage Manager")]
    [SerializeField] private StageStats[] stages;

    [Header("Bullet Pooling")]
    [SerializeField] private int amountToPool;
    [SerializeField] private GameObject bullet;

    [Header("Move to different script")]
    public bool isPaused = false;

    void Start()
    {
        InitializeCellManager();
        InitializePathManager();
        InitializeBulletPoolManager();
        InitializeStageManager();
        //InitializeEnemySpawnManager(); //not yet
    }
    private void InitializeCellManager()
    {
        cellManager = new CellManager();
    }
    private void InitializePathManager()
    {
        pathManager = new PathManager();
    }
    private void InitializeBulletPoolManager()
    {
        bulletPoolManager = new BulletPoolManager(amountToPool, bullet);
    }

    private void InitializeEnemySpawnManager()
    {
        //enemyManager = new EnemySpawner();
    }

    private void InitializeStageManager()
    {
        stageManager = new StageManager(stages);
    }
}
