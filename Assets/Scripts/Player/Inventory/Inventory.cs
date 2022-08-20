using Items.Logic;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Player.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public IPickupable MainItem { get; private set; }

        public bool IsInventoryFull
        {
            get { return _currItemAmmount >= _maxItemAmmount; }
            private set { }
        }

        [SerializeField]
        private Transform _slot;

        [SerializeField]
        private int _maxItemAmmount;


        private List<IPickupable> _pickupableSlots = new List<IPickupable>();
        private int _currItemAmmount = 0;

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
                int slotToPassNum = 0;
                if (_pickupableSlots[currMainItemSlot] == null && MainItem == null)
                {
                    slotToPassNum = currMainItemSlot;
                }
                else
                {
                    for (int slotNum = 0; slotNum < _maxItemAmmount; slotNum++)
                    {
                        if (_pickupableSlots[slotNum] == null)
                        {
                            slotToPassNum = slotNum;
                            break;
                        }
                    }
                }

                PickItemRb(itemToAdd);
                _pickupableSlots[slotToPassNum] = itemToAdd;
                _currItemAmmount++;

                if (slotToPassNum == currMainItemSlot)
                {
                    ChangeMainItem(slotToPassNum);
                }
                return true;
            }
            else return false;
        }

        public void ChangeMainItem(int slotNum)
        {
            if (_pickupableSlots[currMainItemSlot] != null) _pickupableSlots[currMainItemSlot].gameObject.SetActive(false);

            currMainItemSlot = slotNum;
            if (_pickupableSlots[currMainItemSlot] != null)
            {
                MainItem = _pickupableSlots[slotNum];
                _pickupableSlots[currMainItemSlot].gameObject.SetActive(true);
            }
            else
            {
                MainItem = null;
            }
        }

        public void SwitchMainItemSlot()
        {
            int newItemSlot = currMainItemSlot + 1;
            if (newItemSlot >= _maxItemAmmount) newItemSlot = 0;

            ChangeMainItem(newItemSlot);
        }

        public void DropMainItem()
        {
            if (MainItem != null)
            {
                DropItemRb(MainItem);
                _currItemAmmount--;
                _pickupableSlots[currMainItemSlot] = null;
                MainItem = null;
            }
        }

        private void PickItemRb(IPickupable item)
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

        private void DropItemRb(IPickupable item)
        {
            item.gameObject.transform.SetParent(_slot.parent);
            //EditorApplication.isPaused = true;
            item.gameObject.transform.localPosition = new Vector3(0f, item.gameObject.transform.localPosition.y, 0f);
          //  EditorApplication.isPaused = true;
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
}