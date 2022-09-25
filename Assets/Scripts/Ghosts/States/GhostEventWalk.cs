using Infrastructure;
using Infrastructure.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ghosts
{
    public class GhostEventWalk : MonoBehaviour
    {
        [SerializeField]
        private UnityEngine.AI.NavMeshAgent _agent;
        [SerializeField]
        private float _stoppingDistance = 0.3f;
        [SerializeField]
        private GhostInfo _ghostInfo;

        [SerializeField]
        private LineOfSight _lineOfSight;
        [SerializeField]
        private float _checkForLineCD;

        private bool _playerKilled = false;

        private Transform _playerPoint;
        private Transform _heroTransform;
        private PlayerCheckResult _playerCheckResult;

        private WaitForSeconds _checkForLineWait;

        private bool _isGhostDisabled = false;

        private bool _dataSetedUp = false;
        private float _ghostAttackSpeed;
        private Transform[] _patrolPoints;
        private LevelSetUp _levelSetUp;

        private Transform _currDestination = null;

        private const float DistanceToKill = 1f;
        private const float DisableTime = 3f;
        private int _randomPointNum;
        private bool _isInGhostEvent = false;
        private bool _isFollowing = false;
        private float _maxAggroDistance;
        private bool _subscribedToGhostSetUp = false;

        private Vector3 _playerPointDistance;
        private Vector3 _ghostPointDistance;

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
            if (_isGhostDisabled) return;

            if (_isFollowing)
            {
                _currDestination = _playerPoint;
                CheckForKill();
                SetDestination();
                return;
            }
            if (_agent.remainingDistance <= _stoppingDistance || _currDestination == null)
            {
                ChoosePoint();
                SetDestination();
            }
        }

        public void StartGhostEvent()
        {
            _isGhostDisabled = true;
            _isFollowing = false;
            _agent.speed = _ghostAttackSpeed;
            SwitchGhostEventState(true);
            StartCoroutine(CheckForPlayerVisible());
        }

        public void StopGhostEvent()
        {
            SwitchGhostEventState(false);
        }


        public void SwitchGhostEventState(bool isGhostEvent)
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

        private IEnumerator CheckForPlayerVisible()
        {
            while (true)
            {
                if (!_isInGhostEvent) break;

                    _playerCheckResult = _lineOfSight.CheckForPlayer(_playerPoint, _heroTransform);
                    if (_playerCheckResult.IsPlayerVisible && _playerCheckResult.DistanceToPlayer <= _maxAggroDistance)
                    {
                        _isFollowing = true;
                    }
                    else _isFollowing = false;
                yield return _checkForLineWait;
            }
            yield return null;
        }

        private void CheckForKill()
        {
            _playerPointDistance = _heroTransform.position;
            _ghostPointDistance = transform.position;

            if ((Vector3.Distance(_playerPointDistance, _ghostPointDistance) < DistanceToKill) && !_playerKilled)
            {
                _playerKilled = true;
            }
        }
        private void ChoosePoint()
        {
            _randomPointNum = Random.Range(0, _patrolPoints.Length - 1);
            _currDestination = _patrolPoints[_randomPointNum];
        }

        private void SetDestination()
        {
            if (_currDestination != null) _agent.SetDestination(_currDestination.position);
            else { Debug.Log("Curr destination = null"); }
        }

        private void SetUpGhostData()
        {
            _levelSetUp = AllServices.Container.Single<LevelSetUp>();

            _checkForLineWait = new WaitForSeconds(_checkForLineCD);
            _patrolPoints = _levelSetUp.GetGhostPatrolPoints();

            if (_ghostInfo.SetedUp) { SetUpGhostInfo(); _subscribedToGhostSetUp = false; }
            else
            {
                _subscribedToGhostSetUp = true;
                _ghostInfo.GhostSetedUp += SetUpGhostInfo;
            }
            _dataSetedUp = true;
        }
        private void SetUpGhostInfo()
        {
            _playerPoint = _ghostInfo.PlayerPoint;
            _heroTransform = _ghostInfo.PlayerTransform;

            _ghostAttackSpeed = _ghostInfo.GhostData.GhostAttackSpeed;
            _maxAggroDistance = _ghostInfo.GhostData.MaxDistanceToPlayerAggr;
            // _playerTransform = _ghostInfo.
        }
    }
}
