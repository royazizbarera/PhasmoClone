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

    [SerializeField]
    private AudioClip _explodeSound;
    [SerializeField]
    private float _explodeVolume = 0.1f;

    private AudioSource _audioSource;
    private bool _isExploded = false;
    private bool _ghostAttack = false;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_isEnabled)
        {
            EnableAllLights();
        }
        else
        {
            DisableAllLights();
        }
    }
    public void OnClick()
    {
        if (!_isEnabled && !_ghostAttack && !_isExploded)
        {
            EnableAllLights();
            _isEnabled = true;
            _onLightOn?.Invoke();
        }
        else if (_isEnabled && !_ghostAttack && !_isExploded)
        {
            DisableAllLights();
            _isEnabled = false;
            _onLightOff?.Invoke();
        }
        _audioSource.PlayOneShot(_switchSound, _volume);
    }
    public void ExplodeLight()
    {
        if (!_isExploded && _isEnabled)
        {
            DisableAllLights();
            AudioHelper.PlayClipAtPoint(_explodeSound, _connectedLights[0].transform.position, _explodeVolume);
            _isExploded = true;
        }
    }
    public void GhostAttackOffLight()
    {
        DisableAllLights();
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
    private void EnableAllLights()
    {
        foreach (Light light in _connectedLights)
        {
            light.enabled = true;
        }
    }
    private void DisableAllLights()
    {
        foreach (Light light in _connectedLights)
        {
            light.enabled = false;
        }
    }
}