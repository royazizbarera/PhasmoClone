using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName = "ScriptableObjects/Difficulty", order = 1)]
public class DifficultySO : ScriptableObject
{
    public float GracePeriod;
    public float SanityModifier;

    public float HuntDurationModifier;

    public float DifficultyRewardModifier;
    [TextArea(5, 10)]
    public string DifficultyDescription;

}