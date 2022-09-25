using UnityEngine;
using Cinemachine;
using Infrastructure;
using Utilities.Constants;
using Items.Logic;
using Infrastructure.Services;

namespace Environment
{
    public class BoardClickable : MonoBehaviour, IClickable
    {
        [SerializeField] private CinemachineVirtualCamera _boardCamera;

        private InputSystem _inputSystem;
        private bool _isInBoard = false;

        private void Start()
        {
            _inputSystem = AllServices.Container.Single<InputSystem>();
        }
        public void OnClick()
        {
            if (!_isInBoard)
            {
                _boardCamera.Priority = CameraPriorities.ActiveState;
                _inputSystem.LockControl();
                _isInBoard = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }

        public void DecreasePriority()
        {
            _boardCamera.Priority = CameraPriorities.DisabledState;
            UnlockInputControl();
            _isInBoard = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void UnlockInputControl()
        {
            _inputSystem.UnLockControl();
        }
    }
}