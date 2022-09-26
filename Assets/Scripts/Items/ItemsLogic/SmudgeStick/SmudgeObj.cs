using Ghosts.Mood;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmudgeObj : MonoBehaviour
{
    [SerializeField]
    private LayerMask _enemyLayer;

    private const float TriggerDistance = 3f;
    private void OnEnable()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, TriggerDistance, _enemyLayer);
        foreach (var hitCollider in hitColliders)
        {
            GhostMood ghostMood = hitCollider.GetComponent<GhostMood>();
            if (ghostMood) ghostMood.SmudgeEffect();

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GhostMood ghostMood = other.GetComponent<GhostMood>();
        if (ghostMood) ghostMood.SmudgeEffect();
    }
}
