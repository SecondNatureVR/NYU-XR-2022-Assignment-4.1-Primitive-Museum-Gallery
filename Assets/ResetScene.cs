using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour 
{
    public void Reset() {
        Debug.Log("Reset Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
