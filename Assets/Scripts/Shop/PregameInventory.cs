using TMPro;
using UnityEngine;

public class PregameInventory : MonoBehaviour
{
    [SerializeField] private ItemsList _itemsList;
    [SerializeField] private ItemsInventory _inventory;

    private int[] _itemsAmount;

    [SerializeField] private TextMeshProUGUI[] _itemLimits;
    private int[] _minItems;
    private int[] _maxItems;

    private int[] _addedItems;


    private void Start()
    {
        _minItems = new int[_itemLimits.Length];
        _maxItems = new int[_itemLimits.Length];
        _addedItems = new int[_itemLimits.Length];
    }
    public void AddItem(int itemId)
    {
        if (_inventory._purchasedItemsAmount[itemId] >= (_minItems[itemId] + _addedItems[itemId]) && (_minItems[itemId] + _addedItems[itemId] < _maxItems[itemId]))
        {
            _addedItems[itemId] += 1;

            _itemLimits[itemId].text = (_minItems[itemId] + _addedItems[itemId]).ToString() + "/" + _maxItems[itemId].ToString();
        }
    }
    public void RemoveItem(int itemId)
    {
        if (_addedItems[itemId] > 0)
        {
            _addedItems[itemId] -= 1;

            _itemLimits[itemId].text = (_minItems[itemId] + _addedItems[itemId]).ToString() + "/" + _maxItems[itemId].ToString();
        }
    }
    public void LoadItemLimits()
    {
        for (int i = 0; i < _itemLimits.Length; i++)
        {
            _minItems[i] = _itemsList.ItemsInfo[i].MinAmount;
            _maxItems[i] = _itemsList.ItemsInfo[i].MaxAmount;
            _addedItems[i] = 0;
            _itemLimits[i].text = _minItems[i].ToString() + "/" + _maxItems[i].ToString();
        }
    }
}
