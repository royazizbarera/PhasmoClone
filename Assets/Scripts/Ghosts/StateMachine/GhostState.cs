using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GhostState : MonoBehaviour
{
    [SerializeField]
    private UnityEvent[] _onStateEnterActions; 
    
    [SerializeField]
    private UnityEvent[] _onStateExitActions;
    

    public void EnterState()
    {
        foreach(UnityEvent _currEvent in _onStateEnterActions)
        {
            if (_currEvent != null) _currEvent.Invoke();
        }
    }

    public void ExitState()
    {
        foreach (UnityEvent _currEvent in _onStateExitActions)
        {
            if (_currEvent != null) _currEvent.Invoke();
        }
    }
}
