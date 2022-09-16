using Infrastructure.Services;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.States.GameStates
{
    public class BootState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly AllServices _services;
        private readonly ICoroutineRunner _coroutineRunner;
        private DataSaveLoader _dataSaveLoader;
        public BootState(AllServices services, ICoroutineRunner coroutineRunner, GameStateMachine gameStateMachine)
        {
            _services = services;
            _coroutineRunner = coroutineRunner;
            _gameStateMachine = gameStateMachine;
            RegisterServices();

            LoadGameInfo();
        }

        public void Enter()
        {
            CheckLobbyScene();
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<LevelSetUp>(new LevelSetUp());
            _services.RegisterSingle<GameFlowService>(new GameFlowService());
            _services.RegisterSingle<AssetProvider>(new AssetProvider());
            _services.RegisterSingle<DataSaveLoader>(new DataSaveLoader());
            _services.RegisterSingle<SceneLoader>(new SceneLoader(_coroutineRunner));
            _services.RegisterSingle<GameFactory>(new GameFactory(_services.Single<AssetProvider>()));
            _services.RegisterSingle<InputSystem>(_services.Single<GameFactory>().CreateInputSystem().GetComponent<InputSystem>());      
        }
        private void LoadGameInfo()
        {
            _dataSaveLoader = _services.Single<DataSaveLoader>();
            _dataSaveLoader.LoadInfo();
        }

        private void CheckLobbyScene()
        {
            //_gameStateMachine.Enter<LobbyState>();
            if (SceneManager.GetActiveScene().name == "Lobby")
            {
                _gameStateMachine.Enter<LobbyState>();
            }
        }
    }
}