using System;
using UnityEngine;

public class GhostInfo : MonoBehaviour
{
    public GhostDataSO GhostData;
    public Transform PlayerPoint;

    public Action GhostSetedUp;
    public void SetUpGhost(Transform playerTransformPoint)
    {
        PlayerPoint = playerTransformPoint;
        GhostSetedUp?.Invoke();
    }
}
