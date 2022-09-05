using Items;
using Items.ItemsLogic;
using Items.Logic;
using System.Collections;
using UnityEngine;
using Utilities;
namespace Ghosts.EnvIneraction
{
    public class GhostEnvInteraction : MonoBehaviour
    {
        [SerializeField]
        private InteractionScript _interaction;
        [SerializeField]
        private GameObject _handprint;

        [SerializeField]
        private GhostInfo _ghostInfo;

        [SerializeField]
        private LayerMask _ghostInterectWithLayer;
        [SerializeField, Tooltip("How close player should be to interection, to lost sanity")]
        private float _interecMaxDistanceToPlayerSanity = 4f;

        [SerializeField]
        private float _ghostInteractionCDMultiplayer = 0.02f;



        private int _maxEMFLevel = 4;
        // private const float MaxInteractionRadius = 8f;
        private float _defaultInteractionChance;
        private float _interactionCoef;

        private float _itemsThrowInteractionRadius;
        private float _doorsTouchInteractionRadius;
        private float _clickableInteractionRadius;

        private float _maxInteractionRadius = 0;

        private float _itemsThrowPower;

        private int _hitColliderSize = 0;
        private Vector3 _playerPos;
        private Vector3 _interectionPos;
        private bool _interected = false;

        private float _leaveHandprintChance = 25f;
        private bool _canLeaveHandprint = false;

        private float _ghostInterectCD = 0f;

        private WaitForSeconds WaitForCheckCD;

        private Collider[] hitColliders = new Collider[100];

        private bool _interectedWithPickupable = false;
        private bool _interectedWithClickable = false;
        private bool _interectedWithDoor = false;

        private void Start()
        {
            SetUpInfo();
            StartCoroutine(ItemInteraction());
        }

        private IEnumerator ItemInteraction()
        {
            while (true)
            {
                _interected = false;
                //Can be optimized
                InteractSphereCheck();
                if (!_interected) yield return new WaitForSeconds(CalculateInterectCD());
                else yield return WaitForCheckCD;
            }
        }
        private float CalculateInterectCD() => Mathf.Max(0f, _ghostInterectCD - (_ghostInfo.FinalGhostAnger * _ghostInteractionCDMultiplayer));

        private void InteractSphereCheck()
        {
            _hitColliderSize = Physics.OverlapSphereNonAlloc(transform.position, _maxInteractionRadius, hitColliders, _ghostInterectWithLayer);

            ResetInterectedWith();
            for (int i = 0; i < _hitColliderSize; i++)
            {
                if (!_interectedWithPickupable) if (CheckForPickupable(hitColliders[i])) return; else _interectedWithPickupable = true;
                if (!_interectedWithClickable) if (CheckForClickable(hitColliders[i])) return; else _interectedWithClickable = true;
                if (!_interectedWithDoor) if (CheckForDoors(hitColliders[i])) return; else _interectedWithDoor = true;
            }
        }

        private void ResetInterectedWith()
        {
            _interectedWithPickupable = false;
            _interectedWithClickable = false;
            _interectedWithDoor = false;
        }
        private bool CheckForPickupable(Collider hitCollider)
        {
            Pickupable pickupable = hitCollider.GetComponent<Pickupable>();
            if (pickupable != null && Vector3.Distance(transform.position, hitCollider.transform.position) <= _itemsThrowInteractionRadius)
            {
                if (CalculateInteractionChance() == true)
                {
                    _interected = true;
                    InteractWithPickupable(pickupable);
                    return true;
                }
            }
            return false;
        }
        private void InteractWithPickupable(Pickupable item)
        {
            if (item != null)
            {
                InstantiateInteraction(item.transform.position);
                DropItemRb(item);
            }
        }

        private bool CheckForClickable(Collider hitCollider)
        {
            IClickable clickable = hitCollider.GetComponent<IClickable>();
            if (clickable != null && Vector3.Distance(transform.position, hitCollider.transform.position) <= _clickableInteractionRadius)
            {
                if (CalculateInteractionChance() == true)
                {
                    _interected = true;
                    InteractWithClickable(clickable);
                    return true;
                }
            }
            return false;
        }
        private void InteractWithClickable(IClickable clickable)
        {
            if (clickable != null)
            {
                InstantiateInteraction(clickable.gameObject.transform.position);
                clickable.OnClick();
            }
        }

