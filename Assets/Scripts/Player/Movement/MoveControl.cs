using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float _speed = 12f;
    [SerializeField] private float _sprintMultiplier;

    private float _curSpeedMultiplier = 1f;

    private InputSystem _inputSystem;
    [SerializeField]private Transform cameraTransform;

    private float _xMove, _zMove;
    private bool _isSprinting;

    private void Awake()
    {
        _inputSystem = AllServices.Container.Single<InputSystem>();
        //cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        InputMove();
        Vector3 move = CalculateMove(_isSprinting);
        controller.Move(move * _speed * _curSpeedMultiplier * Time.deltaTime);
    }

    private void InputMove()
    {
        _xMove = _inputSystem.Axis.x;
        _zMove = _inputSystem.Axis.y;
        _isSprinting = _inputSystem.IsRunning;
    }

    private Vector3 CalculateMove(bool sprint)
    {
        if (sprint) _curSpeedMultiplier = _sprintMultiplier;

        else _curSpeedMultiplier = 1f;

        Vector3 result = transform.right * _xMove + transform.forward * _zMove;
        result = cameraTransform.forward * result.x + cameraTransform.right * -result.z;
        return result;
    }
}
