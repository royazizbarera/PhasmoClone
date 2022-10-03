using UnityEngine;
using Items.Logic;

[RequireComponent(typeof(Collider))]
public class LightButton : MonoBehaviour, IClickable
{
    [SerializeField] private Light[] _connectedLights;

    [SerializeField] private bool _isEnabled = true;

    [SerializeField]
    private AudioClip _switchSound;
    [SerializeField]
    private float _volume;

    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_isEnabled)
        {
            foreach (Light light in _connectedLights)
            {
                light.enabled = true;
            }
        }
        else
        {
            foreach (Light light in _connectedLights)
            {
                light.enabled = false;
            }
        }
    }
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
        _audioSource.PlayOneShot(_switchSound, _volume);
    }
}