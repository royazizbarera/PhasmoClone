using System;
using UnityEngine;
using UnityEngine.Events;

public class AttackState : MonoBehaviour
{
    public UnityEvent[] _onAttackEnterActions;

    public void onEnterState()
    {
        foreach (UnityEvent currAction in _onAttackEnterActions)
        {
            if (currAction != null) currAction.Invoke();
        }
    }
}
