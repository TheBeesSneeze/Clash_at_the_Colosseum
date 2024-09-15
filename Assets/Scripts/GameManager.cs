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

    [Header("Bullet Pooling")]
    [SerializeField] private int amountToPool;
    [SerializeField] private GameObject bullet;

    public bool isPaused = false;
    public float cursorSensitivity;
    void Start()
    {
        cursorSensitivity = PlayerPrefs.GetFloat("sensitivity", 0);
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
