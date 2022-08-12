using Infrastructure.AssetsProvider;
using Infrastructure.Factory;
using Managers.Services;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootState : IState
    {
        private readonly AllServices _services;
        private readonly ICoroutineRunner _coroutineRunner;
        public BootState(AllServices services, ICoroutineRunner coroutineRunner)
        {
            _services = services;
            _coroutineRunner = coroutineRunner;
            RegisterServices();
        }


        public void Enter()
        {
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<AssetProvider>(new AssetProvider());
            _services.RegisterSingle<SceneLoader>(new SceneLoader(_coroutineRunner));
            _services.RegisterSingle<GameFactory>(new GameFactory(_services.Single<AssetProvider>()));
            _services.RegisterSingle<InputSystem>(_services.Single<GameFactory>().CreateInputSystem().GetComponent<InputSystem>());
        }
    }
}