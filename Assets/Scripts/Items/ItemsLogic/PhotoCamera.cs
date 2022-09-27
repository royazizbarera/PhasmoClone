using Items.Logic;
using UnityEngine;
using System.Collections;
using TMPro;
using System;
using Infrastructure.Services;
using Infrastructure;
using UI.Journal;

namespace Items.ItemsLogic
{
    public class PhotoCamera : MonoBehaviour, IMainUsable, IDroppable
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private TextMeshProUGUI _shotsLeftText;

        [SerializeField] private Light _flash;
        [SerializeField] private float _flashTime;

        [SerializeField] private int _shotsLeft;
        [SerializeField] private float _cooldown;

        [SerializeField] private float _rayCastGirth = 0.5f;
        [SerializeField] private float _rayCastWidth = 5f;
        [SerializeField] private LayerMask _rewardLayer;
        [SerializeField] private Transform _checkPoint;
        [SerializeField] private float _checkRadius;

        private string _snapshotPath;
        private string _rewardName;
        private float _rewardValue;

        [SerializeField] private MeshRenderer _screenMesh;
        [SerializeField] private Material _renderTextureMat;
        [SerializeField] private RenderTexture _renderTexture;

        [SerializeField] private int resWidth = 400;
        [SerializeField] private int resHeight = 300;

        private Journal _journal;
        private GameFactory _gameFactory;
        private GameFlowService _gameFlowService;
        private bool _isReady = true;

        private const float SnapshotDelay = 0.02f;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<GameFactory>();
            _gameFlowService = AllServices.Container.Single<GameFlowService>();
            _journal = _gameFactory.GetJournal();

            _shotsLeftText.text = _shotsLeft.ToString();
            SetUpCamera();
            DisableCamera();
        }

        public void OnMainUse()
        {
            if (CheckIfReady())
            {                
                MakePhoto();
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

            CheckTargets();
            if (_journal != null && _journal.CheckForEmptyPhotos()) Invoke(nameof(TakeSnapshot), SnapshotDelay);

            StartCoroutine(nameof(Cooldown));
        }

        private void CheckTargets()
        {
            if (_journal == null)
            {
                _journal = _gameFactory.GetJournal();
            }
            if (_journal != null && _journal.CheckForEmptyPhotos())
            {
                Collider[] targets = Physics.OverlapCapsule(transform.position, _checkPoint.position, _checkRadius, _rewardLayer);
                if (targets.Length != 0)
                {
                    Collider target = FindNearestPhotable(targets);
                    if (target != null)
                    {
                        PhotoReward targetReward = target.GetComponent<PhotoReward>();
                        Debug.Log(targetReward.GetRewardName());
                        if (targetReward.CheckIfPhotographed() == false)
                        {
                            _rewardName = targetReward.GetRewardName();
                            _rewardValue = targetReward.GetRewardValue();
                            targetReward.Photograph();
                            _gameFlowService.AddPhotoReward(_rewardValue);
                        }  
                    }
                }
                else
                {
                    _rewardName = null;
                    _rewardValue = 0;
                }
            }
        }
        private void TakeSnapshot()
        {
            Texture2D snapShot = _renderTexture.toTexture2D();
            byte[] bytes = snapShot.EncodeToPNG();
            string filename = ScreenShotName(resWidth, resHeight);
            System.IO.File.WriteAllBytes(filename, bytes);
            _snapshotPath = filename;

            _journal.SendPhotoToJournal(_snapshotPath, _rewardName);
            _rewardName = null;
        }
        
        private string ScreenShotName(int width, int height)
        {
            return string.Format("{0}/Resources/Snapshots/snap{1}.png",
                                 Application.dataPath,
                                 _journal.GetCurrentPhoto().ToString());
        }
        
        
        private Collider FindNearestPhotable(Collider[] targets)
        {
            float minDistance = Mathf.Infinity;
            Collider nearestTarget = null;
            foreach (Collider target in targets)
            {
                PhotoReward targetReward = target.GetComponent<PhotoReward>();
                if (targetReward)
                {
                    if (targetReward.enabled == true && !targetReward.CheckIfPhotographed())
                    {
                        {
                            float distance = Vector3.Distance(transform.position, target.transform.position);
                            if (minDistance > distance)
                            {
                                minDistance = distance;
                                nearestTarget = target;
                            }
                        }
                    }
                }
            }
            return nearestTarget;
        }

        private void OnDisable()
        {
            _isReady = true;
            _flash.enabled = false;
        }
        private void OnEnable()
        {
            _camera.gameObject.SetActive(true);
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
            _camera.gameObject.SetActive(false);
        }

        private void SetUpCamera()
        {
            _camera.targetTexture = _renderTexture;
            _screenMesh.material = _renderTextureMat;
        }
    }    
}
