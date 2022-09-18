using Infrastructure.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.States.GameStates
{
    public class GameFlowState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly LevelSetUp _levelSetUp;
        private readonly SceneLoader _sceneLoader;
        private readonly GameFlowService _gameFlowService;

        public GameFlowState(GameStateMachine stateMachine, LevelSetUp levelSetUp, SceneLoader sceneLoader, GameFlowService gameFlowService)
        {
            _stateMachine = stateMachine;
            _gameFlowService = gameFlowService;
            _levelSetUp = levelSetUp;
            _sceneLoader = sceneLoader;

            _gameFlowService.FirstTimeSetUp(sceneLoader, stateMachine);
        }
        public void Enter()
        {
            _gameFlowService.SetUpGameFlowService(_levelSetUp.GhostInfo);
        }

        public void Exit()
        {
            _levelSetUp.ResetLevel();
        }
    }
}