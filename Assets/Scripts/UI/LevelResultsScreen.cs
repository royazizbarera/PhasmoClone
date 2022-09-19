using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using TMPro;
using Infrastructure.Services;
using Infrastructure;

public class LevelResultsScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI IsSurvivedTXT;

    private GameFlowService _gameFlowService;
    public void LoadResults()
    {
        _gameFlowService = AllServices.Container.Single<GameFlowService>();

        if (_gameFlowService.Died == false) IsSurvivedTXT.text = "Survived";
        else IsSurvivedTXT.text = "You Died";
    }
}
