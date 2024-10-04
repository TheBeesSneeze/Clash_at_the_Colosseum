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
using UnityEditor;

namespace Utilities
{
    public class StageBuilderTool : MonoBehaviour
    {
        [InfoBox("Changing the order of any of the cells in the hierarchy will break any existing stage files.")]
        [InfoBox("This tool will only record the position/scale/Solidity of GameObjects with the Cell component")]
        #region Import
        [Header("Import Settings")]
        public TextAsset StageA;
        public TextAsset StageB;

        [Button("Load Stage A")]
        public void LoadStageA()
        {
            StageDebugger.LoadStage(StageA);
        }

        [Button("Load Stage B")]
        public void LoadStageB()
        {
            StageDebugger.LoadStage(StageB);
        }

        #endregion

        #region export

        [Header("Export Settings")]
        [SerializeField] private string _path = "Assets\\Stages\\";
        [SerializeField] private string _fileName = "Stage Layout.txt";
        [SerializeField] private string _fileType = "JSON";

        [Button("Export stage as new file")]
        public void ExportStageFile()
        {
            StageLayout stageElements = GetStageLayout();

            string path = GetNewPath();
            var textFile = File.CreateText(path);

            string elemString = JsonUtility.ToJson(stageElements);
            textFile.WriteLine(elemString);

            textFile.Close();
            Debug.Log("Created new text file at " + path+"\nIt may take a few seconds for the file to appear in the inspector.");
        }

        [Button("Update Stage A")]
        public void UpdateStageA()
        {
            if (StageA == null)
                return;

            OverrideStageFile(StageA);
        }

        [Button("Update Stage B")]
        public void UpdateStageB()
        {
            if (StageB == null)
                return;

            OverrideStageFile(StageB);
        }

        private void OverrideStageFile(TextAsset oldFile)
        {
            StageLayout stageElements = GetStageLayout();

            string path = AssetDatabase.GetAssetPath(oldFile);
            var textFile = File.CreateText(path);

            string elemString = JsonUtility.ToJson(stageElements);
            textFile.WriteLine(elemString);

            textFile.Close();
            Debug.Log(oldFile.name+" has been overwritten at " + path);
        }

        private StageLayout GetStageLayout()
        {
            Cell[] cells = GameObject.FindObjectsOfType<Cell>();
            Debug.Log("Found " + cells.Length + " cells in " + SceneManager.GetActiveScene().name);

            EnemySpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<EnemySpawnPoint>();
            Debug.Log("Found " + spawnPoints.Length + " enemy spawn points in " + SceneManager.GetActiveScene().name);

            StageLayout stageElements = new StageLayout();
            stageElements.SceneName = SceneManager.GetActiveScene().name;
            stageElements.elements = new CellObject[cells.Length];
            stageElements.spawnPoints = new SpawnPointElement[spawnPoints.Length];

            for (int i = 0; i < cells.Length; i++)
            {
                stageElements.elements[i] = new CellObject(cells[i]);
            }

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                stageElements.spawnPoints[i] = new SpawnPointElement(spawnPoints[i]);
            }

            return stageElements;
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
        public bool visible;
        public float other1;
        public float other2;
        public DecorElement(Decor decor)
        {
            pos = decor.transform.position;
            locScale = decor.transform.localScale;
            rot = decor.transform.rotation;
            solid = decor.Solid;
            visible = decor.Visible;
            other1 = 0;
            other2 = 0;
        }
    }
}
