using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace PathFinding
{
    public class CellManager
    {
        private static Cell[] _allCells;

        public static float cellFallTime=10;
        public static float cellFallDistance=10;

        // Start is called before the first frame update
        public CellManager(float _cellFallTime, float _cellFallDistance)
        {
            _allCells = GameObject.FindObjectsOfType<Cell>();
            cellFallDistance = _cellFallTime;
        }


        
    }
}