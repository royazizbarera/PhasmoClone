using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputSystem : MonoBehaviour, IService
{

    public bool IsControlLocked = false;
    public Vector2 Axis { get; private set; }
    public Vector2 CameraAxis { get; private set; }
    public bool IsRunning = false;

    public Action PickUpItemAction, DropItemAction, MainUseAction, PrimaryUseAction, SecondaryUseAction , SwitchItemAction;


    private MainInputAction _mainInputAction;

    private void Awake()
    {
        _mainInputAction = new MainInputAction();
        _mainInputAction.Player.Enable();

        BindFuncs();
    }


    private void Update()
    {
        ReadControl();
    }


    private void ReadControl()
    {
        if (!IsControlLocked)
        {
            CameraAxis = _mainInputAction.Player.Camera.ReadValue<Vector2>();
            Axis = _mainInputAction.Player.Movement.ReadValue<Vector2>();
            IsRunning = _mainInputAction.Player.Run.ReadValue<float>() == 1 ? true : false;
        }
    }

    private void LockControl()
    {
        IsControlLocked = true;
    } 
    
    private void UnLockControl()
    {
        IsControlLocked = false;
    }
    private void BindFuncs()
    {
        _mainInputAction.Player.DropItem.performed += DropItemCallback;
        _mainInputAction.Player.Pickup.performed += PickUpItemCallBack;
        _mainInputAction.Player.MainUse.performed += MainUseCallBack;
        _mainInputAction.Player.PrimaryUse.performed += PrimaryUseCallBack;
        _mainInputAction.Player.SecondaryUse.performed += SecondaryUseCallBack;
        _mainInputAction.Player.SwitchItem.performed += SwitchItemUseCallBack;
    }

    private void DropItemCallback(InputAction.CallbackContext obj)
    {
        if(DropItemAction != null) DropItemAction.Invoke();
    } 
    
    private void PickUpItemCallBack(InputAction.CallbackContext obj)
    {
        if (PickUpItemAction != null) PickUpItemAction.Invoke();
    }

    private void MainUseCallBack(InputAction.CallbackContext obj)
    {
        if (MainUseAction != null) MainUseAction.Invoke();
    } 
    
    private void PrimaryUseCallBack(InputAction.CallbackContext obj)
    {
        if (PrimaryUseAction != null) PrimaryUseAction.Invoke();
    } 
    
    private void SecondaryUseCallBack(InputAction.CallbackContext obj)
    {
        if (SecondaryUseAction != null) SecondaryUseAction.Invoke();
    } 
    
    private void SwitchItemUseCallBack(InputAction.CallbackContext obj)
    {
        if (SwitchItemAction != null) SwitchItemAction.Invoke();
    }
}
