using System;
using UnityEngine;
using Utilities.Constants;

namespace Infrastructure.Services
{
    public class GameFactory : IService
    {
        private GameObject _mainHero = null;
        private GameObject _ghost = null;
        private readonly AssetProvider _assets;

        public GameFactory(AssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(GameObject at)
        {
            GameObject gameObject = _assets.Instantiate(AssetPath.HeroPath, at.transform.position);
            _mainHero = gameObject;
            return gameObject;
        }

        public GameObject CreateGhost(GameObject at)
        {
            GameObject gameObject = _assets.Instantiate(AssetPath.GhostPath, at.transform.position);
            return gameObject;
        }
        public GameObject CreateInputSystem()
        {
            GameObject gameObject = _assets.Instantiate(AssetPath.InputSystemPath);
            return gameObject;
        }

        public GameObject CreateTargetUI()
        {
            GameObject gameObject = _assets.Instantiate(AssetPath.TargetUIPath);
           
            return gameObject;
        }

        public GameObject GetMainHero()
        {
            return _mainHero;
        }

    }
}