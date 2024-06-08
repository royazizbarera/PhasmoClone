using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScarePocong : MonoBehaviour
{

    public GameObject JumpScarePocongImg;
    public GameObject Pocong;
    public AudioSource audioSource;
    public bool isTrigger;

    void Start()
    {
        isTrigger = true;
        Pocong.SetActive(false);
        JumpScarePocongImg.SetActive(false);
        audioSource.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isTrigger == true)
        {
            if(other.gameObject.name == "hero(Clone)")
            {
                audioSource.Play();
                StartCoroutine(EnablePocong());
                StartCoroutine(EnableImg());
                StartCoroutine(DisableImg());
            }
        }
    }

    IEnumerator EnablePocong()
    {
        yield return new WaitForSeconds(1);
        Pocong.SetActive(true);
        isTrigger = false;
    }

    IEnumerator DisableImg()
    {
        yield return new WaitForSeconds(5);
        Pocong.SetActive(false);
        JumpScarePocongImg.SetActive(false);
    }

    IEnumerator EnableImg()
    {
        yield return new WaitForSeconds(2.5f);
        JumpScarePocongImg.SetActive(true);
    }


}
