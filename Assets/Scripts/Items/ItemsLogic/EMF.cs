using Items.ItemsLogic;
using Items.Logic;
using System;
using System.Collections;
using UnityEngine;

public class EMF : MonoBehaviour, IMainUsable
{
    [SerializeField]
    private Light[] _lightPoints;

    private bool _isEmfEnabled = false;
    private int _currEmfLvl = 1;
    private int _EmfLvlFound = 1;

    public const float EmfRadius = 2f;
    private const float EmfCheckCD = 0.2f;

    public void OnMainUse()
    {
        SwitchEnable();
    }

    private void SwitchEnable()
    {
        if (_isEmfEnabled)
        {
            _isEmfEnabled = false;
            StopCoroutine(nameof(CheckForInteractions));
            TurnOff();
        }
        else
        {
            _isEmfEnabled = true;
            TurnOn();
            StartCoroutine(nameof(CheckForInteractions));
        }
    }

    private IEnumerator CheckForInteractions()
    {
        while (true)
        {
            CheckForInter();
            ShowLights();
            yield return new WaitForSeconds(EmfCheckCD);
        }
    }

    private void CheckForInter()
    {
        _EmfLvlFound = 1;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, EmfRadius);
        foreach (var hitCollider in hitColliders)
        {
            InteractionScript _interection = hitCollider.GetComponent<InteractionScript>();
            if(_interection) if (_EmfLvlFound < _interection.EmfLvl) _EmfLvlFound = _interection.EmfLvl;

        }
        _currEmfLvl = _EmfLvlFound;
    }

    private void ShowLights()
    {
        if (!_isEmfEnabled) _currEmfLvl = 0;
        for(int i = 0; i < _currEmfLvl; i++)
        {
            _lightPoints[i].gameObject.SetActive(true);
        }
        for(int i = _currEmfLvl; i< _lightPoints.Length; i++)
        {
            _lightPoints[i].gameObject.SetActive(false);
        }
    }

    private void TurnOn()
    {
        _currEmfLvl = 1;
        ShowLights();
    }

    private void TurnOff()
    {
        _currEmfLvl = 0;
        ShowLights();
    }

}
