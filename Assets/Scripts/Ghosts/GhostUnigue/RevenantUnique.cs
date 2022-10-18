using Ghosts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevenantUnique : MonoBehaviour
{
    private AttackPatrol _attackPatrol;
    private GhostInfo _ghostInfo;

    void Start()
    {
        _attackPatrol = GetComponentInParent<AttackPatrol>();
        _ghostInfo = GetComponentInParent<GhostInfo>();
    }


}
