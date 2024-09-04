///
/// Initializes other manager type classes
/// Do not put any non-initalization methods here
/// make a new manager class or something
/// 
/// Authors: Toby
/// 

using PathFinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static CellManager cellManager;
    public static PathManager pathManager;
    public static BulletPoolManager bulletPoolManager;

    [Header("BulletPool")]
    [SerializeField] private int amountToPool;
    [SerializeField] private GameObject bullet;

    void Start()
    {
        InitializeCellManager();
        InitializePathManager();
        InitializeBulletPoolManager();
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
}
