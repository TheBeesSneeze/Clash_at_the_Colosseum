using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

namespace PathFinding
{
    public class CellManager : Singleton<CellManager>
    {
        //public GameObject player;
        private Cell[] _allCells;
        public Path path;
        public Cell playerCell;

        // Start is called before the first frame update
        void Start()
        {
            _allCells = GameObject.FindObjectsOfType<Cell>();
        }

        public void PlayerCellUpdate(Cell cell)
        {
           playerCell = cell;
        }

        public Path GetPathToPlayer(Cell navigator)
        {
            if (playerCell == null)
                return null;

            return Navigate(navigator, playerCell);
        }

        //doesnt account for if start / end are right next to each other
        // or if start == end
        // or if start cant navigate to end
        public Path Navigate(Cell navigator, Cell target)
        {
            //List<CellNode> path = new List<CellNode>();
            //path.Add(new CellNode(start, end));

            List<Path> nextCells = new List<Path>();
            List<Cell> exploredCells = new List<Cell>();
            Path startNode = new Path(target, navigator);

            foreach (Cell cell in target.SideNeighbors)
            {
                if(!cell.Solid)
                    nextCells.Add(new Path( cell, startNode, navigator));
            }
            nextCells.Sort();

            exploredCells.Add(target);

            bool done = false;
            while(!done && nextCells.Count > 0)
            {
                Path next = nextCells[0];
                nextCells.RemoveAt(0);
                exploredCells.Add(next.cell);
                
                if(next.cell == navigator)
                    return next;

                foreach (Cell cell in next.cell.SideNeighbors)
                {
                    if(!exploredCells.Contains(cell) && !cell.Solid)
                        nextCells.Add(new Path(cell, next, navigator));
                }
                nextCells.Sort();

            }

            return null;
        }

        
    }

    [System.Serializable]
    public class Path : IComparable<Path>
    {
        public int Cost { get=> (_distanceToStart + _distanceToTarget); }

        public Cell cell;
        public Path nextPath;

        private int _distanceToTarget;
        private int _distanceToStart;

        public float DistanceToStart { get => _distanceToStart; }
        public float DistanceToTarget { get => _distanceToTarget; }

        public Path(Cell cell, Cell target)
        {
            this.cell = cell;
            _distanceToStart = 0;
            _distanceToTarget = Mathf.RoundToInt(Vector3.Distance(cell.transform.position, target.transform.position)*10);
        }

        public Path(Cell cell, Path lastPath, Cell target)
        {
            this.cell = cell;
            _distanceToStart = Mathf.RoundToInt(lastPath.DistanceToStart + Vector3.Distance(this.cell.transform.position, lastPath.cell.transform.position)*10);
            _distanceToTarget = Mathf.RoundToInt(Vector3.Distance(cell.transform.position, target.transform.position)*10);
            nextPath = lastPath;
        }

        public int CompareTo(Path other)
        {
            /*
            if(other.Value < Value)
                return -1;
            if(other.Value > Value)
                return 1;

            if(other.DistanceToTarget< DistanceToTarget)
                return -1;
            if (other.DistanceToTarget > DistanceToTarget)
                return 1;
            */

            if (other.Cost < Cost)
                return 1;
            if (other.Cost > Cost)
                return -1;

            if (other.DistanceToTarget < DistanceToTarget)
                return 1;
            if (other.DistanceToTarget > DistanceToTarget)
                return -1;

            return 0;
        }
    }
}

