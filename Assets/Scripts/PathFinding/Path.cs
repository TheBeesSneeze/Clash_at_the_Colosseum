using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PathFinding
{
    [System.Serializable]
    public class Path : IComparable<Path>
    {
        public Vector3 position { get =>  cell.PathPosition; } 
        public int Cost { get => (_distanceToStart + _distanceToTarget + (int)(_heightDifference*100)); }

        public Cell cell;
        public Path nextPath;

        private int _distanceToTarget;
        private int _distanceToStart;
        private float _heightDifference;

        public float DistanceToStart { get => _distanceToStart; }
        public float DistanceToTarget { get => _distanceToTarget; }

        public Path(Cell cell, Cell target)
        {
            this.cell = cell;
            _distanceToStart = 0;
            _distanceToTarget = Mathf.RoundToInt(Vector3.Distance(cell.PathPosition, target.PathPosition) * 10);
            _heightDifference = 0;
        }

        public Path(Cell cell, Path lastPath, Cell target)
        {
            this.cell = cell;
            _distanceToStart = Mathf.RoundToInt(lastPath.DistanceToStart + Vector3.Distance(this.cell.PathPosition, lastPath.position) * 10);
            _distanceToTarget = Mathf.RoundToInt(Vector3.Distance(cell.PathPosition, target.PathPosition) * 10);
            _heightDifference = position.y - lastPath.position.y;
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

        /// <summary>
        /// Debug
        /// </summary>
        public void DrawPath()
        {
            if (nextPath == null)
                return;

            Debug.DrawLine(position + Vector3.up, nextPath.position + Vector3.up, Color.red);

            nextPath.DrawPath();
        }
    }
}
