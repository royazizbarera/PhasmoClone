using Items.Logic;
using UnityEngine;
using System.Collections;
using System;

namespace Items.ItemsLogic
{
    public class VideoCamera : MonoBehaviour, IMainUsable, IDroppable
{
        [SerializeField] private Material _renderTextureMat;
        [SerializeField] private GameObject _camera;
        [SerializeField] private GameObject _cameraGreen;

        private bool _normalMode = false;

        private void Start()
        {
            DisableCamera();
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
        private void DisableCamera()
        {
            _camera.SetActive(false);
            _cameraGreen.SetActive(false);
        }
        private void OnEnable()
        {
            if (_normalMode) SetNormalMode();
            else SetGhostOrbMode();
        }

        public void DropItem()
        {
            DisableCamera();
        }
    }
}
