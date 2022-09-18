using UnityEngine;
using Infrastructure.States.GameStates;
using Infrastructure;
using Utilities.Constants;
using Environment;
using Infrastructure.Services;

namespace UI
{
    public class BoardMenu : MonoBehaviour
    {
        private InputSystem _inputSystem;

        [SerializeField] private GameObject _mainScreen;

        private GameObject _currentScreen;
        private GameObject _previousScreen;

        private void Start()
        {
            _inputSystem = AllServices.Container.Single<InputSystem>();

            LoadStartScreen();
        }

        public void OpenScreen(GameObject screen)
        {
            _currentScreen.SetActive(false);
            screen.SetActive(true);
            _previousScreen = _currentScreen;
            _currentScreen = screen;
        }
        public void BackToPreviousScreen()
        {
            _currentScreen.SetActive(false);
            _previousScreen.SetActive(true);

            GameObject t = _previousScreen;

            _previousScreen = _currentScreen;
            _currentScreen = t;

        }
        public void LoadLevel()
        {
            _inputSystem.UnLockControl();
            GameBootstrapper.Instance.StateMachine.Enter<LoadLevelState>();
        }

        private void LoadStartScreen()
        {
            _currentScreen = _mainScreen;
            _mainScreen.SetActive(true);
        }
    }
}