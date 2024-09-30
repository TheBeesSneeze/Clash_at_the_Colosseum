using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    [System.Serializable]
    public class Path : IComparable<Path>
    {
        public Vector3 position { get =>  cell.transform.position; } 
        public int Cost { get => (_distanceToStart + _distanceToTarget); }

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
            _distanceToTarget = Mathf.RoundToInt(Vector3.Distance(cell.PathPosition, target.PathPosition) * 10);
        }

        public Path(Cell cell, Path lastPath, Cell target)
        {
            this.cell = cell;
            _distanceToStart = Mathf.RoundToInt(lastPath.DistanceToStart + Vector3.Distance(this.cell.PathPosition, lastPath.position) * 10);
            _distanceToTarget = Mathf.RoundToInt(Vector3.Distance(cell.PathPosition, target.PathPosition) * 10);
            nextPath = lastPath;
        }

        public int CompareTo(Path other)
        {
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
