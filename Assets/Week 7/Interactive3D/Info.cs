using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Info : MonoBehaviour {
    [SerializeField] public GazeTarget gaze;
    private TextMeshProUGUI tmp;

    private void Start()
    {
       tmp = GetComponent<TextMeshProUGUI>(); 
    }
    // Update is called once per frame
    void Update()
    {
        if (gaze.target != null)
        {
            tmp.text = gaze.target.transform.name;
        } else
        {
            tmp.text = "";
        }
    }
}
