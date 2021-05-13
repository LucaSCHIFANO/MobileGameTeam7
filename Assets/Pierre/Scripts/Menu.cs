using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
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
        SceneManager.LoadScene("Credits");
    }

    public void BouttonOptions()
    {
        //SceneManager.LoadScene("Options");
    }

    public void BouttonQuitter()
    {
        Application.Quit();
    }
}
