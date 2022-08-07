using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public IPickupable MainItem { get; private set;}


    [SerializeField]
    private int _maxItemAmmount;



    private List<IPickupable> _pickupableSlots;
    private int _currItemAmmount;

    private int currMainItemSlot = 0;


    private void Start()
    {
        ResizeSlots();
        MainItem = null;
    }


    public bool AddItem(IPickupable itemToAdd)
    {
        if (_currItemAmmount < _maxItemAmmount)
        {
            for (int slotNum = 0; slotNum < _maxItemAmmount; slotNum++)
            {
                if (_pickupableSlots[slotNum] == null)
                {
                    _pickupableSlots[slotNum] = itemToAdd;
                    _currItemAmmount++;
                    if(MainItem == null)
                    {
                        ChangeMainItem(slotNum);
                    }
                    return true;
                }
            }
            return false;
        }
        else return false;
    }


    public void ChangeMainItem(int slotNum)
    {
        currMainItemSlot = slotNum;
        MainItem = _pickupableSlots[slotNum];
    }

    public void AddCurrItemSlot()
    {
        currMainItemSlot++;
        if (currMainItemSlot >= _maxItemAmmount) currMainItemSlot = 0;

        MainItem = _pickupableSlots[currMainItemSlot];
    }

    public void DropItem()
    {
        if(MainItem != null)
        {
            _pickupableSlots[currMainItemSlot] = null;
            MainItem = null;
        }
    }


    private void ResizeSlots()
    {
        for (int i = 0; i < _maxItemAmmount; i++)
        {
            _pickupableSlots.Add(null);
        }
    }
}
