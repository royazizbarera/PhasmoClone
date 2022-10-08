using Infrastructure;
using Infrastructure.States;
using Infrastructure.States.GameStates;
using System;
using System.Collections;
using UnityEngine;
using Utilities.Constants;

namespace Infrastructure.Services
{
    public class GameFlowService : IService
    {
        public bool Died = false;
        public bool IsGameEnded = false;

        public Action GameOverAction;
        public Action WinAction;

        private SceneLoader _sceneLoader;
        private GameStateMachine _gameStateMachine;
        private GameObjectivesService _gameObjectivesService;
        private ICoroutineRunner _coroutineRunner;

        private float[] _rewardValues = new float[6];

        private float[] _objectives = new float[3];
        private float _photoReward = 0f;
        private float _totalReward = 0f;
        private float _ghostReward = 30f;
        private float _insuranceReward = 0f;
        private float _deathCoef = 0.25f;
        private float _difficultyCoef = 1f;

        public GameFlowService(ICoroutineRunner coroutineRunner, GameObjectivesService gameObjectivesService)
        {
            _gameObjectivesService = gameObjectivesService;
            _coroutineRunner = coroutineRunner;

            GameOverAction += GameOverCall;
            WinAction += WinGameCall;
        }

        public void FirstTimeSetUp(SceneLoader sceneLoader, GameStateMachine gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        public GhostDataSO GhostDataSO
        {
            get { return _currentGhostSO; }
        }

        private const float DelayBeforeGoToLobby = 2.5f;

        private GhostInfo _currentGhostInfo;
        private GhostDataSO _currentGhostSO;
        private GhostDataSO _currentGhostChoosenSO;

        public void SetUpGameFlowService(GhostInfo currentLevelGhost)
        {
            _currentGhostInfo = currentLevelGhost;
            _currentGhostSO = _currentGhostInfo.GhostData;
        }

        public void ChangeCurrentChoosenGhost(GhostDataSO ghostChoosen)
        {
            _currentGhostChoosenSO = ghostChoosen;
        }

        public bool IsChooseCorrect()
        {
            if(_currentGhostChoosenSO != null)
            {
                if (_currentGhostChoosenSO.name == _currentGhostSO.name)
                {
                    return true;
                }
            }
            return false;
        }

        public float[] GetRewardValues()
        {
            if (IsChooseCorrect()) _rewardValues[0] = _ghostReward; //obj 1 - ghost
            else _rewardValues[0] = 0f;

            for (int i = 1; i < 4; i++)
            _rewardValues[i] = _gameObjectivesService.CurrObjectives[i-1].IsDone ? _gameObjectivesService.CurrObjectives[i - 1].ObjectiveReward : 0f; //objectives

            _rewardValues[4] = _photoReward; //photo
            _rewardValues[5] = _insuranceReward; //insurance

            return _rewardValues;
        }

        public float GetTotalRewardValue()
        {
            CalculateTotalReward();
            return _totalReward;
        }

        public void AddPhotoReward(float value)
        {
            _photoReward += value;
        }
        public void CalculateInsurance(float itemsCost)
        {
            if (Died) _insuranceReward = Mathf.Round(itemsCost / 2);
        }
        private void CalculateTotalReward()
        {
            for (int i = 0; i < _rewardValues.Length; i++) _totalReward += _rewardValues[i];
            if (Died) _totalReward = Mathf.Round((_totalReward - _insuranceReward) * _deathCoef * _difficultyCoef + _insuranceReward);
            else _totalReward = Mathf.Round((_totalReward - _insuranceReward) * _difficultyCoef + _insuranceReward);
        }   
        public void ClearRewards()
        {
            for (int i = 0; i < _rewardValues.Length; i++)
            {
                _rewardValues[i] = 0f;
            }
            for (int i = 0; i < _objectives.Length; i++)
            {
                _objectives[i] = 0f;
            }
            _photoReward = 0f;
            _totalReward = 0f;
            _insuranceReward = 0f;
        }

        private void GameOverCall()
        {
            Died = true;
            _coroutineRunner.StartCoroutine(EndGame());
        } 
        private void WinGameCall()
        {
            Died = false;
            _coroutineRunner.StartCoroutine(EndGame());
        }

        private IEnumerator EndGame()
        {
            IsGameEnded = true;
            yield return new WaitForSeconds(DelayBeforeGoToLobby);
            ActivateLobby();
        }

        private void ActivateLobby()
        {
            _sceneLoader.Load(SceneNames.LobbyScene, _gameStateMachine.Enter<LobbyState>);
        }
    }
}