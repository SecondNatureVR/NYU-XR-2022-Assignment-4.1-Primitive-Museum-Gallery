using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] PauseMenu pauseMenu;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        pauseMenu.ResumeGame();
        gameObject.SetActive(false);
    }
}
