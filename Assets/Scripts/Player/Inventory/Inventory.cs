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
        
        _pickupableSlots.Capacity = _itemAmmount;
        Debug.Log(_pickupableSlots.Count);
        Debug.Log(_pickupableSlots.Capacity);
    }
}
