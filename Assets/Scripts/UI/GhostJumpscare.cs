using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostJumpscare : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;

    private const string Jumpscare = "Jumpscare";
    void Start()
    {
        InvokeRepeating(nameof(CallJumpScare), 3f, 3f);
      //  Invoke(nameof(CallJumpScare) , 3f);
    }

    private void CallJumpScare()
    {
        _anim.SetTrigger(Jumpscare);
    }
}
