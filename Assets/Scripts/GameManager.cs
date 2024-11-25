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
using BulletEffects;

namespace Managers
{ 
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float DefaultSensitivity = 0.3f;

    [Header("Stage Manager")]
    [SerializeField] private StageStats[] stages;

    [Header("Stage Transition")]
    [SerializeField] private float transitonSeconds = 1;
    [SerializeField] private float transitonDelay = 1;

    [Header("Game Settings")]
    public BulletEffect[] BulletEffects;

    //Manager references
    public static CellManager cellManager;
    public static PathManager pathManager;
    public static BulletPoolManager bulletPoolManager;
    public static EnemySpawner enemyManager;
    public static StageManager stageManager;
    public static StageTransitionManager transitionManager;
    public static PublicEvents publicEvents;

    [Header("Cell Manager")]
    public float CellFallTime = 10;
    public float CellFallDistance = 25;


    [Header("Bullet Pooling")]
    [SerializeField] private int amountToPool;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject basicEnemyBullet;
    [SerializeField] private GameObject harpyEnemyBullet;
    [SerializeField] private GameObject cyclopsEnemyBullet;

    [Header("Move to different script")]
    [ReadOnly] public bool isPaused = false;
    [ReadOnly] public bool pausedForUI = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        InitializePublicEvents();
        InitializeCellManager();
        InitializePathManager();
        //InitializeBulletPoolManager();
        InitializeStageTransitionManager();
        InitializeStageManager();
        //InitializeEnemySpawnManager(); //not yet
        
    }
    private void InitializeCellManager()
    {
        cellManager = new CellManager(CellFallTime, CellFallDistance);
    }
    private void InitializePathManager()
    {
        pathManager = new PathManager();
    }
    private void InitializeBulletPoolManager()
    {
        bulletPoolManager = new BulletPoolManager(amountToPool, bullet, basicEnemyBullet, harpyEnemyBullet, cyclopsEnemyBullet);
    }

    private void InitializeEnemySpawnManager()
    {
        //enemyManager = new EnemySpawner();
    }

    private void InitializeStageTransitionManager()
    {
        transitionManager = new StageTransitionManager(transitonSeconds, transitonDelay);
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
        //bulletPoolManager.OnDisable();
    }

    private void Update()
    {
        pathManager.Update();
    }
}
}