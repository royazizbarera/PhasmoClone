using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsInventory : MonoBehaviour
{
    [SerializeField] JSONData _jsonData;

    public int[] _purchasedItemsAmount;

    private void Start()
    {
        _jsonData.LoadInfo();
        _purchasedItemsAmount = _jsonData._storedInfo.ItemsAmount;
    }
    public void AddItem(int itemId)
    {
        _purchasedItemsAmount[itemId] += 1;

        _jsonData._storedInfo.ItemsAmount = _purchasedItemsAmount;
        _jsonData.SaveInfo();
    }
}
