using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    [SerializeField] public float duration = 0.8f;
    [SerializeField] Animator theBoss;
    private bool isPlaying = false;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
        audio.pitch = 0f;
        Toggle();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerAction>().actionTriggered.AddListener(Toggle);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerAction>().actionTriggered.RemoveListener(Toggle);
    }
    void Toggle()
    {
        StopAllCoroutines();
        if (isPlaying)
        {
            StartCoroutine(SetPitch(audio.pitch, 0));
        }
        else
        {
            StartCoroutine(SetPitch(audio.pitch, 1));
        }
        isPlaying = !isPlaying;
    }

    public IEnumerator SetPitch(float fromPitch, float toPitch)
    {
        Debug.Log($"Start {fromPitch} -> {toPitch}");
        float startTime = Time.time;
        float t = 0;
        while (t < 1) {
            t = Mathf.InverseLerp(startTime, startTime + duration, Time.time);
            var val = Mathf.Lerp(fromPitch, toPitch, t);
            audio.pitch = val;
            theBoss.speed = val > .2f ? val : 0;
            Debug.Log($"{audio.pitch} @ t={t}");
            yield return null;
        }
    }
}
