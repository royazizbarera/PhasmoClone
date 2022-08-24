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

        private Transform _playerTransform;
        private PlayerCheckResult _playerCheckResult;

        private bool _dataSetedUp = false;
        private float _ghostAttackSpeed;
        private Transform[] _patrolPoints;
        private LevelSetUp _levelSetUp;

        private Transform _currDestination = null;
        
        private int _randomPointNum;
        private bool _isAttacking = false;
        private bool _isFollowing = false;
        private float _maxAggroDistance;

        private void OnEnable()
        {
            if (!_dataSetedUp) SetUpGhostData();
            StartAttackPatrolling();
        }

        private void OnDisable()
        {
            StopAttackPatrolling();
        }

        private void OnDestroy()
        {
            _ghostInfo.GhostSetedUp -= SetUpPlayerTranform;
        }
        private void Update()
        {
            if (!_isAttacking) return;
            if (_isFollowing)
            {
                _currDestination = _playerTransform;
                SetDestination();
                return;
            }
            if (_agent.remainingDistance <= _stoppingDistance || _currDestination == null)
            {
                ChoosePoint();
                SetDestination();
            }
        }


        public void StartAttackPatrolling()
        {
            _isFollowing = false;
            _agent.speed = _ghostAttackSpeed;
            SwitchAttackState(true);
            StartCoroutine(CheckForPlayerVisible());
        }

        public void SwitchAttackState(bool isAttacking)
        {
            _isAttacking = isAttacking;
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
                _playerCheckResult = _lineOfSight.CheckForPlayer(_playerTransform);
                if (_playerCheckResult.IsPlayerVisible && _playerCheckResult.DistanceToPlayer <= _maxAggroDistance)
                {              
                    _isFollowing = true;
                }
                else _isFollowing = false;
                yield return new WaitForSeconds(_checkForLineCD);
            }
            yield return null;
        }

        private void StopAttackPatrolling()
        {
            SwitchAttackState(false);
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
            _ghostAttackSpeed = _ghostInfo.GhostData.GhostAttackSpeed;
            _maxAggroDistance = _ghostInfo.GhostData.MaxDistanceToPlayerAggre;
            _patrolPoints = _levelSetUp.GetGhostPatrolPoints();
            if (_ghostInfo.PlayerTransform != null) _playerTransform = _ghostInfo.PlayerTransform;
            else
            {
                _ghostInfo.GhostSetedUp += SetUpPlayerTranform;
            }
            _dataSetedUp = true;
        }
        private void SetUpPlayerTranform()
        {
            _playerTransform = _ghostInfo.PlayerTransform;
        }

    }
}