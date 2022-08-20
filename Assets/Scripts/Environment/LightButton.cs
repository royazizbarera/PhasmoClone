using UnityEngine;
using Items.Logic;

[RequireComponent(typeof(Collider))]
public class LightButton : MonoBehaviour, IClickable
{
    [SerializeField] private Light[] _connectedLights;

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
        }
        else
        {
            foreach (Light light in _connectedLights)
            {
                light.enabled = false;
            }
            _isEnabled = false;
        }
    }
}