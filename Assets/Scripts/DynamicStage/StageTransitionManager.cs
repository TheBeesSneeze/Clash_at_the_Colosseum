///
/// Initialized in GameManager
/// Toby
/// 

using PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;
using System.Threading.Tasks;


public class StageTransitionManager
{
    private static Cell[] allCells;
    private static EnemySpawnPoint[] allSpawnPoints;
    private static float _stageTransitonTime;
    private static float _stageTransitionDelay;
    public StageTransitionManager(float stageTransitionTime, float stageTransitionDelay)
    {
        _stageTransitonTime = stageTransitionTime;
        _stageTransitionDelay = stageTransitionDelay;

        allCells = GameObject.FindObjectsOfType<Cell>();
        allSpawnPoints = GameObject.FindObjectsOfType<EnemySpawnPoint>();

    }

    public async static void TransitionStage(TextAsset start, TextAsset end)
    {
        await Task.Delay((int)(_stageTransitionDelay * 1000));

        float startTime = Time.time;
        float t = 0;
        while (t < 1)
        {
            t = (Time.time - startTime) / _stageTransitonTime;
            t = Mathf.Min(1, t);
            
            TransitionStagePercent(start, end, t);

            await Task.Yield();
        }
        Debug.Log("invoking");
        PublicEvents.OnStageTransitionFinish.Invoke();
    }

    public static void TransitionStagePercent(TextAsset start, TextAsset end, float transitionPercent)
    {
        if (start == null || end == null)
            return;

        StageElements startLayout = GetStageElements(start);
        StageElements endLayout = GetStageElements(end);

        Cell[] activeCells = GameObject.FindObjectsOfType<Cell>();
        EnemySpawnPoint[] activeSpawnPoints = GameObject.FindObjectsOfType<EnemySpawnPoint>();

        //Assert.AreEqual(stageElements.SceneName, SceneManager.GetActiveScene().name); // Yeah. we went there. deal with it.
        if (startLayout.SceneName != endLayout.SceneName)
            Debug.LogWarning("Stages were built in seperate unity scenes");
        Assert.AreEqual(startLayout.elements.Length, endLayout.elements.Length);
        //Assert.AreEqual(startLayout.spawnPoints.Length, endLayout.spawnPoints.Length);

        StageElement[] startCellData = startLayout.elements;
        StageElement[] endCellData = endLayout.elements;
        for (int i = 0; i < activeCells.Length; i++)
        {
            Transform cell = activeCells[i].transform;
            cell.position = Vector3.Lerp(startCellData[i].p, endCellData[i].p, transitionPercent);
            cell.localScale = Vector3.Lerp(startCellData[i].ls, endCellData[i].ls, transitionPercent);
            cell.rotation = Quaternion.Lerp(startCellData[i].lr, endCellData[i].lr, transitionPercent);
        }

        SpawnPointElement[] startSpawnPoints = startLayout.spawnPoints;
        SpawnPointElement[] endSpawnPoints = endLayout.spawnPoints;
        for (int i = 0; i < activeSpawnPoints.Length; i++)
        {
            if(i>= startSpawnPoints.Length || i >= endSpawnPoints.Length)
            {
                continue;
            }    
            activeSpawnPoints[i].transform.position = Vector3.Lerp(startSpawnPoints[i].pos, endSpawnPoints[i].pos, transitionPercent);
        }
    }

    public static StageElements GetStageElements(TextAsset file)
    {
        Assert.IsTrue(file != null);
        StageElements layout = JsonUtility.FromJson<StageElements>(file.text);
        return layout;
    }
}
