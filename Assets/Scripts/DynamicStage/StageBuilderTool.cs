///
/// Debug tool for level Designers
/// Gizmos are drawn in Cell.cs
/// - Toby
/// 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using PathFinding;
using System.IO;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class StageBuilderTool : MonoBehaviour
    {
        [InfoBox("Changing the order of any of the cells in the hierarchy will break any existing stage files.")]
        [InfoBox("This tool will only record the position/scale/Solidity of GameObjects with the Cell component")]
        #region Import
        [Header("Import Settings")]
        public TextAsset defaultStage;
        public TextAsset stageToLoad;

        [Button("Load Default Stage")]
        public void LoadDefaultStage()
        {
            StageDebugger.LoadStage(defaultStage);
        }

        [Button("Import Stage File")]
        public void ImportJson()
        {
            StageDebugger.LoadStage(stageToLoad);
        }

        #endregion

        #region export

        [Header("Export Settings")]
        [SerializeField] private string _path = "Assets\\Stages\\";
        [SerializeField] private string _fileName = "Stage Layout.txt";
        [SerializeField] private string _fileType = "JSON";

        [Button]
        ///
        /// Create new file with all the cell information
        /// 
        public void ExportStageFile()
        {
            Cell[] cells = GameObject.FindObjectsOfType<Cell>();
            Debug.Log("Found " + cells.Length + " cells in " + SceneManager.GetActiveScene().name);
            
            EnemySpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<EnemySpawnPoint>();
            Debug.Log("Found " + spawnPoints.Length + " enemy spawn points in " + SceneManager.GetActiveScene().name);

            StageElements stageElements = new StageElements();
            stageElements.SceneName = SceneManager.GetActiveScene().name;
            stageElements.elements = new StageElement[cells.Length];
            stageElements.spawnPoints = new SpawnPointElement[spawnPoints.Length];

            for (int i = 0; i<cells.Length; i++)
            {
                stageElements.elements[i] = new StageElement(cells[i]);
            }

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                stageElements.spawnPoints[i] = new SpawnPointElement(spawnPoints[i]);
            }

            string path = GetNewPath();
            var textFile = File.CreateText(path);

            string elemString = JsonUtility.ToJson(stageElements);
            textFile.WriteLine(elemString);

            textFile.Close();
            Debug.Log("Created new text file at " + path+"\nIt may take a few seconds for the file to appear in the inspector.");
        }

        private string GetNewPath()
        {
            int number = 1;
            while(true)
            {
                string path = "";

                if(number == 1)
                    path = _path + _fileName + "." + _fileType;
                else
                    path = _path + _fileName + " " + number + "." + _fileType;

                if (!File.Exists(path))
                    return path;

                number++;
            }
            
        }
        #endregion
    }

    [Serializable]
    public class StageElements
    {
        public string SceneName;
        public StageElement[] elements;
        public SpawnPointElement[] spawnPoints;
    }

    [Serializable]
    public class StageElement
    {
        public Vector3 p; //position
        public Vector3 ls; //localScale
        public Quaternion lr; //localRotation
        public bool sld; //solid
        public StageElement(Cell cell)
        {
            p = cell.transform.position;
            ls = cell.transform.localScale;
            lr = cell.transform.localRotation;
            sld = cell.Solid;
        }
    }

    [Serializable]
    public class SpawnPointElement
    {
        public Vector3 pos;
        public SpawnPointElement(EnemySpawnPoint spawnPoint)
        {
            pos = spawnPoint.transform.position;
        }
    }

}
