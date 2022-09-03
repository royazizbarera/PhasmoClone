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

        private const int SanityDivider = 2;

        private SanityHandler _playerSanity;
        private float _ghostAnger = 0f;
        void Start()
        {
            _ghostStateMachine.ChangeState(_idleState);
            if (_ghostInfo.SetedUp) SetUp();
            else _ghostInfo.GhostSetedUp += SetUp;
        }

        private void Update()
        {
            _ghostInfo.FinalGhostAnger = Mathf.Max(0f, _ghostAnger - _playerSanity.Sanity / SanityDivider);
        }

        public void SetGhostAnger(float ghostAngerToAdd)
        {
            _ghostAnger = ghostAngerToAdd;
        }

        public void GetAngry()
        {
            _ghostStateMachine.ChangeState(_attackState);
        }

        private void SetUp()
        {
            _playerSanity = _ghostInfo.PlayerSanity;
        }
    }
}