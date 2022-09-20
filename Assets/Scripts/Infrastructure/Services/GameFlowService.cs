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
        private ICoroutineRunner _coroutineRunner;

        private float[] _rewardValues = new float[7];

        private float[] _objectives = new float[4];
        private float _photoReward = 0f;
        private float _totalReward = 0f;
        private float _ghostReward = 30f;

        public GameFlowService(ICoroutineRunner coroutineRunner)
        {
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
                return true;
            }
            return false;
        }

        public float[] GetRewardValues()
        {
            _rewardValues[0] = _objectives[0]; //obj 1
            _rewardValues[1] = _objectives[1]; //obj 2
            _rewardValues[2] = _objectives[2]; //obj 3
            _rewardValues[3] = _objectives[3]; //obj 4
            _rewardValues[4] = _photoReward; //photo

            _rewardValues[5] = 0f; //insurance

            if (CheckForCorrectGhost()) _rewardValues[6] = _ghostReward; //ghost
            else _rewardValues[6] = 0f;

            return _rewardValues;
        }
        public void AddPhotoReward(float value)
        {
            _photoReward += value;
        }
        public float GetTotalRewardValue()
        {
            CalculateTotalReward();
            return _totalReward;
        }
        private void CalculateTotalReward()
        {
            for (int i = 0; i < _rewardValues.Length; i++) _totalReward += _rewardValues[i];
        }
        private bool CheckForCorrectGhost()
        {
            return true;
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