using Managers.Services;
using Infrastructure;
using UnityEngine;

namespace Player.Movement
{
    public class MoveControl : MonoBehaviour
    {
        [SerializeField] private CharacterController _charController;

        [SerializeField] private Transform _playerBody;
        [SerializeField] private Transform _playerHead;
        [SerializeField] private float _mouseSensitivity = 100f;

        [SerializeField] private float _maxMoveSpeed = 12f;
        [SerializeField] private float _sprintMultiplier;

        private float _curSpeedMultiplier = 1f;
        private float _mouseX, _mouseY;
        private Vector2 mouseDelta;
        private float _xRotation = 0f;

        private InputSystem _inputSystem;

        private Transform cameraTransform;

        private float _xMove, _zMove;
        private bool _isSprinting;


        private void Awake()
        {
            
        }
        private void Start()
        {
            _inputSystem = AllServices.Container.Single<InputSystem>();
            cameraTransform = Camera.main.transform;
        }

        void Update()
        {
            InputMouseMove();
            InputMove();

            PlayerRotation();
            PlayerMovement();
        }

        private void InputMove()
        {
            _xMove = _inputSystem.Axis.x;
            _zMove = _inputSystem.Axis.y;
            _isSprinting = _inputSystem.IsRunning;
        }

        private void InputMouseMove()
        {
            mouseDelta = _inputSystem.CameraAxis;
            _mouseX = mouseDelta.x * _mouseSensitivity * Time.deltaTime;
            _mouseY = mouseDelta.y * _mouseSensitivity * Time.deltaTime;
        }

        private Vector3 CalculateMove(bool sprint)
        {
            if (sprint) _curSpeedMultiplier = _sprintMultiplier;

            else _curSpeedMultiplier = 1f;

            Vector3 result = transform.right * _xMove + transform.forward * _zMove;
            result = cameraTransform.right * result.x + cameraTransform.forward * result.z;
            return result;
        }

        private void PlayerRotation()
        {
            _playerBody.Rotate(Vector3.up * _mouseX);

            _xRotation -= _mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            _playerHead.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }

        private void PlayerMovement()
        {
            Vector3 move = CalculateMove(_isSprinting);
            _charController.Move(move * _maxMoveSpeed * _curSpeedMultiplier * Time.deltaTime);
        }
    }
}