using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSFX : MonoBehaviour
{
    public AudioSource playSound;
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "hero(Clone)")
        {
            playSound.Play();
        }
    }
}
