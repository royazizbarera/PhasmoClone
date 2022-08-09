using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Board : MonoBehaviour, IClickable
{
    [SerializeField] private CinemachineVirtualCamera _boardCamera;

    private InputSystem _inputSystem;

    private void Start()
    {
        _inputSystem = AllServices.Container.Single<InputSystem>();
    }
    public void OnClick()
    {
        _boardCamera.Priority = CameraPriorities.ActiveState;
        _inputSystem.LockControl();
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void DecreasePriority()
    {
        _boardCamera.Priority = CameraPriorities.DisabledState;
        _inputSystem.UnLockControl();
        Cursor.lockState = CursorLockMode.Locked;
    }
}
