using Items;
using Items.ItemsLogic;
using Items.Logic;
using System.Collections;
using UnityEngine;

namespace Ghosts.EnvIneraction
{
    public class GhostEnvInteraction : MonoBehaviour
    {
        [SerializeField]
        private InteractionScript _interaction;
        [SerializeField]
        private GhostInfo _ghostInfo;

       // private const float MaxInteractionRadius = 8f;

        private float _itemsThrowInterectionRadius;
        private float _doorsTouchInterectionRadius;
        private float _clickableInterectionRadius;
        private float _itemsThrowPower;

        private void Start()
        {
            _itemsThrowPower = _ghostInfo.GhostData.ItemThrowPower;
            _itemsThrowInterectionRadius = _ghostInfo.GhostData.ItemThrowRadiusOfInterection;
            _doorsTouchInterectionRadius = _ghostInfo.GhostData.DoorsTouchRadiusInterection;
            _clickableInterectionRadius = _ghostInfo.GhostData.ClickableTouchRadiusInterection;

            StartCoroutine(ItemInteraction());
        }


        private IEnumerator ItemInteraction()
        {
            while (true)
            {
                InteractWithPickupable();
                InteractWithDoors();
                InteractWithClickable();
                yield return new WaitForSeconds(2f);
            }

        }
        private void InteractWithPickupable()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _itemsThrowInterectionRadius);
            for(int i = 0; i< hitColliders.Length;i++)
            {
                Pickupable item = hitColliders[i].GetComponent<Pickupable>();
                if (item != null)
                {
                    InteractionScript obj = Instantiate(_interaction, item.transform.position, Quaternion.identity);
                    obj.EmfLvl = 2;
                    DropItemRb(item);
                }
            }
        } 
        
        private void InteractWithDoors()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _doorsTouchInterectionRadius);
            for(int i = 0; i< hitColliders.Length;i++)
            {
                DoorDraggable door = hitColliders[i].GetComponent<DoorDraggable>();
                if(door != null)
                {
                    InteractionScript obj = Instantiate(_interaction, door.transform.position, Quaternion.identity);
                    obj.EmfLvl = 3;
                    door.GhostDrugDoor();
                }
            }

        }

        private void InteractWithClickable()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _clickableInterectionRadius);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                IClickable clickable = hitColliders[i].GetComponent<IClickable>();
                if (clickable != null)
                {
                    InteractionScript obj = Instantiate(_interaction, hitColliders[i].transform.position, Quaternion.identity);
                    obj.EmfLvl = 4;
                    clickable.OnClick();
                }
            }
        }

        private void DropItemRb(IPickupable item)
        {
            Rigidbody itemRB = item.gameObject.GetComponent<Rigidbody>();
            Vector3 randomItemDir = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 1f), Random.Range(-1f, 1f));

            itemRB.AddForce(randomItemDir * _itemsThrowPower, ForceMode.VelocityChange);
        }
    }
}