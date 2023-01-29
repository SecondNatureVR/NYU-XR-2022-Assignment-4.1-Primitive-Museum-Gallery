using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDisplay : MonoBehaviour
{
    [SerializeField] float timeScale = 1f;
    private float randomSeed;

    // Start is called before the first frame update
    void Start()
    {
        randomSeed = Random.value * 360;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time * timeScale + randomSeed;
        transform.rotation = Quaternion.Euler(t, t, t);
    }
}
