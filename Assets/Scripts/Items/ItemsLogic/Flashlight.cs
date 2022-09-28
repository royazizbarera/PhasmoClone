using Items.Logic;
using UnityEngine;

namespace Items.ItemsLogic
{
    public class Flashlight : MonoBehaviour, IMainUsable
    {
        [SerializeField]
        private Light _light;

        [SerializeField]
        private AudioClip _switchSound;
        [SerializeField]
        private float _volume;

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        public void OnMainUse()
        {
            if (_light.enabled) _light.enabled = false;
            else _light.enabled = true;

            _audioSource.PlayOneShot(_switchSound, _volume);
        }
    }
}