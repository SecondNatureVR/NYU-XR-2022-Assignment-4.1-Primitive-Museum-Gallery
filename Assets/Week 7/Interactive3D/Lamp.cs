using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] private Light light;
    [SerializeField] private AudioSource audio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            gameObject.name = $"Turn Lamp {(light.gameObject.activeSelf ? "Off" : "On")}";
            other.GetComponent<PlayerAction>().actionTriggered.AddListener(ToggleLight);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            gameObject.name = "Lamp";
            other.GetComponent<PlayerAction>().actionTriggered.RemoveListener(ToggleLight);
        }
    }

    public void ToggleLight()
    {
        light.gameObject.SetActive(!light.gameObject.activeSelf);
        audio.Play();
        gameObject.name = $"Turn Lamp {(light.gameObject.activeSelf ? "Off" : "On")}";
    }
}
