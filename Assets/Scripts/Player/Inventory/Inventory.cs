using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int _itemAmmount;

    private List<IPickupable> _pickupableSlots;

    private void Start()
    {
        _pickupableSlots = new List<IPickupable>(_itemAmmount);
        Debug.Log("Count = " + _pickupableSlots.Count);
        Debug.Log("Capacity = " + _pickupableSlots.Capacity);
    }


   // public void AddItem
}
