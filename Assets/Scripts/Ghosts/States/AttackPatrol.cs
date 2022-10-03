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

        private const float SmudgeEffectTime = 5f;
        private bool _playerKilled = false;
        private GameFlowService _gameFlow;

        private Transform _playerPoint;
        private Transform _heroTransform;
        private PlayerCheckResult _playerCheckResult;
        private AudioControl _audioControl;

        private WaitForSeconds _checkForLineWait;

        private bool _isGhostDisabled = false;

        private bool _dataSetedUp = false;
        private float _ghostAttackSpeed;
        private float _distanceToPlayer = 0f;

        private bool _underSmudgeEffect = false;
        private Transform[] _patrolPoints;
        private LevelSetUp _levelSetUp;

        private Transform _currDestination = null;

        private int _randomPointNum;
        private bool _isAttacking = false;
        private bool _isFollowing = false;
        private float _maxAggroDistance;
        private float _heartBeatVolumePercent = 0f;

        private Vector3 _playerPointDistance;
        private Vector3 _ghostPointDistance;

        private const float MaxVolumeDistanceHeartBeat = 2f;
        private const float MinVolumeDistanceHeartBeat = 7f;

        private const float DistanceToKill = 1f;
        private const float DisableTime = 3f;
        private void OnEnable()
        {
            if (!_dataSetedUp) SetUpGhostData();
        }
        private void OnDestroy()
        {
            _ghostInfo.GhostSetedUp -= SetUpParams;
        }
        private void Update()
        {
            if (!_isAttacking) return;
            if (_isGhostDisabled) return;

            CheckDistanceToPlayer();
            SetHeartBeatVolume();
            if (_isFollowing && !_underSmudgeEffect)
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

        public void StartAttackPatrolling()
        {
            _isGhostDisabled = true;
            _isFollowing = false;
            _agent.speed = _ghostAttackSpeed;
            SwitchAttackState(true);
            StartCoroutine(nameof(CheckForPlayerVisible));
            Invoke(nameof(EnableAttackAfterCD), DisableTime);
        }

        public void StopAttackPatrolling()
        {
            SwitchAttackState(false);
            StopCoroutine(nameof(CheckForPlayerVisible));
        }


        public void SwitchAttackState(bool isAttacking)
        {
            _isAttacking = isAttacking;
            if (!_agent.isOnNavMesh) return;
            if (isAttacking)
            {
                _audioControl.StartHeartBeat();
                _agent.isStopped = false;
            }
            else
            {
                _audioControl.StopHeartBeat();
                _agent.ResetPath();
                _agent.isStopped = true;
            }
        }

        public void SmudgeEffect()
        {
            _underSmudgeEffect = true;
            Invoke(nameof(StopSmudgeEffect), SmudgeEffectTime);
        }

        private void StopSmudgeEffect()
        {
            _underSmudgeEffect = false;
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

        private void CheckDistanceToPlayer()
        {
            _distanceToPlayer = Vector3.Distance(_playerPointDistance, _ghostPointDistance);
        }

        private void SetHeartBeatVolume()
        {
            // Calculate heart beat volume percent, where if _distanceToPlayer <= MaxVolumeDistanceHeartBeat => 100%, and if _distanceToPlayer >= MinVolumeDistanceHeartBeat => 0%
            _heartBeatVolumePercent = ((_distanceToPlayer - MaxVolumeDistanceHeartBeat) * 100) / (MinVolumeDistanceHeartBeat - MaxVolumeDistanceHeartBeat);
            _heartBeatVolumePercent = 100f - _heartBeatVolumePercent;
            _heartBeatVolumePercent = Mathf.Clamp(_heartBeatVolumePercent, 0f, 100f);

            _audioControl.SetHeartBeatVolume(_heartBeatVolumePercent);
        }


        private void CheckForKill()
        {
            if (_underSmudgeEffect) return;
            _playerPointDistance = _heroTransform.position;
            _ghostPointDistance = transform.position;

            if ((_distanceToPlayer < DistanceToKill) && !_playerKilled)
            {
                _gameFlow.GameOverAction?.Invoke();
                _playerKilled = true;
            }
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
            _gameFlow = AllServices.Container.Single<GameFlowService>();

            _checkForLineWait = new WaitForSeconds(_checkForLineCD);
            _patrolPoints = _levelSetUp.GetGhostPatrolPoints();
            if (_ghostInfo.SetedUp) SetUpParams();
            else
            {
                _ghostInfo.GhostSetedUp += SetUpParams;
            }
            _dataSetedUp = true;
        }
        private void SetUpParams()
        {
            _audioControl = _ghostInfo.MainHero.GetComponent<AudioControl>();

            _playerPoint = _ghostInfo.PlayerPoint;
            _heroTransform = _ghostInfo.MainHero.transform;

            _ghostAttackSpeed = _ghostInfo.GhostData.GhostAttackSpeed;
            _maxAggroDistance = _ghostInfo.GhostData.MaxDistanceToPlayerAggr;
            // _playerTransform = _ghostInfo.
        }

    }
}