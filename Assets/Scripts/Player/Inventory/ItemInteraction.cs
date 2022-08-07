using UnityEngine;

public class ItemInteraction : MonoBehaviour
{

    [SerializeField]
    private Camera _characterCamera;
    [SerializeField]
    private Transform _slot;

    // Reference to the currently held item.
    private IPickupable pickedItem;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (pickedItem != null)
            {
                DropItem(pickedItem);
            }
            else
            {
                var ray = _characterCamera.ViewportPointToRay(Vector3.one * 0.5f);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 2.2f))
                {
                    var pickable = hit.transform.GetComponent<IPickupable>();

                    if (pickable != null)
                    {
                        PickItem(pickable);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Method for picking up item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void PickItem(IPickupable item)
    {
        // Assign reference
        pickedItem = item;

        item.gameObject.SetActive(false);

        Rigidbody itemRB = item.gameObject.GetComponent<Rigidbody>();
        // Disable rigidbody and reset velocities
        itemRB.isKinematic = true;
        itemRB.velocity = Vector3.zero;
        itemRB.angularVelocity = Vector3.zero;

        // Set Slot as a parent
        item.gameObject.transform.SetParent(_slot);

        // Reset position and rotation
        item.gameObject.transform.localPosition = Vector3.zero;
        item.gameObject.transform.localEulerAngles = Vector3.zero;

    }

    /// <summary>
    /// Method for dropping item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void DropItem(IPickupable item)
    {
        pickedItem = null;

        item.gameObject.transform.SetParent(null);

        Rigidbody itemRB = item.gameObject.GetComponent<Rigidbody>();
        itemRB.isKinematic = false;


        itemRB.AddForce(item.gameObject.transform.up * 2, ForceMode.VelocityChange);
    }
}
