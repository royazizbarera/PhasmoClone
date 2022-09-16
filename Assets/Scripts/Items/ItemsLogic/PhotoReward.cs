using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoReward : MonoBehaviour
{
    [SerializeField] private float Value;
    [SerializeField] private string Name;

    public string GetRewardName()
    {
        return Name;
    }
    public float GetRewardValue()
    {
        return Value;
    }
}
