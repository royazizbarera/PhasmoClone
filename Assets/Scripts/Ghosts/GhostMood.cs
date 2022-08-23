using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMood : MonoBehaviour
{
    
    [SerializeField]
    private GhostState _attackState, _idleState;

    [SerializeField]
    private GhostStateMachine _ghostStateMachine;

    void Start()
    {
        _ghostStateMachine.ChangeState(_idleState);
    }
    
    void GetAngry()
    {
        _ghostStateMachine.ChangeState(_attackState);
    }
}
