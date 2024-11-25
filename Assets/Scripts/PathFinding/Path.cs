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
        public virtual Vector3 position { get =>  cell.PathPosition; } 
        public int Cost { get => (_distanceToStart + _distanceToTarget + (int)(_heightDifference*100)); }

        public Cell cell;
        public Path nextPath;

        protected int _distanceToTarget;
        protected int _distanceToStart;
        protected float _heightDifference;

        public float DistanceToStart { get => _distanceToStart; }
        public float DistanceToTarget { get => _distanceToTarget; }


        public Path() {}
        //public Path(Transform targetTransform){}

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

            Debug.DrawLine(position + Vector3.up, nextPath.position + Vector3.up, Color.magenta, 1);

            nextPath.DrawPath();
        }

        /// <summary>
        /// Return number of paths ahead (recursively)
        /// </summary>
        public int GetPathLength()
        {
            if (nextPath == null)
                return 0;

            return nextPath.GetPathLength() + 1;
        }

        public bool IsLengthGreaterThan(int value)
        {
            if (value < 0)
                return false;

            if(nextPath != null)
                return nextPath.IsLengthGreaterThan(value-1);

            return true;
        }
    }
}
