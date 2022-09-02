using Managers.Services;
using Infrastructure;
using UnityEngine;
using System;

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

        [SerializeField] private float _maxSprintDuration = 3.5f;
        [SerializeField] private float _sprintCD = 5f;
        private float _sprintRestMultiplayer;
        private float _sprintRestWhileSprintingMultiplayer;
        private float _currSprintDuration = 0f;

        private float _curSpeedMultiplier = 1f;
        private float _mouseX, _mouseY;
        private Vector2 mouseDelta;
        private float _xRotation = 0f;

        private const float Epsilon = 0.01f;
        private const float SprintCDDivider = 5f;
        private const float MinTimeForRest = 2f;
        private InputSystem _inputSystem;

        private float _timeFromLustSprint = 0f;
        private Transform cameraTransform;
        private float _xMove, _zMove;
        private bool _isSprintPressed;
        private bool _sprint = false;

        private bool _isSprintLocked = false;

        private void Start()
        {
            _inputSystem = AllServices.Container.Single<InputSystem>();
            cameraTransform = Camera.main.transform;
            _sprintRestMultiplayer = (_maxSprintDuration / _sprintCD);
        }

        void Update()
        {
            InputMouseMove();
            InputMove();
            SprintCalc();

            PlayerRotation();
            PlayerMovement();
        }

        private void InputMove()
        {
            _xMove = _inputSystem.Axis.x;
            _zMove = _inputSystem.Axis.y;
            _isSprintPressed = _inputSystem.IsRunning;
        }

        private void InputMouseMove()
        {
            mouseDelta = _inputSystem.CameraAxis;
            _mouseX = mouseDelta.x * _mouseSensitivity * Time.deltaTime;
            _mouseY = mouseDelta.y * _mouseSensitivity * Time.deltaTime;
        }

        private void SprintCalc()
        {
            if (_isSprintPressed)
            {
                if (_isSprintLocked)
                {
                    MinusCurrSprintDuration();
                    if (_currSprintDuration <= Epsilon) _isSprintLocked = false;
                }
                else
                {
                    _currSprintDuration += Time.deltaTime;
                    if(_currSprintDuration >= _maxSprintDuration) _isSprintLocked = true;
                }
                _sprint = !_isSprintLocked;
                if (_sprint) _timeFromLustSprint = 0f;
            }
            else
            {
                _sprint = false;
                if (_currSprintDuration > Epsilon) MinusCurrSprintDuration();
                else _isSprintLocked = false;
            }
            _timeFromLustSprint += Time.deltaTime;
        }


        private void MinusCurrSprintDuration()
        {
            _sprintRestWhileSprintingMultiplayer = _timeFromLustSprint >= MinTimeForRest ? 1f : _timeFromLustSprint / SprintCDDivider;
            if (_isSprintLocked) _currSprintDuration -= Time.deltaTime * _sprintRestMultiplayer;
            else _currSprintDuration -= Time.deltaTime * _sprintRestWhileSprintingMultiplayer;
            _currSprintDuration = Mathf.Max(0, _currSprintDuration);
        }
        

        private Vector3 VectorToForward(Vector3 cameraTransform)
        {
            Vector3 returnVector;
            returnVector.x = -cameraTransform.z;
            returnVector.y = 0;
            returnVector.z = cameraTransform.x;
            return returnVector;
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
            Vector3 move = CalculateMove(_sprint);
            _charController.Move(move * _maxMoveSpeed * _curSpeedMultiplier * Time.deltaTime);
        }


        private Vector3 CalculateMove(bool sprint)
        {
            if (sprint) _curSpeedMultiplier = _sprintMultiplier;

            else _curSpeedMultiplier = 1f;

            Vector3 result = transform.right * _xMove + transform.forward * _zMove;
            return result;
        }
    }
}