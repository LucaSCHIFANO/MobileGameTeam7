using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    private void Start()
    {
        GameIsPaused = false;
            Resume();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Boutton();
        }
    }


    public void Boutton()
    {
        Debug.Log("Test");
        if (GameIsPaused)
            Resume();
        else
            Pause();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Confirm()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

