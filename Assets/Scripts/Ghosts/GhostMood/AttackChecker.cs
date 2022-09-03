using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChecker : MonoBehaviour
{
    [SerializeField]
    private GhostInfo _ghostInfo;
    [SerializeField]
    private float _ghostAttackCheckCD;


    private SanityHandler _playerSanity;
    private WaitForSeconds GhostAttackCheckCD;
    void Start()
    {
        if (_ghostInfo.SetedUp) SetUp();
        else _ghostInfo.GhostSetedUp += SetUp;

        GhostAttackCheckCD = new WaitForSeconds(_ghostAttackCheckCD);
    }


    private IEnumerator CheckForAttackInum()
    {
        while (true)
        {
            CheckForAttack();
            yield return GhostAttackCheckCD;
        }
    }

    private void CheckForAttack()
    {
        
    }

    private void SetUp()
    {
        _playerSanity = _ghostInfo.PlayerSanity;
    }

}
