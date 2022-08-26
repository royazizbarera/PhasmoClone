using GameFeatures;
using Infrastructure;
using Infrastructure.Services;
using Items.Logic;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Utilities.Constants;

public class Thermo : MonoBehaviour, IMainUsable
{
    [SerializeField]
    private RoomIdentifire _currRoom;
    [SerializeField]
    private Canvas _thermoScreen;
    [SerializeField]
    private TextMeshProUGUI _temperatureTXT;

    private LevelRooms.LevelRoomsEnum _ghostRoom;
        
    private LevelSetUp _levelSetUp;
    private bool _isThermoEnabled = false;
    private int _currTemperature = 1;

    private const float CheckTempCD = 0.5f;

    private void Start()
    {
        _levelSetUp = AllServices.Container.Single<LevelSetUp>();
        _ghostRoom = _levelSetUp.CurrGhostRoom;
        if (_ghostRoom == LevelRooms.LevelRoomsEnum.NoRoom) _levelSetUp.OnLevelSetedUp += SetUpRoom;
    }

    public void OnMainUse()
    {
        SwitchEnable();
    }

    private void SwitchEnable()
    {
        if (_isThermoEnabled)
        {
            _isThermoEnabled = false;
            TurnOff();
        }
        else
        {
            _isThermoEnabled = true;
            TurnOn();
        }
    }
    
    private IEnumerator CheckTemperature()
    {
        while (true)
        {
            CheckTemp();
            yield return new WaitForSeconds(CheckTempCD);
        }
    }

    private void CheckTemp()
    {
        if (_currRoom.CurrRoom == _ghostRoom && _ghostRoom != LevelRooms.LevelRoomsEnum.NoRoom)
        {
            _currTemperature = 0;
        }
        else _currTemperature = 14;
        SetText();
    }

    private void SetText()
    {
        _temperatureTXT.text = _currTemperature.ToString();
    }

    private void TurnOn()
    {
        _thermoScreen.gameObject.SetActive(true);
        StartCoroutine(nameof(CheckTemperature));
    }

    private void TurnOff()
    {
        _thermoScreen.gameObject.SetActive(false);
        StopCoroutine(nameof(CheckTemperature));
    }

    private void SetUpRoom()
    {
        _ghostRoom = _levelSetUp.CurrGhostRoom;
    }

}
