using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public float offset = 5;
    public float speed = 1;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float t = Mathf.Sin(Time.time * speed);
        transform.position = startPos + t * offset * transform.right;
    }
}
