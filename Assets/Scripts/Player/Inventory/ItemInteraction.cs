using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class ItemInteraction : MonoBehaviour
{

    private Camera _characterCamera;
    private Inventory _inventory;

    private InputSystem _inputSystem;

    private void Start()
    {
        _characterCamera = Camera.main;
        _inventory = GetComponent<Inventory>();

        _inputSystem = AllServices.Container.Single<InputSystem>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //if (pickedItem != null)
            //{
            //    DropItem(pickedItem);
            //}
            //else
            //{
            //    var ray = _characterCamera.ViewportPointToRay(Vector3.one * 0.5f);
            //    RaycastHit hit;

            //    if (Physics.Raycast(ray, out hit, 2.2f))
            //    {
            //        var pickable = hit.transform.GetComponent<IPickupable>();

            //        if (pickable != null)
            //        {
            //            PickItem(pickable);
            //        }
            //    }
            //}
        }
    }

}
