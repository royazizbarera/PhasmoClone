using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    [SerializeField] private float _money;

    [SerializeField] private TextMeshProUGUI _shopMoney;

    private void Start()
    {
        _shopMoney.text = "Money: " + _money.ToString();
    }
    public bool CheckForEnoughMoney(float amount)
    {
        if (_money >= amount) return true;
        else return false;
    }

    public void SpendMoney(float amount)
    {
        _money -= amount;
        _shopMoney.text = "Money: " + _money.ToString();
    }
}
