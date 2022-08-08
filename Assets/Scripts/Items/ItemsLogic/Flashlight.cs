using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour, IMainUsable
{
    [SerializeField]
    private Light _light;

    public void OnMainUse()
    {
        if (_light.enabled) _light.enabled = false;
        else _light.enabled = true;
    }
}
