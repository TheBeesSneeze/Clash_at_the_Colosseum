///
/// Just a node that contains the player.
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace PathFinding
{
    public class PlayerPath : Path
    {
        public override Vector3 position { get => _target.position; }
        private Transform _target;
        public PlayerPath(Transform targetTransform)
        {
            _distanceToStart = 0;
            _distanceToTarget = 0;
            _heightDifference = 0;
            _target = targetTransform;
        }
    }
}


