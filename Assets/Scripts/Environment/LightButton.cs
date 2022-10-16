using UnityEngine;
using Items.Logic;
using System;

[RequireComponent(typeof(Collider))]
public class LightButton : MonoBehaviour, IClickable
{
    public Action _onLightOn, _onLightOff;

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
            _onLightOn?.Invoke();
        }
        else if (_isEnabled && !_ghostAttack)
        {
            foreach (Light light in _connectedLights)
            {
                light.enabled = false;
            }
            _isEnabled = false;
            _onLightOff?.Invoke();
        }
        _audioSource.PlayOneShot(_switchSound, _volume);
    }

    public void GhostAttackOffLight()
    {
        foreach (Light light in _connectedLights)
        {
            light.enabled = false;
        }
        _onLightOff?.Invoke();
        _isEnabled = false;
        _ghostAttack = true;
    }
    public void EndGhostAttack()
    {
        _ghostAttack = false;
    }
    public bool CheckIfEnabled()
    {
        return _isEnabled;
    }
}