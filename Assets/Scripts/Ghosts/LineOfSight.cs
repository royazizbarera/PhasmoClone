using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ghosts
{
    public class LineOfSight : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _layerMask;
        private RaycastHit _ray;

        private PlayerCheckResult _checkResult = new PlayerCheckResult();

        public PlayerCheckResult CheckForPlayer(Transform playerTransform)
        {
            
            return _checkResult;
        }
    }

    public struct PlayerCheckResult
    {
        public bool IsPlayerVisible;
        public float DistanceToPlayer;
    }
}