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
    private bool _ghostAttack = false;
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
        if (!_isEnabled && !_ghostAttack)
        {
            foreach (Light light in _connectedLights)
            {
                light.enabled = true;
            }
            _isEnabled = true;
        }
        else if (_isEnabled && !_ghostAttack)
        {
            foreach (Light light in _connectedLights)
            {
                light.enabled = false;
            }
            _isEnabled = false;
        }
        _audioSource.PlayOneShot(_switchSound, _volume);
    }

    public void GhostAttackOffLight()
    {
        foreach (Light light in _connectedLights)
        {
            light.enabled = false;
        }
        _isEnabled = false;
        _ghostAttack = true;
    }
    public void EndGhostAttack()
    {
        _ghostAttack = false;
    }
}