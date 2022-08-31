using Items.Logic;
using UnityEngine;
using System.Collections;

namespace Items.ItemsLogic
{
    public class UVFlashlight : MonoBehaviour, IMainUsable
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
                _light.enabled = true;
                _uvLight.EnableUVLight();
                _isEnabled = true;
            }
            else
            {
                _light.enabled = false;
                _uvLight.DisableUVLight();
                _isEnabled = false;
            }
        }
    }
}