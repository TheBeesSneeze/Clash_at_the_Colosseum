///
/// Toby
/// 

using PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTransitonManager
{
    private Cell[] allCells;
    private EnemySpawnPoint[] allSpawnPoints;
    public StageTransitonManager()
    {
        allCells = GameObject.FindObjectsOfType<Cell>();
        allSpawnPoints = GameObject.FindObjectsOfType<EnemySpawnPoint>();

    }
}
