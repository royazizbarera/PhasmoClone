using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScarePocongSasajen : MonoBehaviour
{
    public GameObject JumpScarePocongImg;
    public GameObject Pocong;
    public AudioSource audioSource;
    public Light light;
    private bool isFirstTrigger = true;
    private bool done = false;

    void Start()
    {
        Pocong.SetActive(false);
        JumpScarePocongImg.SetActive(false);
        audioSource.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!done)
        {
            if (other.gameObject.name == "hero(Clone)")
            {
                if (isFirstTrigger)
                {
                    isFirstTrigger = false;
                }
                else
                {
                    TriggerJumpScare();
                    done = true;
                }
            }
        }
    }

    private void TriggerJumpScare()
    {
        audioSource.Play();
        StartCoroutine(EnablePocong());
        StartCoroutine(EnableImg());
        StartCoroutine(DisableImg());
    }

    private void StopTrigger()
    {
        Pocong.SetActive(false);
        audioSource.Stop();
        light.color = new Color(1f, 1f, 1f);
        light.enabled = false;
    }

    IEnumerator EnablePocong()
    {
        yield return new WaitForSeconds(1);
        Pocong.SetActive(true);
    }

    IEnumerator DisableImg()
    {
        yield return new WaitForSeconds(5);
        Pocong.SetActive(false);
        JumpScarePocongImg.SetActive(false);
        audioSource.Stop();
        light.color = new Color(1f, 1f, 1f);
        light.enabled = false;
        StopTrigger();
    }

    IEnumerator EnableImg()
    {
        yield return new WaitForSeconds(2.5f);
        JumpScarePocongImg.SetActive(true);
    }
}
