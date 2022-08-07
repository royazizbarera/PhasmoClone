using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputSystem : MonoBehaviour, IService
{
    public Vector2 Axis { get; private set; }
    public Vector2 CameraAxis { get; private set; }
    public bool IsRunning = false;

    public Action PickUpItem, DropItem;


    private MainInputAction _mainInputAction;

    private void Awake()
    {
        _mainInputAction = new MainInputAction();
        _mainInputAction.Player.Enable();

        BindFuncs();
    }


    private void Update()
    {
        CameraAxis = _mainInputAction.Player.Camera.ReadValue<Vector2>();
        Axis = _mainInputAction.Player.Movement.ReadValue<Vector2>();
        IsRunning = _mainInputAction.Player.Run.ReadValue<float>() == 1 ? true : false;
    }

    private void BindFuncs()
    {
        _mainInputAction.Player.DropItem.performed += DropItemCallback;
        _mainInputAction.Player.Pickup.performed += PickUpItemCallBack;
    }

    private void DropItemCallback(InputAction.CallbackContext obj)
    {
        if(DropItem != null) DropItem.Invoke();
    } 
    
    private void PickUpItemCallBack(InputAction.CallbackContext obj)
    {
        if (PickUpItem != null) PickUpItem.Invoke();
    }

}
