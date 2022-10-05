using UnityEngine;
using UnityEngine.Audio;

namespace Infrastructure.Services
{
    public class AudioManager : MonoBehaviour, IService
    {
        [SerializeField]
        private AudioMixerGroup _audioMixer;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(this);
        }

        public void PlaySound(AudioClip _audio, float volume = 1f)
        {
            if (_audio != null)
                _audioSource.PlayOneShot(_audio, volume);
        }
    }
}