using UnityEngine;
using Infrastructure.Services;
using Infrastructure;
using UnityEngine.AI;
using Utilities.Constants;
using System.Collections;

namespace Ghosts
{
    public class AttackPatrol : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent _agent;
        [SerializeField]
        private float _stoppingDistance = 0.3f;
        [SerializeField]
        private GhostInfo _ghostInfo;

        [SerializeField]
        private LineOfSight _lineOfSight;
        [SerializeField]
        private float _checkForLineCD;

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


        private const float DisableTime = 3f;
        private int _randomPointNum;
        private bool _isAttacking = false;
        private bool _isFollowing = false;
        private float _maxAggroDistance;

        private void OnEnable()
        {
            if (!_dataSetedUp) SetUpGhostData();
        }


        private void OnDestroy()
        {
            _ghostInfo.GhostSetedUp -= SetUpPlayerTranform;
        }
        private void Update()
        {
            if (!_isAttacking) return;
            if (_isGhostDisabled) return;

            if (_isFollowing)
            {
                _currDestination = _playerPoint;
                SetDestination();
                return;
            }
            if (_agent.remainingDistance <= _stoppingDistance || _currDestination == null)
            {
                ChoosePoint();
                SetDestination();
            }
            CheckForKill();
        }

        public void StartAttackPatrolling()
        {
            _isGhostDisabled = true;
            _isFollowing = false;
            _agent.speed = _ghostAttackSpeed;
            SwitchAttackState(true);
            StartCoroutine(CheckForPlayerVisible());
            Invoke(nameof(EnableAttackAfterCD), DisableTime);
        }

        public void StopAttackPatrolling()
        {
            SwitchAttackState(false);
        }


        public void SwitchAttackState(bool isAttacking)
        {
            _isAttacking = isAttacking;
            if (!_agent.isOnNavMesh) return;
            if (isAttacking)
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
                if (!_isAttacking) break;
                if (!_isGhostDisabled)
                {
                    _playerCheckResult = _lineOfSight.CheckForPlayer(_playerPoint, _heroTransform);
                    if (_playerCheckResult.IsPlayerVisible && _playerCheckResult.DistanceToPlayer <= _maxAggroDistance)
                    {
                        _isFollowing = true;
                    }
                    else _isFollowing = false;
                }
                yield return _checkForLineWait;
            }
            yield return null;
        }

        private void CheckForKill()
        {
            
        }
        private void EnableAttackAfterCD() => _isGhostDisabled = false;
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
            _ghostAttackSpeed = _ghostInfo.GhostData.GhostAttackSpeed;
            _maxAggroDistance = _ghostInfo.GhostData.MaxDistanceToPlayerAggr;
            _patrolPoints = _levelSetUp.GetGhostPatrolPoints();
            if (_ghostInfo.PlayerPoint != null) _playerPoint = _ghostInfo.PlayerPoint;
            else
            {
                _ghostInfo.GhostSetedUp += SetUpPlayerTranform;
            }
            _dataSetedUp = true;
        }
        private void SetUpPlayerTranform()
        {
            _playerPoint = _ghostInfo.PlayerPoint;
            _heroTransform = _ghostInfo.PlayerTransform;
           // _playerTransform = _ghostInfo.
        }

    }
}