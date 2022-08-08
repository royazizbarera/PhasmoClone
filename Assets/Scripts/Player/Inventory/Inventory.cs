using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public IPickupable MainItem { get; private set;}


    [SerializeField]
    private Transform _slot;

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
                    PickItem(itemToAdd);
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

    private void PickItem(IPickupable item)
    {
        Rigidbody itemRB = item.gameObject.GetComponent<Rigidbody>();
        itemRB.isKinematic = true;
        itemRB.velocity = Vector3.zero;
        itemRB.angularVelocity = Vector3.zero;

        // Set Slot as a parent
        item.gameObject.transform.SetParent(_slot);

        // Reset position and rotation
        item.gameObject.transform.localPosition = Vector3.zero;
        item.gameObject.transform.localEulerAngles = Vector3.zero;

        item.gameObject.SetActive(false);
    }

    public void ChangeMainItem(int slotNum)
    {
        if(_pickupableSlots[currMainItemSlot] != null) _pickupableSlots[currMainItemSlot].gameObject.SetActive(false);

        currMainItemSlot = slotNum;
        if (_pickupableSlots[currMainItemSlot] != null)
        {
            MainItem = _pickupableSlots[slotNum];
            _pickupableSlots[currMainItemSlot].gameObject.SetActive(true);
        }
    }

    public void AddCurrItemSlot()
    {
        int newItemSlot = currMainItemSlot + 1;
        if (newItemSlot >= _maxItemAmmount) newItemSlot = 0;

        ChangeMainItem(newItemSlot);
    }

    public void DropItem()
    {
        if(MainItem != null)
        {
            DropItem(MainItem);
            _pickupableSlots[currMainItemSlot] = null;
            MainItem = null;
        }
    }

    private void DropItem(IPickupable item)
    {
        item.gameObject.transform.SetParent(null);

        Rigidbody itemRB = item.gameObject.GetComponent<Rigidbody>();
        itemRB.isKinematic = false;

        itemRB.AddForce(item.gameObject.transform.up * 2, ForceMode.VelocityChange);
    }


    private void ResizeSlots()
    {
        for (int i = 0; i < _maxItemAmmount; i++)
        {
            _pickupableSlots.Add(null);
        }
    }


}
