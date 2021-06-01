using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;

    private void Start()
    {
        Resume();
        optionMenuUI.SetActive(false);
    }

    public void Boutton()
    {
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
        var mc = MapComposent.Instance;
        Instantiate(mc.fade, transform.position, transform.rotation, gameObject.transform);
        StartCoroutine("waitforclose");
    }


    public void Confirm()
    {
        Time.timeScale = 1f;
        var mc = MapComposent.Instance;
        Instantiate(mc.fade, transform.position, transform.rotation, gameObject.transform);
        StartCoroutine("waitforclose");
    }


    private IEnumerator waitforclose()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        SceneManager.LoadScene("MainMenu");
    }

        

    public void OptionsActive()
    {
        pauseMenuUI.SetActive(false);
        optionMenuUI.SetActive(true);
    }

    public void OptionsEnactive()
    {
        optionMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}

