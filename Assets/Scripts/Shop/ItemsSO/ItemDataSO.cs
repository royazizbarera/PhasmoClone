using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop", menuName = "ScriptableObjects/ItemData", order = 2)]
public class ItemDataSO : ScriptableObject
{
    public string Name = "item name";
    public string Description = "item description";
    public float Price = 10f;
    public float MaxAmount = 2f;
}