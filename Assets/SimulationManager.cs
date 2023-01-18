using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    [SerializeField] public float timeScale = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = timeScale;
    }
}
