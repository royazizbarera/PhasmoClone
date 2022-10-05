using Items.Logic;
using UnityEngine;

namespace Items.ItemsLogic
{
    public class Flashlight : MonoBehaviour, IMainUsable
    {
        [SerializeField]
        private Light _light;

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        public void OnMainUse()
        {
            if (_light.enabled) _light.enabled = false;
            else _light.enabled = true;

            _audioSource.Play();
        }
    }
}