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

        public GameFlowState(GameStateMachine stateMachine, LevelSetUp levelSetUp, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _levelSetUp = levelSetUp;
            _sceneLoader = sceneLoader;
        }
        public void Enter()
        {

        }

        public void Exit()
        {
            _levelSetUp.ResetLevel();
        }
    }
}