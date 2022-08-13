using UnityEngine;
using Items.Logic;

public class LightSwitch : MonoBehaviour, IClickable
{
    [SerializeField] private Light[] _connectedLights;

    [SerializeField] private Transform _rotationAxis;
    [SerializeField] private float _enabledButtonAngle = 5f;

    [SerializeField] private bool _isEnabled = true;
    public void OnClick()
    {
        if (!_isEnabled)
        {
            foreach (Light light in _connectedLights)
            {
                light.enabled = true;               
            }
            _isEnabled = true;
            _rotationAxis.Rotate(_enabledButtonAngle, 0f, 0f);
        }
        else
        {
            foreach (Light light in _connectedLights)
            {
                light.enabled = false;        
            }
            _isEnabled = false;
            _rotationAxis.Rotate(-_enabledButtonAngle, 0f, 0f);
        }
    }
}
