using Ghosts.Mood;
using UnityEngine;

namespace Ghosts
{
    public class GhostEventWalk : MonoBehaviour
    {
        [SerializeField]
        private GhostMood _ghostMood;

        [SerializeField]
        private UnityEngine.AI.NavMeshAgent _agent;
        [SerializeField]
        private float _stoppingDistance = 1f;
        [SerializeField]
        private GhostInfo _ghostInfo;
        [SerializeField]
        private float _ghostSpeed = 2f;


        private const float MinTimeGhostEvent = 4f;
        private const float MaxTimeGhostEvent = 8f;

        private float _timeOfCurrentGhostEvent;
        private float _durationOfGhostEvent;

        private Transform _playerPoint;

        private bool _isInGhostEvent = false;
        private bool _subscribedToGhostSetUp = false;

        private void Start()
        {
             SetUpGhostData();
        }

        private void OnDestroy()
        {
            if(_subscribedToGhostSetUp)
            _ghostInfo.GhostSetedUp -= SetUpGhostInfo;
        }
        private void Update()
        {
            if (!_isInGhostEvent) return;
            if (IsGhostEventTimeOver()) StopGhostEventState();

            SetDestination();
            if (Vector3.Distance(transform.position, _playerPoint.position)  <= _stoppingDistance)
            {
                GhostDissapear();
            }
        }

        public void StartGhostEvent()
        {
            _agent.speed = _ghostSpeed;
            _timeOfCurrentGhostEvent = 0f;
            _durationOfGhostEvent = Random.Range(MinTimeGhostEvent, MaxTimeGhostEvent);

            SwitchGhostEventState(true);
        }

        public void StopGhostEvent() => SwitchGhostEventState(false);

        private void SwitchGhostEventState(bool isGhostEvent)
        {
            _isInGhostEvent = isGhostEvent;
            if (!_agent.isOnNavMesh) return;
            if (isGhostEvent)
            {
                _agent.isStopped = false;
            }
            else
            {
                _agent.ResetPath();
                _agent.isStopped = true;
            }
        }

        private void StopGhostEventState() => _ghostMood.StopGhostEvent();

        private void GhostDissapear()
        {
            StopGhostEventState();
        }

        private bool IsGhostEventTimeOver()
        {
            _timeOfCurrentGhostEvent += Time.deltaTime;

            if (_timeOfCurrentGhostEvent >= _durationOfGhostEvent) return true;
            else return false;
        }


        private void SetDestination()
        {
            _agent.SetDestination(_playerPoint.position);
        }


        private void SetUpGhostData()
        {
            if (_ghostInfo.SetedUp) { SetUpGhostInfo(); _subscribedToGhostSetUp = false; }
            else
            {
                _subscribedToGhostSetUp = true;
                _ghostInfo.GhostSetedUp += SetUpGhostInfo;
            }
        }
        private void SetUpGhostInfo()
        {
            _playerPoint = _ghostInfo.PlayerPoint;
        }
    }
}
