using GameFeatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Constants;

public class AmbientSounds : MonoBehaviour
{   
    [SerializeField] private float _activeVolume = 0.3f;
    [SerializeField] private float _inactiveVolume = 0.05f;

    [SerializeField] private AudioSource _outsideAudioSource;
    [SerializeField] private AudioSource _insideAudioSource;

    private RoomIdentifire _roomIdentifire;

    private WaitForSeconds WaitForSeconds = new WaitForSeconds(0.5f);

    private bool _inHouse = false;

    private int _fadeSteps = 10;
    private float _fadeTime = 0.4f;

    private void Start()
    {
        _roomIdentifire = GetComponent<RoomIdentifire>();

        _outsideAudioSource.volume = _activeVolume;
        _insideAudioSource.volume = _inactiveVolume;

        StartCoroutine(nameof(CheckPlayerPosition));
    }

    IEnumerator CheckPlayerPosition()
    {   
        while (true)
        {
            if (_roomIdentifire.CurrRoom == LevelRooms.LevelRoomsEnum.NoRoom && _inHouse)
            {
                _inHouse = false;
                StartCoroutine(nameof(SetOutsideSoundVolumes));
            }
            else if (_roomIdentifire.CurrRoom != LevelRooms.LevelRoomsEnum.NoRoom && !_inHouse)
            {
                _inHouse = true;
                StartCoroutine(nameof(SetInsideSoundVolumes));
            }

            yield return WaitForSeconds;
        }
    }

    IEnumerator SetInsideSoundVolumes()
    {
        for (int i = 0; i < _fadeSteps; i++)
        {
            _outsideAudioSource.volume -= (_activeVolume - _inactiveVolume) / _fadeSteps;
            _insideAudioSource.volume += (_activeVolume - _inactiveVolume) / _fadeSteps;

            yield return new WaitForSeconds(_fadeTime / _fadeSteps);
        }

        _insideAudioSource.volume = _activeVolume;
        _outsideAudioSource.volume = _inactiveVolume;
    }
    IEnumerator SetOutsideSoundVolumes()
    {
        for (int i = 0; i < _fadeSteps; i++)
        {
            _insideAudioSource.volume -= (_activeVolume - _inactiveVolume) / _fadeSteps;
            _outsideAudioSource.volume += (_activeVolume - _inactiveVolume) / _fadeSteps;

            yield return new WaitForSeconds(_fadeTime / _fadeSteps);
        }

        _outsideAudioSource.volume = _activeVolume;
        _insideAudioSource.volume = _inactiveVolume;
    }
}
