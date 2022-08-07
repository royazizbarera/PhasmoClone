using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float _sensitivity = 100f;

    private InputSystem _inputSystem;

    private Transform playerBody;
    private float mouseX, mouseY;
    private Vector2 mouseDelta;
    private float _xRotation = 0f;

    private void Start()
    {
        playerBody = this.transform;
        _inputSystem = AllServices.Container.Single<InputSystem>();
    }

    private void Update()
    {
        InputMouseMove();
        HorizontalRotate(mouseX);
        VerticalRotate(mouseY);
    }

    private void InputMouseMove()
    {
        mouseDelta = _inputSystem.CameraAxis;
        mouseX = mouseDelta.x * _sensitivity * Time.deltaTime;
        mouseY = mouseDelta.y * _sensitivity * Time.deltaTime;
    }

    private void HorizontalRotate(float angle)
    {
        playerBody.Rotate(Vector3.up * angle);
    }
    private void VerticalRotate(float angle)
    {
        _xRotation -= angle;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }
}
