using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputSystem : MonoBehaviour, IService
{
    public Vector2 Axis { get; private set; }
    public bool IsRunning = false;

    public Action dropItemAction;


    private MainInputAction _mainInputAction;

    private void Awake()
    {
        _mainInputAction = new MainInputAction();
        _mainInputAction.Player.Enable();

        BindFuncs();
    }


    private void Update()
    {
        Axis = _mainInputAction.Player.Movement.ReadValue<Vector2>();
        IsRunning = _mainInputAction.Player.Run.ReadValue<float>() == 1 ? true : false;
    }

    private void BindFuncs()
    {
        _mainInputAction.Player.DropItem.performed += DropItemCallback;
    }

    private void DropItemCallback(InputAction.CallbackContext obj)
    {
        dropItemAction.Invoke();
    }

}
