using Items.Logic;
using System;
using System.Collections;
using UnityEngine;

namespace Items.ItemsLogic
{
    public class SmudgeStick : MonoBehaviour, IMainUsable
    {
        [SerializeField]
        private SmudgeObj _smudgeObj;
        [SerializeField]
        private ParticleSystem _smokeParts;
        [SerializeField]
        private GameObject _sticksParent;

        private const float TimeBetweenSmudgeObjects = 0.5f;
        private bool _isUsed = false;

        private WaitForSeconds _waitSpawnTime;

        private void Start()
        {
            _waitSpawnTime = new WaitForSeconds(TimeBetweenSmudgeObjects);   
        }

        public void OnMainUse()
        {
            if (!_isUsed)
            {
                _smokeParts.Play();
                _isUsed = true;
                StartCoroutine(nameof(Smudging));
            }
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if(other.)
        //}

        private IEnumerator Smudging()
        {
            while (true)
            {
                Instantiate(_smudgeObj.gameObject, transform.position, Quaternion.identity);
                yield return _waitSpawnTime;
            }
           
        }
    }
}