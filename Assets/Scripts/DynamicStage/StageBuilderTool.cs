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
        public void ExportStageFile()
        {
            //Create new file with all the cell information
            Cell[] cells = GameObject.FindObjectsOfType<Cell>();
            Debug.Log("Found " + cells.Length + " cells in scene");

            StageElements stageElements = new StageElements();
            stageElements.elements = new StageElement[cells.Length];

            string path = GetNewPath();
            var textFile = File.CreateText(path);

            for(int i = 0; i<cells.Length; i++)
            {
                stageElements.elements[i] = new StageElement(cells[i]);
            }

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
                string path = _path + _fileName + " " + number + "." + _fileType;
                if(!File.Exists(path))
                    return path;

                number++;
            }
            
        }
        #endregion
    }

    [Serializable]
    public class StageElements
    {
        //This can NOT be the only way to do this i am SO MAD
        public StageElement[] elements;
    }

    [Serializable]
    public class StageElement
    {
        public Vector3 position;
        public Vector3 localScale;
        public bool solid;
        public StageElement(Cell cell)
        {
            position = cell.transform.position;
            localScale = cell.transform.localScale;
            solid = cell.Solid;
        }
    }

}
