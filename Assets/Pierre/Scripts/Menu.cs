using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;

public class Menu : MonoBehaviour
{
    public GameObject creditsMenuUI;
    public GameObject MenuUI;

    public bool isConnectedToGooglePlayServices;


    private void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    private void Start()
    {
        MenuUI.SetActive(true);
        creditsMenuUI.SetActive(false);

        SignInToGooglePlayServices();
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


    public void SignInToGooglePlayServices()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
            switch (result)
            {
                case SignInStatus.Success:
                    isConnectedToGooglePlayServices = true;
                    break;
                default:
                    isConnectedToGooglePlayServices = false;
                    break;
            }
        });
    }
}
