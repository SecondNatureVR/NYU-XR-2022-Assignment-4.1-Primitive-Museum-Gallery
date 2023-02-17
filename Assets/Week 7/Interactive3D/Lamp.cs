using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] private Light light;
    [SerializeField] private AudioSource audio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerAction>().actionTriggered.AddListener(ToggleLight);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerAction>().actionTriggered.RemoveListener(ToggleLight);
    }

    public void ToggleLight()
    {
        light.gameObject.SetActive(!light.gameObject.activeSelf);
        audio.Play();
    }
}
