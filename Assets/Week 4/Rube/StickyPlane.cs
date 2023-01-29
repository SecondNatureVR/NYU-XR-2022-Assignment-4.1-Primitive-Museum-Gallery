using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class StickyPlane : MonoBehaviour
{
    [SerializeField] public float speed = 50f;
    private Transform stuckObj;
    private Vector3 offset;
    private void OnCollisionEnter(Collision collision)
    {
        var ragdoll = collision.gameObject.GetComponentInParent<StableFirst>().transform;
        ragdoll.parent = transform;
        stuckObj = collision.gameObject.transform;
        offset = stuckObj.position - collision.GetContact(0).point;
    }

    public void Update()
    {
        if (stuckObj != null)
        {
            stuckObj.transform.position = transform.position + offset;
            transform.Translate(transform.forward * Time.deltaTime * speed);
        }
    }
}
