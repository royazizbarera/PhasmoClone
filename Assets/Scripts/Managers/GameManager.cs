using UnityEngine.SceneManagement;
using UnityEngine;
using Infrastructure.States;
using Utilities;
using Infrastructure;
using Managers.Services;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GameStateMachine StateMachine;

        [SerializeField]
        private InputSystem _input;
        private void Awake() //boot
        {
            StateMachine = new GameStateMachine(new AllServices(), _input);
            StateMachine.Enter<BootState>();
        }
        public AsyncOperation StartLobby()
        {
            return LoadLevel("Lobby");
        }


        public AsyncOperation LoadLevel(string levelName)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
            if (ao == null)
            {
                Debug.LogError("[GameManager] Unable to load level " + levelName);
                return null;
            }
            return ao;

        }

        public AsyncOperation UnloadLevel(string levelName)
        {
            AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);

            if (ao == null)
            {
                Debug.LogError("[GameManager] Unable to unload level " + levelName);
                return null;
            }
            return ao;
        }

    }
}