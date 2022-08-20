using Items.Logic;
using Infrastructure;
using Items.Logic;
using Managers.Services;
using UnityEngine;

namespace Player.Inventory
{
    [RequireComponent(typeof(Inventory))]
    public class ItemInteraction : MonoBehaviour
    {
        [SerializeField]
        private float _rayCastGirth = 0.5f;

        [SerializeField]
        private float _rayCastWidth = 2.3f;

        [SerializeField]
        private float _maxDistanceGrab = 1.5f;

        [SerializeField]
        private LayerMask _interactableItemLayer;

        private Camera _mainCamera;

        private Inventory _inventory;
        private InputSystem _inputSystem;

        private IDraggable _currDraggableItem;
        private void Start()
        {
            _inventory = GetComponent<Inventory>();
            _inputSystem = AllServices.Container.Single<InputSystem>();


            _mainCamera = Camera.main;
            if(_mainCamera == null)
            {
                Debug.Log("Huita");
            }
            _inputSystem.DropItemAction += DropItem;
            _inputSystem.PickUpItemAction += PickUpItem;
            _inputSystem.SwitchItemAction += SwitchMainItem;
            _inputSystem.PrimaryUseAction += PrimaryUse;
            _inputSystem.SecondaryUseAction += SecondaryUse;
            _inputSystem.MainUseAction += ClickOnItem;
            _inputSystem.MainUseCanceledAction += CancelDraggItem;
        }

        private void OnDestroy()
        {
            _inputSystem.DropItemAction -= DropItem;
            _inputSystem.PickUpItemAction -= PickUpItem;
            _inputSystem.SwitchItemAction -= SwitchMainItem;
            _inputSystem.PrimaryUseAction -= PrimaryUse;
            _inputSystem.SecondaryUseAction -= SecondaryUse;
            _inputSystem.MainUseAction -= ClickOnItem;
            _inputSystem.MainUseCanceledAction -= CancelDraggItem;
        }

        private void ClickOnItem()
        {
            var ray = _mainCamera.ViewportPointToRay(Vector3.one * _rayCastGirth);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _rayCastWidth, _interactableItemLayer))
            {
                Debug.DrawRay(hit.point, hit.normal, Color.green, 2, false);
                IClickable clickable = hit.transform.GetComponent<IClickable>();
                _currDraggableItem = hit.transform.GetComponent<IDraggable>();

                if (clickable != null)
                {
                    clickable.OnClick();
                }
                if(_currDraggableItem != null)
                {
                    _currDraggableItem.OnDragBegin();
                }
            }
        }

        private void Update()
        {
            if(_currDraggableItem != null)
            {
                HoldObject();
            }
        }
        private void HoldObject()
        {
            if (Vector3.Distance(_currDraggableItem.gameObject.transform.position, _mainCamera.transform.position) > _maxDistanceGrab)
            {
                CancelDraggItem();
            }
        }
        private void CancelDraggItem()
        {
            if(_currDraggableItem != null)
            {
                _currDraggableItem.OnDragEnd();
                _currDraggableItem = null;
            }
        }

        private void PrimaryUse()
        {
            if (_inventory.MainItem != null)
            {
                if (_inventory.MainItem is IMainUsable)
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
            if (_inventory.MainItem != null)
            {
                _inventory.DropMainItem();
            }
        }

        private void PickUpItem()
        {
            if (_inventory.IsInventoryFull) return;
            var ray = _mainCamera.ViewportPointToRay(Vector3.one * _rayCastGirth);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _rayCastWidth, _interactableItemLayer))
            {
                IPickupable pickable = hit.transform.GetComponent<IPickupable>();
                if (pickable != null)
                {
                    _inventory.AddItem(pickable);
                }
            }
        }

    }
}