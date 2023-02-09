using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    private PlayerMovement target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerAction>().actionTriggered.AddListener(Launch);
            target = other.GetComponent<PlayerMovement>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerAction>().actionTriggered.RemoveListener(Launch);
            target = null;
        }
    }

    public void Launch()
    {
        target?.AddForce(Vector3.up * 10f);
    }
}
