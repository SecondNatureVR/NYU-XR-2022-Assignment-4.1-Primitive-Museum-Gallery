using System.Collections;
using System.Collections.Generic;
using cakeslice;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using TMPro;

public class GazeTarget : MonoBehaviour
{
    [SerializeField] public Camera camera;
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 30.0f, 0b11111011, QueryTriggerInteraction.Ignore))
        {
            if (target != hit.collider.gameObject)
            {
                Clear();
                target = hit.collider.gameObject;
                int count = 0;
                foreach (var r in target.GetComponentsInChildren<Renderer>()) {
                    var outline = r.gameObject.GetComponent<Outline>();
                    if (outline == null)
                    {
                        outline = r.gameObject.AddComponent<Outline>();
                    }
                    outline.enabled = true;
                    count++;
                }
                Debug.Log($"{count} outlines activated on {target.name}");
            }
        } else {
            Clear();
        }
    }

    public void Clear()
    {
        if (target == null) return;
        foreach (var outline in target.GetComponentsInChildren<Outline>()) {
            outline.enabled = false;
        }
        target = null;
    }
}
