using PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class StageDebugger : MonoBehaviour
    {
        [OnValueChanged("TransitionStages")]
        [SerializeField] TextAsset _startLayout;
        [OnValueChanged("TransitionStages")]
        [SerializeField] TextAsset _endLayout;
        [OnValueChanged("TransitionStages")]
        [SerializeField][Range(0,1)] float transitionPercent;

        public void TransitionStages()
        {
            if (_startLayout == null || _endLayout == null)
                return ;

            Debug.Log(transitionPercent);

            //TODO: how to lerp solidity???

            StageElements startLayout = GetStageElements(_startLayout);
            StageElements endLayout = GetStageElements(_endLayout);

            Cell[] activeCells = GameObject.FindObjectsOfType<Cell>();
            EnemySpawnPoint[] activeSpawnPoints = GameObject.FindObjectsOfType<EnemySpawnPoint>();

            //Assert.AreEqual(stageElements.SceneName, SceneManager.GetActiveScene().name); // Yeah. we went there. deal with it.
            if (startLayout.SceneName != endLayout.SceneName)
                Debug.LogWarning("Stages were built in seperate unity scenes");
            Assert.AreEqual(startLayout.elements.Length, endLayout.elements.Length);
            Assert.AreEqual(startLayout.spawnPoints.Length, endLayout.spawnPoints.Length);

            StageElement[] startCellData = startLayout.elements;
            StageElement[] endCellData = endLayout.elements;
            for (int i = 0; i < activeCells.Length; i++)
            {
                Transform cell = activeCells[i].transform;
                cell.position = Vector3.Lerp(startCellData[i].p, endCellData[i].p, transitionPercent);
                cell.localScale = Vector3.Lerp(startCellData[i].ls, endCellData[i].ls, transitionPercent);
                cell.localRotation = Quaternion.Lerp(startCellData[i].lr, endCellData[i].lr, transitionPercent);
            }

            SpawnPointElement[] startSpawnPoints = startLayout.spawnPoints;
            SpawnPointElement[] endSpawnPoints = endLayout.spawnPoints;
            for (int i = 0; i < activeSpawnPoints.Length; i++)
            {
                activeSpawnPoints[i].transform.position = Vector3.Lerp(startSpawnPoints[i].pos, endSpawnPoints[i].pos, transitionPercent);}
        }

        public static StageElements GetStageElements(TextAsset file)
        {
            Assert.IsTrue(file != null);
            StageElements layout = JsonUtility.FromJson<StageElements>(file.text);
            return layout;
        }

        /// <summary>
        /// Instantly sets stage to match stageElements
        /// </summary>
        public static void LoadStage(TextAsset stageToLoad)
        {
            if (stageToLoad == null)
            {
                Debug.LogWarning("No stage file loaded");
                return;
            }

            StageElements stageElements = GetStageElements(stageToLoad);
            StageElement[] cellEmements = stageElements.elements;
            SpawnPointElement[] spawnPointElements = stageElements.spawnPoints;

            Cell[] cells = GameObject.FindObjectsOfType<Cell>();
            EnemySpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<EnemySpawnPoint>();

            //Assert.AreEqual(stageElements.SceneName, SceneManager.GetActiveScene().name); // Yeah. we went there. deal with it.
            if (stageElements.SceneName != SceneManager.GetActiveScene().name)
                Debug.LogWarning("Stages were built in seperate unity scenes");
            Assert.AreEqual(cellEmements.Length, cells.Length); 
            Assert.AreEqual(spawnPoints.Length, spawnPoints.Length); 

            for (int i = 0; i < cellEmements.Length; i++)
            {
                cells[i].Solid = cellEmements[i].sld;
                Transform cell = cells[i].transform;
                cell.position   = cellEmements[i].p;
                cell.localScale = cellEmements[i].ls;
                cell.localRotation = cellEmements[i].lr;
            }

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i].transform.position = spawnPointElements[i].pos;
            }
        }


    }

}
