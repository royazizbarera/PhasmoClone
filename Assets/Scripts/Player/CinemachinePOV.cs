using UnityEngine;
using Cinemachine;

public class CinemachinePOV : CinemachineExtension
{
    [SerializeField] private float _sensitivity = 100f;
    [SerializeField] private float _clampAngle = 90f;
    [SerializeField] private Transform playerBody;

    private InputSystem _inputSystem;
    private Vector3 _startingRotation;

    protected override void Awake()
    {
        _inputSystem = AllServices.Container.Single<InputSystem>();
        base.Awake();
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (_startingRotation == null) _startingRotation = transform.localRotation.eulerAngles;
                Vector2 deltaInput = _inputSystem.CameraAxis;
                _startingRotation.x += deltaInput.x * _sensitivity * Time.deltaTime;
                _startingRotation.y -= deltaInput.y * _sensitivity * Time.deltaTime;
                _startingRotation.y = Mathf.Clamp(_startingRotation.y, -_clampAngle, _clampAngle);
                state.RawOrientation = Quaternion.Euler(_startingRotation.y, _startingRotation.x, 0f);
            }
        }
    }
}
