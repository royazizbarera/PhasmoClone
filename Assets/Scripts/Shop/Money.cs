using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    [SerializeField] private JSONData _jsonData;

    [SerializeField] private float _money;

    [SerializeField] private TextMeshProUGUI _shopMoneyTXT;

    private void Start()
    {
        _jsonData.LoadInfo();
        _money = _jsonData._storedInfo.Money;
        _shopMoneyTXT.text = "Money: " + _money.ToString();
    }
    public bool CheckForEnoughMoney(float amount)
    {
        if (_money >= amount) return true;
        else return false;
    }

    public void SpendMoney(float amount)
    {
        _money -= amount;
        _shopMoneyTXT.text = "Money: " + _money.ToString();
        _jsonData._storedInfo.Money = _money;
        _jsonData.SaveInfo();
    }
}
