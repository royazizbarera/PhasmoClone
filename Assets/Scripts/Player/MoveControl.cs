using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float _speed = 12f;
    [SerializeField] private float _sprintMultiplier;

    private float _curSpeedMultiplier = 1f;
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool sprint = Input.GetKey(KeyCode.LeftShift);

        if (sprint) _curSpeedMultiplier = _sprintMultiplier;
        else _curSpeedMultiplier = 1f;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * _speed * _curSpeedMultiplier * Time.deltaTime);
    }
}
