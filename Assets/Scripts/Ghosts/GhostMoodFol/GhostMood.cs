using UnityEngine;

namespace Ghosts.Mood
{
    public class GhostMood : MonoBehaviour
    {
        [SerializeField]
        private GhostInfo _ghostInfo;
        [SerializeField]
        private GhostState _attackState, _idleState, _ghostEventState;
        [SerializeField]
        private GhostStateMachine _ghostStateMachine;

        private bool _subscribedToGhostSetUp = false;
        public bool IsHunting = false;

        private const int SanityDivider = 2;

        private float _huntTime = 0f;

        private SanityHandler _playerSanity;

        public float _ghostAnger = 0f;
        public float _ghostFinalAnger = 0f;
        void Start()
        {
            _ghostStateMachine.ChangeState(_idleState);
            if (_ghostInfo.SetedUp) { SetUp(); _subscribedToGhostSetUp = false; }
            else { _ghostInfo.GhostSetedUp += SetUp; _subscribedToGhostSetUp = true; }
        }

        private void OnDestroy()
        {
            if (_subscribedToGhostSetUp) _ghostInfo.GhostSetedUp -= SetUp;
        }

        private void Update()
        {
            if(_playerSanity != null) _ghostInfo.FinalGhostAnger = Mathf.Max(0f, _ghostAnger - _playerSanity.Sanity / SanityDivider);

            _ghostFinalAnger = _ghostInfo.FinalGhostAnger;
        }

        public void SetGhostAnger(float ghostAnger)
        {
            _ghostAnger = ghostAnger;
        }

        public void StartHunting(float huntTime)
        {
            _huntTime = huntTime;
            IsHunting = true;
            _ghostStateMachine.ChangeState(_attackState);

            Invoke(nameof(StopHunting), huntTime);
        }

        public void StopHunting()
        {
            IsHunting = false;
            _ghostStateMachine.ChangeState(_idleState);
        }

        public void StartGhostEvent()
        {
            _ghostStateMachine.ChangeState(_ghostEventState);
        }

        public void StopGhostEvent()
        {
            _ghostStateMachine.ChangeState(_idleState);
        }

        private void SetUp()
        {
            _playerSanity = _ghostInfo.PlayerSanity;
        }
    }
}