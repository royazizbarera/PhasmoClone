using System;
using UnityEngine;

public class GhostInfo : MonoBehaviour
{
    public GhostDataSO GhostData;
    public Transform PlayerTransform;

    public Action GhostSetedUp;
    public void SetUpGhost(Transform mainHero)
    {
        PlayerTransform = mainHero;
        GhostSetedUp?.Invoke();
    }
}
