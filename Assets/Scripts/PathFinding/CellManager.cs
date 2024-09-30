using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public class CellManager
    {
        private static Cell[] _allCells;
        private Transform _player;
        public static Cell PlayerCell;

        // Start is called before the first frame update
        public CellManager()
        {
            _allCells = GameObject.FindObjectsOfType<Cell>();
            _player = GameObject.FindObjectOfType<PlayerBehaviour>().transform;
        }

        public static void UpdateCellNeighbors()
        {

        }

        //called in gamemanager
        public void Update()
        {
            //might be able to replace with a getter in pathmanager?

            if(Physics.Raycast(_player.transform.position, Vector3.down, out RaycastHit hit,Mathf.Infinity))
            {
                if (hit.transform.TryGetComponent<Cell>(out Cell c))
                {
                    PlayerCell = c;
                }
            }
        }
    }
}