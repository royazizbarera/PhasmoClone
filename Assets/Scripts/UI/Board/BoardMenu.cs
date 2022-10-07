using UnityEngine;
using Infrastructure.States.GameStates;
using Infrastructure;
using Utilities.Constants;
using Environment;
using Infrastructure.Services;
using Cinemachine;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class BoardMenu : MonoBehaviour
    {
        [SerializeField] 
        private CinemachineVirtualCamera _boardCamera;

        [SerializeField] 
        private GameObject _mainScreen;
        [SerializeField] 
        private GameObject _levelResultScreen;

        [SerializeField]
        private LevelResultsScreen _levelResults;

        [SerializeField]
        private TextMeshProUGUI _levelNameTXT;
        [SerializeField]
        private Button _playButton;
        [SerializeField]
        private TextMeshProUGUI _playButtonTXT;

        private GameObject _currentScreen;
        private GameObject _previousScreen;

        private InputSystem _inputSystem;
        private LevelSetUp _levelSetUp;

        private bool _isStarted = false;

        private void Start()
        {
            _inputSystem = AllServices.Container.Single<InputSystem>();
            _levelSetUp = AllServices.Container.Single<LevelSetUp>();

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
            if (!_isStarted)
            {
                _isStarted = true;
                _inputSystem.UnLockControl();
                GameBootstrapper.Instance.StateMachine.Enter<LoadLevelState>();
            }
        }

        public void ChooseMap(GetLevelName levelName)
        {
            _levelSetUp.ChooseMap(levelName.LevelName);
            _levelNameTXT.text = "Contract: " + levelName.LevelName.ToString();
            _playButton.interactable = true;
            _playButtonTXT.alpha = 255f;
        }

        private void LoadStartScreen()
        {
            if (AllServices.Container.Single<GameFlowService>().IsGameEnded == true)
            {
                if (_mainScreen.activeInHierarchy) _mainScreen.SetActive(false);

                LookAtBoard();
                _currentScreen = _levelResultScreen;

                _levelResults.LoadResults();

                _levelResultScreen.SetActive(true);
            }
            else
            {
                _currentScreen = _mainScreen;
                _mainScreen.SetActive(true);
            }
        }

        private void LookAtBoard()
        {
            _boardCamera.Priority = CameraPriorities.ActiveState;
            _inputSystem.LockControl();
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}