        private bool CheckForDoors(Collider hitCollider)
        {
            DoorDraggable door = hitCollider.GetComponent<DoorDraggable>();
            if (door != null && Vector3.Distance(transform.position, hitCollider.transform.position) <= _doorsTouchInteractionRadius)
            {
                if (CalculateInteractionChance() == true)
                {
                    _interected = true;
                    InteractWithDoors(door);

                    if (_canLeaveHandprint && door.FingerprintTransform != null)
                    {
                        if (RandomGenerator.CalculateChance(_leaveHandprintChance))
                        {
                            GameObject newFingerprint = Instantiate(_handprint, door.FingerprintTransform.position, door.FingerprintTransform.rotation);
                            newFingerprint.transform.parent = door.transform;
                        }
                    }

                    return true;
                }
            }
            return false;
        }
        private void InteractWithDoors(DoorDraggable door)
        {
            if (door != null)
            {
                InstantiateInteraction(door.transform.position);
                door.GhostDrugDoor();
            }
        }


        private bool CalculateInteractionChance()
        {
            float chance = _defaultInteractionChance + (_ghostInfo.FinalGhostAnger * _interactionCoef);
            return RandomGenerator.CalculateChance(chance);
        }


        private void InstantiateInteraction(Vector3 pos)
        {
            _interected = true;
            InteractionScript obj = Instantiate(_interaction, pos, Quaternion.identity);

            CheckForPlayerSanity(obj);
            SetObjectEmfLvl(ref obj);
        }

        private void CheckForPlayerSanity(InteractionScript obj)
        {
            if (_ghostInfo.PlayerPoint == null) return;
            _playerPos = _ghostInfo.PlayerPoint.position;
            _interectionPos = obj.transform.position;
            _playerPos.y = 0f; _interectionPos.y = 0f;
            if (Vector3.Distance(_playerPos, _interectionPos) <= _interecMaxDistanceToPlayerSanity)
            {
                _ghostInfo.PlayerSanity.ChangeSanity(_ghostInfo.GhostData.PlayerSanityMinusPerGhostInterection);
            }
        }

        private void SetObjectEmfLvl(ref InteractionScript obj)
        {
            obj.EmfLvl = Random.Range(2, _maxEMFLevel);
        }
        private void SetUpInfo()
        {
            _maxInteractionRadius = 0f;
            _maxEMFLevel = 4;
            if (_ghostInfo.GhostData.GhostEvidences.Contains(GhostEvidence.GhostEvidencesTypes.EMF5)) _maxEMFLevel = 5;
            if (_ghostInfo.GhostData.GhostEvidences.Contains(GhostEvidence.GhostEvidencesTypes.Fingerprints)) _canLeaveHandprint = true;
            WaitForCheckCD = new WaitForSeconds(_ghostInfo.GhostData.GhostFindInerectionsCD);
            _ghostInterectCD = _ghostInfo.GhostData.GhostBetweenInterectionsCD;

            _itemsThrowPower = _ghostInfo.GhostData.ItemThrowPower;
            _itemsThrowInteractionRadius = _ghostInfo.GhostData.ItemThrowRadiusOfInterection;
            _maxInteractionRadius = Mathf.Max(_itemsThrowInteractionRadius, _maxInteractionRadius);

            _doorsTouchInteractionRadius = _ghostInfo.GhostData.DoorsTouchRadiusInterection;
            _maxInteractionRadius = Mathf.Max(_doorsTouchInteractionRadius, _maxInteractionRadius);

            _clickableInteractionRadius = _ghostInfo.GhostData.ClickableTouchRadiusInterection;
            _maxInteractionRadius = Mathf.Max(_clickableInteractionRadius, _maxInteractionRadius);

            _defaultInteractionChance = _ghostInfo.GhostData.DefaultInterectionChance;
            _interactionCoef = _ghostInfo.GhostData.InterectionsCoef;
        }
        private void DropItemRb(IPickupable item)
        {
            Rigidbody itemRB = item.gameObject.GetComponent<Rigidbody>();
            Vector3 randomItemDir = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 1f), Random.Range(-1f, 1f));

            itemRB.AddForce(randomItemDir * _itemsThrowPower, ForceMode.VelocityChange);
        }
    }
}