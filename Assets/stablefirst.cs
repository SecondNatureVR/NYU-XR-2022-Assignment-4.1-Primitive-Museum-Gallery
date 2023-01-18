using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StableFirst : MonoBehaviour
{
    [SerializeField] public float threshold = 50f;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var rb in GetComponentsInChildren<Rigidbody>()) {
            rb.sleepThreshold = threshold;
            rb.Sleep();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
