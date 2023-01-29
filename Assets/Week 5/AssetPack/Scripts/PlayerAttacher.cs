using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacher : MonoBehaviour
{
    [SerializeField] private Transform targetParent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = targetParent;
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
            other.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
