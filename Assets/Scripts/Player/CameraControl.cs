using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float _sensitivity = 100f;

    private Transform playerBody;
    private float _xRotation = 0f;

    private void Start()
    {
        playerBody = this.transform;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

        HorizontalRotate(mouseX);
        VerticalRotate(mouseY);
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
