using Infrastructure;
using Infrastructure.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    private DataSaveLoader _dataSaveLoader;
    private GameFactory _gameFactory;

    void Start()
    {
        _dataSaveLoader = AllServices.Container.Single<DataSaveLoader>();
        _gameFactory = AllServices.Container.Single<GameFactory>();
    }

    public void AddMoney(float money)
    {
        _dataSaveLoader.AddMoney(money);
        _gameFactory.GetMainHero().GetComponent<Money>().LoadMoney();
    }

}
