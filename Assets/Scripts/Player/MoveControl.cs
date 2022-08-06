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

    private float _xMove, _zMove;
    private bool _isSprinting;

    private void Awake()
    {
        //_inputSystem = AllServices.Container.Single<InputSystem>();
    }
    void Update()
    {
        _xMove = Input.GetAxis("Horizontal");
        _zMove = Input.GetAxis("Vertical");
        bool sprint = Input.GetKey(KeyCode.LeftShift);

        Vector3 move = CalculateMove(sprint);

        controller.Move(move * _speed * _curSpeedMultiplier * Time.deltaTime);
    }


    private Vector3 CalculateMove(bool sprint)
    {
        if (sprint) _curSpeedMultiplier = _sprintMultiplier;

        else _curSpeedMultiplier = 1f;

        Vector3 result = transform.right * _xMove + transform.forward * _zMove;
        return result;
    }



}
