using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Board : MonoBehaviour, IClickable
{
    [SerializeField] private CinemachineVirtualCamera _boardCamera;
    public void OnClick()
    {
        _boardCamera.Priority = CameraPriorities.ActiveState;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void DecreasePriority()
    {
        _boardCamera.Priority = CameraPriorities.DisabledState;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
