using Items.Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritBox : MonoBehaviour, IMainUsable
{
    [SerializeField]
    private AudioSource _radioFrequency;
    [SerializeField]
    private AudioClip[] _ghostVoiceClips;
    [SerializeField]
    private float _ghostVoiceVolume = 0.3f;

    private WaitForSeconds WaitForSecond = new WaitForSeconds(1f); 
    private bool _isEnabled = false;
    public void OnMainUse()
    {
        if (_isEnabled == false) EnableSpiritBox();
        else DisableSpiritBox();
    }

    IEnumerator DetectGhostVoice()
    {
        while (true)
        {
            yield return WaitForSecond;
            yield return WaitForSecond;
            yield return WaitForSecond;
            yield return WaitForSecond;
            yield return WaitForSecond;

            _radioFrequency.PlayOneShot(_ghostVoiceClips[Random.Range(0,_ghostVoiceClips.Length)], _ghostVoiceVolume);
        }
    }
    private void OnEnable()
    {
        if (_isEnabled) EnableSpiritBox();
    }
    private void OnDisable()
    {
        StopCoroutine(nameof(DetectGhostVoice));
    }
    private void EnableSpiritBox()
    {
        _isEnabled = true;
        _radioFrequency.Play();
        StartCoroutine(nameof(DetectGhostVoice));
    }
    private void DisableSpiritBox()
    {
        _isEnabled = false;
        _radioFrequency.Pause();
        StopCoroutine(nameof(DetectGhostVoice));
    }
}
