using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ghosts.GhostMood
{
    public class GhostMood : MonoBehaviour
    {
        [SerializeField]
        private GhostInfo _ghostInfo;

        [SerializeField]
        private GhostState _attackState, _idleState;
        [SerializeField]
        private GhostStateMachine _ghostStateMachine;

        private float _ghostAnger = 0f;

        void Start()
        {
            _ghostStateMachine.ChangeState(_idleState);
        }

        private void Update()
        {
            _ghostInfo.FinalGhostAnger = _ghostAnger;
        }

        public void SetGhostAnger(float ghostAngerToAdd)
        {
            _ghostAnger = ghostAngerToAdd;
        }

        public void GetAngry()
        {
            _ghostStateMachine.ChangeState(_attackState);
        }
    }
}