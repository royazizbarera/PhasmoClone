using GameFeatures;
using Infrastructure;
using Infrastructure.Services;
using Items.Logic;
using System.Collections;
using TMPro;
using UnityEngine;
using Utilities.Constants;

public class Thermo : MonoBehaviour, IMainUsable, IDisababled
{
    [SerializeField]
    private RoomIdentifire _currRoom;
    [SerializeField]
    private Canvas _thermoScreen;
    [SerializeField]
    private TextMeshProUGUI _temperatureTXT;

    [SerializeField]
    private GameObject _thermoBody;

    private GhostInfo _ghostInfo;
    private LevelRooms.LevelRoomsEnum _ghostRoom;
        
    private LevelSetUp _levelSetUp;
    private bool _isThermoEnabled = false;
    private int _currTemperature = 20;

    private bool _hasTempChanged = false;
    private bool _isItCold = true;

    private int _prevTemp = 1;
    private int _minTemp = 1;

    private int _minPossibleTemp = 1;

    private int _randMinNum, _ranMaxNum;

    private WaitForSeconds CheckTempWaiting;

    private const float CheckTempCD = 1f;
    private const int MaxTemp = 29;
    private const int MinTemp = 11;
    private const int GhostRoomMaxTemp = 9;
    private const int GhostRoomMinusMinTemp = -20;
    private const int DefaultTemp = 20;

    private void Start()
    {
        CheckTempWaiting = new WaitForSeconds(CheckTempCD);
        _levelSetUp = AllServices.Container.Single<LevelSetUp>();
        _ghostRoom = _levelSetUp.CurrGhostRoom;
        _currTemperature = DefaultTemp;
        if (_ghostRoom == LevelRooms.LevelRoomsEnum.NoRoom) _levelSetUp.OnLevelSetedUp += SetUpInfo;
    }

    public void OnMainUse()
    {
        SwitchEnable();
    }

    private void SwitchEnable()
    {
        if (_isThermoEnabled)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }
    
    private IEnumerator CheckTemperature()
    {
        while (true)
        {
            yield return CheckTempWaiting;
            CheckTemp();
        }
    }

    private void CheckTemp()
    {
        SetMinMaxValue();

        _hasTempChanged = CalculateTemp();

        if (_hasTempChanged) SetText();
    }

    private void SetMinMaxValue()
    {
        if ( (_currRoom.CurrRoom == _ghostRoom && _ghostRoom != LevelRooms.LevelRoomsEnum.NoRoom) || _isItCold)
        {
            _minTemp = _minPossibleTemp;
        }
        else
        {
            _minTemp = MinTemp;
        }
    }
    private bool CalculateTemp()
    {
        _randMinNum = -3; _ranMaxNum = 3;
        if (_currTemperature + _randMinNum < _minTemp) _randMinNum = 0;
        if (_currTemperature + _ranMaxNum > MaxTemp) _ranMaxNum = 0;

        int plusRandNum = Random.Range(_randMinNum, _ranMaxNum);
        _prevTemp = _currTemperature;
        _currTemperature = _currTemperature + (plusRandNum);

        _currTemperature = Mathf.Clamp(_currTemperature, _minTemp, MaxTemp);

        return _prevTemp == _currTemperature ? false : true;
    }

    private void SetText()
    {
        _temperatureTXT.text = _currTemperature.ToString();
    }

    private void TurnOn()
    {
        _isThermoEnabled = true;
        _thermoScreen.gameObject.SetActive(true);
        StartCoroutine(nameof(CheckTemperature));
    }

    private void TurnOff()
    {
        _isThermoEnabled = false;
        _thermoScreen.gameObject.SetActive(false);
        StopCoroutine(nameof(CheckTemperature));
    }

    private void OnEnable()
    {
        if (_isThermoEnabled) StartCoroutine(nameof(CheckTemperature));
    }

    private void SetUpInfo()
    {
        _ghostInfo = _levelSetUp.GhostInfo;
        _ghostRoom = _levelSetUp.CurrGhostRoom;
        if (_ghostInfo.GhostData.GhostEvidences.Contains(GhostEvidence.GhostEvidencesTypes.FreezingTemps)) _minPossibleTemp = GhostRoomMinusMinTemp;

        if (this == null) return;
        Invoke(nameof(WarmTheHouse), _levelSetUp.SelectedDifficulty.TimeToWarmHouse);
    }

    private void WarmTheHouse()
    {
        _isItCold = false;
    }
    public void EnableItem()
    {
        _thermoBody.SetActive(true);

        if (_isThermoEnabled)
        _thermoScreen.gameObject.SetActive(true);
    }

    public void DisableItem()
    {
        _thermoBody.SetActive(false);
        _thermoScreen.gameObject.SetActive(false);
    }

}
