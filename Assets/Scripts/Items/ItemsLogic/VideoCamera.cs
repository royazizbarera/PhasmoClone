using Items.Logic;
using UnityEngine;

namespace Items.ItemsLogic
{
    public class VideoCamera : MonoBehaviour, IMainUsable
{
        [SerializeField] private Material _renderTextureMat;
        [SerializeField] private GameObject _camera;
        [SerializeField] private GameObject _cameraGreen;

        private bool _normalMode = true;

        private void Start()
        {
            SetNormalMode();
        }
        public void OnMainUse()
        {
            if (_normalMode)
            {
                SetGhostOrbMode();
            }
            else
            {
                SetNormalMode();
            }
        }

        private void SetNormalMode()
        {
            _renderTextureMat.color = Color.white;
            _cameraGreen.SetActive(false);
            _camera.SetActive(true);
            _normalMode = true;
        }
        private void SetGhostOrbMode()
        {
            _renderTextureMat.color = Color.green;
            _camera.SetActive(false);
            _cameraGreen.SetActive(true);
            _normalMode = false;
        }
    }
}
