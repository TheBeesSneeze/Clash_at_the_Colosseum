using PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using NaughtyAttributes;

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

            //TODO: how to lerp solid???

            StageElement[] startLayout = GetStageElements(_startLayout);
            StageElement[] endLayout = GetStageElements(_endLayout);
            Cell[] cells = GameObject.FindObjectsOfType<Cell>();

            Assert.AreEqual(startLayout.Length, endLayout.Length);
            Assert.AreEqual(cells.Length, startLayout.Length);

            for (int i = 0; i < cells.Length; i++)
            {
                Transform cell = cells[i].transform;
                cell.position = Vector3.Lerp(startLayout[i].position, endLayout[i].position, transitionPercent);
                cell.localScale = Vector3.Lerp(startLayout[i].localScale, endLayout[i].localScale, transitionPercent);
            }
        }

        public static StageElement[] GetStageElements(TextAsset file)
        {
            Assert.IsTrue(file != null);
            StageElements layout = JsonUtility.FromJson<StageElements>(file.text);
            return layout.elements;
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

            StageElement[] stageElements = GetStageElements(stageToLoad);
            Cell[] cells = GameObject.FindObjectsOfType<Cell>();

            Assert.AreEqual(stageElements.Length, cells.Length); // Yeah. we went there. deal with it.

            for (int i = 0; i < stageElements.Length; i++)
            {
                cells[i].Solid = stageElements[i].solid;
                Transform cell = cells[i].transform;
                cell.position   = stageElements[i].position;
                cell.localScale = stageElements[i].localScale;
            }
        }


    }

}
