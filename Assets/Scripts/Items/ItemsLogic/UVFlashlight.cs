using Items.Logic;
using UnityEngine;

namespace Items.ItemsLogic
{
    public class UVFlashlight : MonoBehaviour, IMainUsable
    {
        [SerializeField]
        private Light _light;

        [SerializeField]
        public Material _revealableMaterial;

        private bool _isEnabled = false;

        private void Update()
        {
            if (_isEnabled)
            {
                _revealableMaterial.SetVector("lightPosition", _light.transform.position);
                _revealableMaterial.SetVector("lightDirection", -_light.transform.forward);
                _revealableMaterial.SetFloat("lightAngle", _light.spotAngle);
            }
        }
        public void OnMainUse()
        {
            if (_isEnabled == false)
            {
                _light.enabled = true;
                _isEnabled = true;
            }
            else
            {
                _light.enabled = false;
                _revealableMaterial.SetFloat("lightAngle", 0f);
                _isEnabled = false;
            }
        }
    }
}