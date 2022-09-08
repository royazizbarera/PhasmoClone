using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Money _money;
    [SerializeField] private ItemsInventory _inventory;

    [SerializeField] private Image _itemMenu;

    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private TextMeshProUGUI _amount;

    [SerializeField] private ItemsList _itemsList;

    [SerializeField] private int _maxItems = 20;

    private int _curItem = 0;
    public void OpenItemMenu(int itemId)
    {
        _itemMenu.gameObject.SetActive(true);

        _name.text = _itemsList.ItemsInfo[itemId].Name;
        _description.text = _itemsList.ItemsInfo[itemId].Description;
        _price.text = _itemsList.ItemsInfo[itemId].Price.ToString();
        _amount.text = _inventory._purchasedItemsAmount[itemId].ToString() + " / " + _maxItems.ToString();

        _curItem = itemId;
    }

    public void BuyItem()
    {
        if (_money.CheckForEnoughMoney(_itemsList.ItemsInfo[_curItem].Price) && _inventory._purchasedItemsAmount[_curItem] < _maxItems)
        {
            _money.SpendMoney(_itemsList.ItemsInfo[_curItem].Price);
            _inventory.AddItem(_curItem);
            _amount.text = _inventory._purchasedItemsAmount[_curItem].ToString() + " / " + _maxItems.ToString();
        }
    }
}
