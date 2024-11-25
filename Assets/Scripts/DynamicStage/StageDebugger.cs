using PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying) return;
#endif

            if (_startLayout == null || _endLayout == null)
                return ;

            StageTransitionManager.TransitionStagePercentByFile(_startLayout, _endLayout,transitionPercent);
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

            StageTransitionManager.TransitionStagePercentByFile(stageToLoad, stageToLoad, 0);

            /*

            StageLayout stageElements = StageTransitionManager.GetStageElements(StageB);
            CellObject[] cellEmements = stageElements.elements;
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
                cells[i].Solid = cellEmements[i].slope;
                Transform cell = cells[i].transform;
                cell.position   = cellEmements[i].pos;
                cell.localScale = cellEmements[i].locScale;
                cell.localRotation = cellEmements[i].rot;
            }

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i].transform.position = spawnPointElements[i].pos;
            }
            */
        }


    }

}
