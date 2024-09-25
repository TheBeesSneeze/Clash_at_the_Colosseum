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

            StageLayout stageElements = new StageLayout();
            stageElements.SceneName = SceneManager.GetActiveScene().name;
            stageElements.elements = new CellObject[cells.Length];
            stageElements.spawnPoints = new SpawnPointElement[spawnPoints.Length];

            for (int i = 0; i<cells.Length; i++)
            {
                stageElements.elements[i] = new CellObject(cells[i]);
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
    public class StageLayout
    {
        public string SceneName;
        public CellObject[] elements;
        public SpawnPointElement[] spawnPoints;
        public DecorElement[] decorObjects;
    }

    [Serializable]
    public class CellObject
    {
        public Vector3 pos; //position
        public Vector3 locScale; //localScale
        public Quaternion rot; //localRotation
        public bool slope; //solid
        public float other1;
        public float other2;
        public CellObject(Cell cell)
        {
            pos = cell.transform.position;
            locScale = cell.transform.localScale;
            rot = cell.transform.rotation;
            slope = cell.Slope;
            other1 = 0;
            other2 = 0;
        }
    }


    [Serializable]
    public class SpawnPointElement
    {
        public Vector3 pos;
        public int enemyIndex = 0;
        public float other1;
        public float other2;
        public SpawnPointElement(EnemySpawnPoint spawnPoint)
        {
            pos = spawnPoint.transform.position;
            enemyIndex = (int)spawnPoint.enemyToSpawn;
            other1 = 0;
            other2 = 0;
        }
    }

    [Serializable]
    public class DecorElement
    {
        public Vector3 pos; //position
        public Vector3 locScale; //localScale
        public Quaternion rot; //localRotation
        public bool solid;
        public float other1;
        public float other2;
        public DecorElement(Decor decor)
        {
            pos = decor.transform.position;
            locScale = decor.transform.localScale;
            rot = decor.transform.rotation;
            solid = decor.Solid;
            other1 = 0;
            other2 = 0;
        }
    }
}
