using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using TMPro;
using Infrastructure.Services;
using Infrastructure;

public class LevelResultsScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _isSurvivedTXT;
    [SerializeField] private TextMeshProUGUI _survivabilityCoefTXT;
    [SerializeField] private TextMeshProUGUI _difficultyCoefTXT;

    [SerializeField] private TextMeshProUGUI[] _rewardValuesTXT;
    [SerializeField] private TextMeshProUGUI _ghostTypeTXT;
    [SerializeField] private TextMeshProUGUI _totalRewardTXT;

    private float[] _rewardValues;
    private float _totalReward;


    private GameFlowService _gameFlowService;
    private LevelSetUp _levelSetUp;
    public void LoadResults()
    {
        _gameFlowService = AllServices.Container.Single<GameFlowService>();
        _levelSetUp = AllServices.Container.Single<LevelSetUp>();

        if (_gameFlowService.Died == false)
        {
            _isSurvivedTXT.text = "Survived";
            _survivabilityCoefTXT.text = "x1";
        }
        else
        {
            _isSurvivedTXT.text = "You Died";
            _survivabilityCoefTXT.text = "x0.1";
        }

        _rewardValues = _gameFlowService.GetRewardValues();
        _totalReward = _gameFlowService.GetTotalRewardValue();

        for (int i = 0; i < _rewardValues.Length; i++)
        {
            _rewardValuesTXT[i].text = _rewardValues[i].ToString() + " $";
        }

        _ghostTypeTXT.text = _levelSetUp.GhostInfo.GhostData.name;

        _totalRewardTXT.text = _totalReward.ToString() + " $";
    }
}
