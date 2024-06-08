using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salam : MonoBehaviour
{

    public AudioSource audioSource;
    public bool isTrigger;

    void Start()
    {
        isTrigger = true;
        audioSource.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger == true)
        {
            if (other.gameObject.name == "hero(Clone)")
            {
                audioSource.Play();
            }
        }
    }


}
