using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;

    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private TextMeshProUGUI _volumeTXT;
    void Start()
    {
        LoadVolume();
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey("SoundsVolume"))
        {
            _audioMixer.audioMixer.SetFloat("SoundsVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("SoundsVolume")));
            _volumeTXT.text = Mathf.RoundToInt(Mathf.Lerp(0, 100, PlayerPrefs.GetFloat("SoundsVolume"))).ToString() + "%";
            _volumeSlider.value = PlayerPrefs.GetFloat("SoundsVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("SoundsVolume", 1f);
            _audioMixer.audioMixer.SetFloat("SoundsVolume", Mathf.Lerp(-80, 0, 1f));
            _volumeTXT.text = Mathf.RoundToInt(Mathf.Lerp(0, 100, 1f)).ToString() + "%";
            _volumeSlider.value = 1f;
        }
    }

    public void ChangeVolume()
    {
        _audioMixer.audioMixer.SetFloat("SoundsVolume", Mathf.Lerp(-80, 0, _volumeSlider.value));
        _volumeTXT.text = Mathf.RoundToInt(Mathf.Lerp(0, 100, _volumeSlider.value)).ToString() + "%";


        PlayerPrefs.SetFloat("SoundsVolume", _volumeSlider.value);
    }


}
