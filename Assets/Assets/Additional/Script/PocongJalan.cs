using System.Collections;
using UnityEngine;

public class PocongJalan : MonoBehaviour
{
    public GameObject GoalsPoint;
    public GameObject Pocong;
    public AudioSource audioSource;
    public Light light;
    public bool isTrigger;
    private Animator pocongAnimator;
    public float speed;

    private Coroutine lightCoroutine;

    void Start()
    {
        pocongAnimator = GetComponent<Animator>();
        isTrigger = true;
        Pocong.SetActive(false);
        GoalsPoint.SetActive(false);
    }

    void Update()
    {
        if (isTrigger && Pocong.activeSelf)
        {
            Pocong.transform.position = Vector3.MoveTowards(Pocong.transform.position, GoalsPoint.transform.position, speed * Time.deltaTime);
            StopTrigger();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger)
        {
            if (other.gameObject.name == "hero(Clone)")
            {
                SetTrigger();
            }
        }
    }

    private void SetTrigger()
    {
        audioSource.Play();
        Pocong.SetActive(true);
        GoalsPoint.SetActive(true);
        light.intensity = 10;
        light.enabled = true;
        light.color = new Color(0.5f, 0f, 0f);

        // Mulai coroutine untuk menyalakan dan mematikan lampu secara acak
        if (lightCoroutine == null)
        {
            lightCoroutine = StartCoroutine(BlinkLight());
        }
    }

    private void StopTrigger()
    {
        if (Pocong.transform.position == GoalsPoint.transform.position)
        {
            Pocong.SetActive(false);
            GoalsPoint.SetActive(false);
            audioSource.Stop();
            light.color = new Color(1f, 1f, 1f);
            light.enabled = false;
            isTrigger = false;

            // Hentikan coroutine saat Pocong berhenti
            if (lightCoroutine != null)
            {
                StopCoroutine(lightCoroutine);
                lightCoroutine = null;
            }
        }
    }

    private IEnumerator BlinkLight()
    {
        while (true)
        {
            // Mendapatkan durasi acak untuk menunggu sebelum mengubah status lampu
            float randomDelay = Random.Range(0.1f, 0.3f);
            yield return new WaitForSeconds(randomDelay);

            light.enabled = !light.enabled; // Toggles the light on and off
        }
    }
}
