/*******************************************************************************
 * File Name :         GameManager.cs
 * Author(s) :         Toby, Sky
 *
 * Brief Description : Initializes other manager type classes
 * Do not put any non-initalization methods here (cough tyler cough isPaused),
 * make a new manager class or something
 *****************************************************************************/

using PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GameManager : Singleton<GameManager>
{
    //Manager references
    public static CellManager cellManager;
    public static PathManager pathManager;
    public static BulletPoolManager bulletPoolManager;
    public static EnemySpawner enemyManager;
    public static StageManager stageManager;
    public static StageTransitionManager transitionManager;
    public static PublicEvents publicEvents;

    [Header("Stage Manager")]
    [SerializeField] private StageStats[] stages;

    [Header("Stage Transition")]
    [SerializeField] private float transitonSeconds = 1;

    [Header("Bullet Pooling")]
    [SerializeField] private int amountToPool;
    [SerializeField] private GameObject bullet;

    

    [Header("Move to different script")]
    [ReadOnly] public bool isPaused = false;
    [ReadOnly] public float cursorSensitivity;

    void Start()
    {
        cursorSensitivity = PlayerPrefs.GetFloat("sensitivity", 0);
        InitializeCellManager();
        InitializePathManager();
        InitializeBulletPoolManager();
        InitializeStageTransitionManager();
        InitializeStageManager();
        //InitializeEnemySpawnManager(); //not yet
        InitializePublicEvents();
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

    private void InitializeStageTransitionManager()
    {
        transitionManager = new StageTransitionManager(transitonSeconds);
    }

    private void InitializeStageManager()
    {
        stageManager = new StageManager(stages);
    }

    private void InitializePublicEvents()
    {
        publicEvents = new PublicEvents();
    }

    private void OnDisable()
    {
        bulletPoolManager.OnDisable();
    }
}
