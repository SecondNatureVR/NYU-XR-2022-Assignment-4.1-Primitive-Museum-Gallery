using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
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

    public void ToggleDoor()
    {
        animator.SetBool("isOpen", !animator.GetBool("isOpen"));
    }
}
