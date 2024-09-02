using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PathFinding
{
    public class CellManager
    {
        private static Cell[] _allCells;

        // Start is called before the first frame update
        public CellManager()
        {
            _allCells = GameObject.FindObjectsOfType<Cell>();
        }

        public static void UpdateCellNeighbors()
        {

        }
    }
}