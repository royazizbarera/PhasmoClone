using Infrastructure;
using Infrastructure.Services;
using Items.Logic;
using UnityEngine;

namespace Items.ItemsLogic
{
    public class Flashlight : MonoBehaviour, IMainUsable
    {
        [SerializeField]
        private Light _light;
        [SerializeField]
        private AudioClip _audioClip;
        [SerializeField]
        private float _audioVolume = 0.6f;

        private AudioManager _audioManager;

        private void Start()
        {
            _audioManager = AllServices.Container.Single<AudioManager>();
        }
        public void OnMainUse()
        {
            if (_light.enabled) _light.enabled = false;
            else _light.enabled = true;
            _audioManager.PlaySound(_audioClip, _audioVolume);
        }
    }
}