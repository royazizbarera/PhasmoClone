using Items.Logic;
using Infrastructure;
using Managers.Services;
using UnityEngine;

public class DoorDragable : MonoBehaviour, IDraggable
{

    [SerializeField]
    private float _forceAmmount = 30f;

    private Camera _cam;

    private bool _isInterracting = false;

    private float _mouseX, _mouseY;
    private Vector3 _pushDir, _direction;
    private Rigidbody _rb;

    private InputSystem _input;
    void Awake()
    {
        _cam = Camera.main;
    }


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _input = AllServices.Container.Single<InputSystem>();
    }

    void FixedUpdate()
    {
        if (_isInterracting)
        {
            _mouseX = _input.CameraAxis.x;
            _mouseY = _input.CameraAxis.y;

            _pushDir = _mouseX * _cam.transform.right - _mouseY * _cam.transform.right;
            //_direction = transform.position - 2 * transform.right;
           // _rb.AddForceAtPosition(20 * _pushDir, _direction);
           _rb.AddForce(_forceAmmount * _pushDir);;
        }
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
