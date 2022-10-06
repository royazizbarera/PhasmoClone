using Infrastructure;
using Infrastructure.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoReward : MonoBehaviour
{
    [SerializeField] private float Value;
    [SerializeField] private string Name;

    private bool _isPhotographed = false;

    private GameObjectivesService _gameObjectives;
    private void Start()
    {
        _gameObjectives = AllServices.Container.Single<GameObjectivesService>();
    }
    public string GetRewardName()
    {
        return Name;
    }
    public float GetRewardValue()
    {
        return Value;
    }
    public void Photograph()
    {
         _gameObjectives.PhotoTaken(Name);
        _isPhotographed = true;
    }
    public bool CheckIfPhotographed()
    {
        return _isPhotographed;
    }
}
