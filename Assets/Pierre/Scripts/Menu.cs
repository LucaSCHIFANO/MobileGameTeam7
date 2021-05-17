using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject creditsMenuUI;
    public GameObject MenuUI;

    private void Start()
    {
        MenuUI.SetActive(true);
        creditsMenuUI.SetActive(false);
    }

    public void BouttonJouer()
    {
        SceneManager.LoadScene("ScenePierre");
    }

    public void BouttonTuto()
    {
        SceneManager.LoadScene("Tuto");
    }

    public void BouttonCredits()
    {
        
    }

    public void BouttonOptions()
    {
        //SceneManager.LoadScene("Options");
    }

    public void BouttonQuitter()
    {
        Application.Quit();
    }

    public void CreditsActive()
    {
        MenuUI.SetActive(false);
        creditsMenuUI.SetActive(true);
    }

    public void CreditsDisable()
    {
        MenuUI.SetActive(true);
        creditsMenuUI.SetActive(false);
    }
}
