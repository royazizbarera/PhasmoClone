using Infrastructure.AssetsProvider;
using UnityEngine;
using Utilities.Constants;

namespace Infrastructure.Factory
{
    public class GameFactory :  IService
    {
        private readonly AssetProvider _assets;

        public GameFactory(AssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(GameObject at)
        {
            GameObject gameObject = _assets.Instantiate(AssetPath.HeroPath, at.transform.position);
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
     
        //public void CreateHud() =>
        //  InstantiateRegistered(AssetPath.HudPath);

        //private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        //{
        //    GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);

        //    RegisterProgressWatchers(gameObject);
        //    return gameObject;
        //}

        //private GameObject InstantiateRegistered(string prefabPath)
        //{
        //    GameObject gameObject = _assets.Instantiate(path: prefabPath);

        //    RegisterProgressWatchers(gameObject);
        //    return gameObject;
        //}

    }
}