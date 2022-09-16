using Items.Logic;
using Infrastructure;
using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(HingeJoint))]
public class DoorDraggable : MonoBehaviour, IDraggable
{
    public bool IsDoorFullyOpened = false;
    public bool IsDoorClosed = false;

    public Transform FingerprintTransform;

    [SerializeField]
    private float _forceAmmount = 15f;
    [SerializeField]
    private float _distance = 3f;

    private Camera _cam;
    private bool _isInterracting = false;
    private Rigidbody _rb;
    private HingeJoint _hingeJoint;
    private float _openedRotation; // Rotation of the door, when it is fully opened

    private float doorCurrRotation;
    private const float MinGhostsForcePower = 6f;
    private const float MaxGhostsForcePower = 13f;
    private const float MinGhostForceTime = 1.2f;
    private const float MaxGhostForceTime = 3f;

    private const float Epsilon = 1f;
    private const float CheckDoorStateCD = 0.3f;
    private const float MotorForce = 100f;

    void Awake()
    {
        _cam = Camera.main;
    }


    void Start()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        _rb = GetComponent<Rigidbody>();
        _openedRotation = _hingeJoint.limits.max * _hingeJoint.axis.y;
        if (_openedRotation < -1f) _openedRotation += 360f;
        StartCoroutine(CheckDoorState());
    }

    void FixedUpdate()
    {
        if (_isInterracting)
        {
            DraggDoor();
           // if (IsDoorClosed) { Debug.Log("Door is closed"); }
           // else if (IsDoorFullyOpened) Debug.Log("Door is fully opened");
        }
    }

    public void GhostDrugDoor()
    {
        _hingeJoint.useMotor = true;
        var motor = _hingeJoint.motor;
        motor.targetVelocity = GenerateForce() * (float)GenerateDirection();
        motor.force = MotorForce;
        _hingeJoint.motor = motor;
        Invoke(nameof(StopDruggingDoor), UnityEngine.Random.Range(MinGhostForceTime, MaxGhostForceTime));
    }

    private void StopDruggingDoor()
    {
        var motor = _hingeJoint.motor;
        motor.targetVelocity = 0f;
        _hingeJoint.motor = motor;
    }
    private float GenerateForce() => UnityEngine.Random.Range(MinGhostsForcePower, MaxGhostsForcePower);

    private int GenerateDirection()
    {
        int randomNum = UnityEngine.Random.Range(0, 1);
        if (randomNum == 0) randomNum = -1;
        if (IsDoorClosed) randomNum = 1;
        else if (IsDoorFullyOpened) randomNum = -1;
        return randomNum;
    }

    private IEnumerator CheckDoorState()
    {
        while (true)
        {
            CheckDoorRotation();
            yield return new WaitForSeconds(CheckDoorStateCD);
        }
    }
    private void CheckDoorRotation()
    {
        doorCurrRotation = transform.localEulerAngles.y;
        if (doorCurrRotation < -5f) doorCurrRotation += 360;
       
        if( Mathf.Abs( transform.localEulerAngles.y) <= Epsilon)
        {
            IsDoorClosed = true;
            IsDoorFullyOpened = false;
        }
        else if( Mathf.Abs(transform.localEulerAngles.y - _openedRotation) <= Epsilon)
        {
            IsDoorClosed = false;
            IsDoorFullyOpened = true;
        }
        else
        {
            IsDoorClosed = false;
            IsDoorFullyOpened = false;
        }
    } 

    private void DraggDoor()
    {
        Ray playerAim = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 nextPos = _cam.transform.position + playerAim.direction * _distance;
        Vector3 currPos = transform.position;
        _rb.velocity = (nextPos - currPos) * _forceAmmount;
    }

    public void OnDragBegin()
    {
        _isInterracting = true;
        //Debug.Log("DrugBegin");
    }

    public void OnDragEnd()
    {
        _isInterracting = false;
        //Debug.Log("DrugEnd");
    }
}
