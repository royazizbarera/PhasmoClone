using Items;
using Items.Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ghosts.EnvIneraction
{
    public class GhostItemInteraction : MonoBehaviour
    {
        [SerializeField]
        private GhostInfo _ghostInfo;

        private float _itemsThrowInterectionRadius;
        private float _itemsThrowPower;

        private void Start()
        {
            _itemsThrowPower = _ghostInfo.GhostData.ItemThrowPower;
            _itemsThrowInterectionRadius = _ghostInfo.GhostData.ItemThrowRadiusOfInterrection;

            StartCoroutine(ItemInteraction());
        }


        private IEnumerator ItemInteraction()
        {
            while (true)
            {
                InteractWithIPickupable();
                yield return new WaitForSeconds(2f);
            }

        }


        private void InteractWithIPickupable()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _itemsThrowInterectionRadius);
            for(int i = 0; i< hitColliders.Length;i++)
            {
                Pickupable item = hitColliders[i].GetComponent<Pickupable>();
                if (item != null)
                {
                    DropItemRb(item);
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