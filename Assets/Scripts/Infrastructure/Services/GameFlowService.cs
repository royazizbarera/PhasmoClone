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
            float[] rewardValues = new float[7];

            for (int i = 0; i < rewardValues.Length; i++)
            {
                rewardValues[i] = 10f + i;
            }

            return rewardValues;
        }
        public float GetTotalRewardValue()
        {
            return 100f;
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