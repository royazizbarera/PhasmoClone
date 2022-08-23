using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _ghostMesh;

    [SerializeField]
    private float _minMeshActiveTime;
    [SerializeField]
    private float _maxMeshActiveTime;

    [SerializeField]
    private float _minMeshFlickCD;  
    [SerializeField]
    private float _maxMeshFlickCD;


    private bool _isMeshActive = false;
    private bool _isAttacking = false;

    private void Start()
    {
        _ghostMesh.SetActive(false);
    }

    public void DisableMesh()
    {
        if (!enabled) return;

        _isAttacking = false;
        _ghostMesh.SetActive(false);
        _isMeshActive = false;
    }

    public void AttackStateMesh()
    {
        if (!enabled) return;

        _isAttacking = true;
        StartCoroutine(AttackStateMeshCoroutine());
    }

    public void ActivateMesh()
    {
        if (!enabled) return;

        _isMeshActive = true;
        _ghostMesh.SetActive(true);
    }

    private IEnumerator AttackStateMeshCoroutine()
    {
        while (true)
        {
            if (!_isAttacking) break;

            SwitchMeshState();

            if (_isMeshActive) yield return new WaitForSeconds(Random.Range(_minMeshActiveTime, _maxMeshActiveTime));
            else yield return new WaitForSeconds(Random.Range(_minMeshFlickCD, _maxMeshFlickCD));
        }

        yield return null;
    }



    private void SwitchMeshState()
    {
        if (_isMeshActive)
        {
            _ghostMesh.SetActive(false);
            _isMeshActive = false;
        }
        else
        {
            _ghostMesh.SetActive(true);
            _isMeshActive = true;
        }
    }

    private void OnDisable()
    {
        _ghostMesh.SetActive(true);
        _isMeshActive = true;
    }
}
