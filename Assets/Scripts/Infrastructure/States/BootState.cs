using Infrastructure.AssetsProvider;
using Infrastructure.Factory;
using Managers.Services;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootState : IState
    {
        private readonly AllServices _services;
        private readonly InputSystem _inputSystem;
        public BootState(AllServices services, InputSystem inputSystem)
        {
            _services = services;
            _inputSystem = inputSystem;

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
            _services.RegisterSingle<InputSystem>(_inputSystem);
            _services.RegisterSingle<AssetProvider>(new AssetProvider());
            _services.RegisterSingle<GameFactory>(new GameFactory(_services.Single<AssetProvider>()));
        }
    }
}