using Items.Logic;
using UnityEngine;
using System.Collections;
using TMPro;
using System;

namespace Items.ItemsLogic
{
    public class PhotoCamera : MonoBehaviour, IMainUsable, IDroppable
    {
        [SerializeField] private GameObject _camera;
        [SerializeField] private TextMeshProUGUI _shotsLeftText;

        [SerializeField] private Light _flash;
        [SerializeField] private float _flashTime;

        [SerializeField] private int _shotsLeft;
        [SerializeField] private float _cooldown;

        [SerializeField] private float _rayCastGirth = 0.5f;
        [SerializeField] private float _rayCastWidth = 5f;
        [SerializeField] private LayerMask _rewardLayer;

        private bool _isReady = true;

        private void Start()
        {
            _shotsLeftText.text = _shotsLeft.ToString();
            DisableCamera();
        }

        public void OnMainUse()
        {
            if (CheckIfReady())
            {                
                MakePhoto();
                CheckTargets();
            }
        }
        private bool CheckIfReady()
        {
            if (_shotsLeft > 0 && _isReady) return true;
            else return false;
        }
        private void MakePhoto()
        {   
            _flash.enabled = true;
            _shotsLeft -= 1;
            _shotsLeftText.text = _shotsLeft.ToString();
            _isReady = false;

            StartCoroutine(nameof(Cooldown));

            //Debug.Log("Photo");
        }

        private void CheckTargets()
        {
            
        }
        private void OnDisable()
        {
            _isReady = true;
            _flash.enabled = false;
        }
        private void OnEnable()
        {
            _camera.SetActive(true);
        }
        IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(_flashTime);
            _flash.enabled = false;
            yield return new WaitForSeconds(_cooldown);
            _isReady = true;
        }

        public void DropItem()
        {
            DisableCamera();
        }

        private void DisableCamera()
        {
            _camera.SetActive(false);
        }
    }    
}
