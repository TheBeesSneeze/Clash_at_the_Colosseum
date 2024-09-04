///
/// Debug tool for level Designers
/// - Toby
/// 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using PathFinding;

namespace Utilities
{
    public class StageBuilder : MonoBehaviour
    {
        public TextAsset json;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(JsonUtility.ToJson(this.transform.position));
        }

        // Update is called once per frame
        void Update()
        {

        }

        [Button]
        public void SaveToJson()
        {
            Cell[] cells = GameObject.FindObjectsOfType<Cell>();
            foreach (Cell cell in cells)
            {
                StageElement elem = new StageElement(this.transform);
                string elemString = JsonUtility.ToJson(elem);
            }
            

            Debug.Log(elemString);
        }
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
