using Items.Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, IClickable
{

    [SerializeField]
    private Light _light;
    public void OnClick()
    {
        _light.enabled = false;
    }
}
