using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    [SerializeField] public float duration = 0.8f;
    [SerializeField] Animator theBoss;
    [SerializeField][Range(1, 300)] public float lerpMax;
    [SerializeField][Range(1, 2)] public float reverbCurve;
    private GameObject player;
    private bool isPlaying = false;
    private float dist;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audio.Play();
        audio.pitch = 0f;
        Toggle();
    }

    private void Update()
    {
        dist = Vector3.Distance(transform.position, player.transform.position);
        float t = Mathf.InverseLerp(10, 300, dist);
        float t2 = Mathf.InverseLerp(0, lerpMax, Mathf.Pow(dist,reverbCurve));
        float dryLevel = Mathf.Lerp(0, -5000, t);
        float roomLevel = Mathf.Lerp(-10000, 0, t2);
        audio.outputAudioMixerGroup.audioMixer.SetFloat("DryLevel", dryLevel);
        audio.outputAudioMixerGroup.audioMixer.SetFloat("RoomLevel", roomLevel);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.name = $"Turn Radio {(isPlaying ? "Off" : "On")}";
            other.GetComponent<PlayerAction>().actionTriggered.AddListener(Toggle);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            gameObject.name = "Radio";
            other.GetComponent<PlayerAction>().actionTriggered.RemoveListener(Toggle);
        }
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
        gameObject.name = $"Turn Radio {(isPlaying ? "Off" : "On")}";
    }

    public IEnumerator SetPitch(float fromPitch, float toPitch)
    {
        //Debug.Log($"Start {fromPitch} -> {toPitch}");
        float startTime = Time.time;
        float t = 0;
        while (t < 1) {
            t = Mathf.InverseLerp(startTime, startTime + duration, Time.time);
            var val = Mathf.Lerp(fromPitch, toPitch, t);
            audio.pitch = val;
            theBoss.speed = val > .2f ? val : 0;
            //Debug.Log($"{audio.pitch} @ t={t}");
            yield return null;
        }
    }
}
