using Infrastructure.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Constants;

namespace Infrastructure.States.GameStates
{
    public class LoadLevelState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly GameFactory _gameFactory;
        private readonly LevelSetUp _levelSetUp;
        private readonly SceneLoader _sceneLoader;
        private GameStateMachine gameStateMachine;

        public LoadLevelState(GameStateMachine stateMachine, GameFactory gameFactory, LevelSetUp levelSetUp, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _levelSetUp = levelSetUp;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load(SceneNames.LevelNames.Turkwood.ToString(), InitGameWorld);
        }

        public void Exit()
        {
        }


        private void InitGameWorld()
        {
            _levelSetUp.InitializeLevel();
            InstantiateAll();

            _stateMachine.Enter<GameFlowState>();
        }

        private void InstantiateAll()
        {
            GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(Tags.InitialPoint));
            GameObject ghost = _gameFactory.CreateGhost(GameObject.FindWithTag(Tags.GhostInitialPoint));
            ghost.GetComponent<GhostInfo>().SetUpGhost(hero.transform);
        }
    }
}