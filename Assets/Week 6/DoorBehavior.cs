using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] [Range(-180, 180)] public float openAngle;
    [SerializeField] [Range(0, 5)] private float duration = 1f;
    
    public bool isOpen = false;

    private float currentAngle { get => pivot.localEulerAngles.y; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerAction>().actionTriggered.AddListener(ToggleDoor);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerAction>().actionTriggered.RemoveListener(ToggleDoor);
    }
    void ToggleDoor()
    {
        StopAllCoroutines();
        if (isOpen)
        {
            StartCoroutine(SwingDoor(currentAngle, 0));
        }
        else
        {
            StartCoroutine(SwingDoor(currentAngle, openAngle));
        }
        isOpen = !isOpen;
    }

    public IEnumerator SwingDoor(float fromAngle, float toAngle)
    {
        Debug.Log($"Start {fromAngle} -> {toAngle}");
        float startTime = Time.time;
        float t = 0;
        while (t < 1) {
            t = Mathf.InverseLerp(startTime, startTime + duration, Time.time);
            pivot.rotation = Quaternion.AngleAxis(
                Mathf.LerpAngle(fromAngle, toAngle, t),
                Vector3.up
            );
            Debug.Log($"{currentAngle} @ t={t}");
            yield return null;
        }
    }
}
