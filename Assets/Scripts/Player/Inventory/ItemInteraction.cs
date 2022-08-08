using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class ItemInteraction : MonoBehaviour
{
    [SerializeField]
    private float _rayCastGirth = 0.5f;

    [SerializeField]
    private float _rayCastWidth = 2.3f;

    [SerializeField]
    private Camera _characterCamera;

    private Inventory _inventory;
    private InputSystem _inputSystem;

    private void Start()
    {
        _inventory = GetComponent<Inventory>();
        _inputSystem = AllServices.Container.Single<InputSystem>();

        _inputSystem.DropItemAction += DropItem;
        _inputSystem.PickUpItemAction += PickUpItem;
        _inputSystem.SwitchItemAction += SwitchMainItem;
        _inputSystem.PrimaryUseAction += PrimaryUse;
        _inputSystem.SecondaryUseAction += SecondaryUse;
        _inputSystem.MainUseAction += ClickOnItem;
    }


    private void ClickOnItem()
    {
        var ray = _characterCamera.ViewportPointToRay(Vector3.one * _rayCastGirth);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _rayCastWidth))
        {
            IClickable clickable = hit.transform.GetComponent<IClickable>();
            if (clickable != null)
            {
                clickable.OnClick();
            }
        }
    }

    private void PrimaryUse()
    {
        if(_inventory.MainItem != null)
        {
            if(_inventory.MainItem is IMainUsable)
            {
                (_inventory.MainItem as IMainUsable).OnMainUse();
            }
        } 

    }

    private void SecondaryUse()
    {
        if (_inventory.MainItem != null)
        {
            if (_inventory.MainItem is ISecUsable)
            {
                (_inventory.MainItem as ISecUsable).OnSecUse();
            }
        }
    }

    private void SwitchMainItem()
    {
        _inventory.SwitchMainItemSlot();
    }

    private void DropItem()
    {
        if(_inventory.MainItem != null)
        {
            _inventory.DropMainItem();
        }
    }

    private void PickUpItem()
    {  
        if (_inventory.IsInventoryFull) return;
        var ray = _characterCamera.ViewportPointToRay(Vector3.one * _rayCastGirth);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit,_rayCastWidth))
        {

            IPickupable pickable = hit.transform.GetComponent<IPickupable>();
            if (pickable != null)
            {
                _inventory.AddItem(pickable);
            }
        }
    }

}
