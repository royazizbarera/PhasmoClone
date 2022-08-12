using UnityEngine;
using Infrastructure.States.GameStates;
using Infrastructure;
using Utilities.Constants;
using Environment;

namespace UI
{
    public class BoardMenu : MonoBehaviour
    {
        private BoardClickable _board;

        private void Start()
        {
            _board = GetComponent<BoardClickable>();
        }
        public void LoadLevel()
        {
            SceneLoader sceneLoader = AllServices.Container.Single<SceneLoader>();
            if (sceneLoader != null)
            {
                _board.UnlockInputControl();
                sceneLoader.Load(SceneNames.LevelNames.Turkwood.ToString(), GameBootstrapper.Instance.StateMachine.Enter<LoadLevelState>);
            }
        }
    }
}