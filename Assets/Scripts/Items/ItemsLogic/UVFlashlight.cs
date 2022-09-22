using Items.Logic;
using UnityEngine;
using System.Collections;

namespace Items.ItemsLogic
{
    public class UVFlashlight : MonoBehaviour, IMainUsable, IDroppable
    {
        [SerializeField] private UVLight _uvLight;

        [SerializeField]
        private Light _light;


        [SerializeField]
        public Material _revealableMaterial;

        private bool _isEnabled = false;

        public void OnMainUse()
        {
            if (_isEnabled == false)
            {
                _uvLight.EnableUVLight();
                _light.enabled = true;
                _isEnabled = true;
            }
            else
            {
                _uvLight.DisableUVLight();
                _light.enabled = false;
                _isEnabled = false;
            }
        }
        private void OnDisable()
        {
            _uvLight.DisableUVLight();
        }
        private void OnEnable()
        {
            if (_isEnabled) _uvLight.EnableUVLight();
        }

        public void DropItem()
        {
            if (_isEnabled) _uvLight.EnableUVLight();
        }
    }
}