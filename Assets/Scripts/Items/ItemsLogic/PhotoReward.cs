using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoReward : MonoBehaviour
{
    [SerializeField] private float Value;
    [SerializeField] private string Name;

    private bool _isPhotographed = false;

    public string GetRewardName()
    {
        return Name;
    }
    public float GetRewardValue()
    {
        return Value;
    }
    public void Photograph()
    {
        _isPhotographed = true;
    }
    public bool CheckIfPhotographed()
    {
        return _isPhotographed;
    }
}
