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
    public GameObject OptionsUI;

    public GameObject fade;

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
        Instantiate(fade, transform.position, transform.rotation, gameObject.transform);
        StartCoroutine("waitforthat");
    }

    private IEnumerator waitforthat()
    {
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene("SceneLuca");
    }

    public void BouttonTuto()
    {
        SceneManager.LoadScene("Tuto");
    }

    public void BouttonQuitter()
    {
        Application.Quit();
    }

    public void CreditsActive()
    {
        MenuUI.SetActive(false);
        creditsMenuUI.SetActive(true);
        OptionsUI.SetActive(false);
    }

    public void CreditsDisable()
    {
        MenuUI.SetActive(false);
        creditsMenuUI.SetActive(false);
        OptionsUI.SetActive(true);
    }

    public void OptionDisable()
    {
        MenuUI.SetActive(true);
        creditsMenuUI.SetActive(false);
        OptionsUI.SetActive(false);
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
