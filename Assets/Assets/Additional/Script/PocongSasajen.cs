using System.Collections;
using UnityEngine;

public class PocongSasajen : MonoBehaviour
{
    public GameObject Pocong;
    public GameObject TriggerJumpScare;
    public AudioSource audioSource;
    public Light light;
    public bool isTrigger;

    private Coroutine lightCoroutine;

    void Start()
    {
        isTrigger = true;
        Pocong.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger)
        {
            if (other.gameObject.name == "hero(Clone)")
            {
                SetTrigger();
                isTrigger = false;
            }
        }
    }

    private void SetTrigger()
    {
        audioSource.Play();
        StartCoroutine(ShowPocong());
    }

    private IEnumerator ShowPocong()
    {
        yield return new WaitForSeconds(20);  // Delay before showing Pocong
        Pocong.SetActive(true);
        light.intensity = 4;
        light.enabled = true;
        light.color = new Color(0.5f, 0f, 0f);

        if (lightCoroutine == null)
        {
            lightCoroutine = StartCoroutine(BlinkLight());
        }
    }

    private void StopTrigger()
    {
        Pocong.SetActive(false);
        audioSource.Stop();
        light.color = new Color(1f, 1f, 1f);
        light.enabled = false;
        isTrigger = false;

        if (lightCoroutine != null)
        {
            StopCoroutine(lightCoroutine);
            lightCoroutine = null;
        }
    }

    private IEnumerator BlinkLight()
    {
        for (int i = 0; i < 1000; i++)
        {
            float randomDelay = Random.Range(0.01f, 0.2f);
            yield return new WaitForSeconds(randomDelay);
            light.enabled = !light.enabled;
            
        }
    }
}
