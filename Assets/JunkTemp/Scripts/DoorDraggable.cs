using Items.Logic;
using Infrastructure;
using Managers.Services;
using UnityEngine;

public class DoorDraggable : MonoBehaviour, IDraggable
{

    [SerializeField]
    private float _forceAmmount = 15f;
    [SerializeField]
    private float _distance = 3f;

    private Camera _cam;
    private bool _isInterracting = false;
    private Rigidbody _rb;

    void Awake()
    {
        _cam = Camera.main;
    }


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (_isInterracting)
        {
            DraggDoor();
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
