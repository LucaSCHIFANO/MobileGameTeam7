using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject creditsMenuUI;
    public GameObject MenuUI;
    public GameObject OptionsUI;

    public GameObject fade;

    private void Start()
    {
        MenuUI.SetActive(true);
        creditsMenuUI.SetActive(false);
    }

    public void BouttonJouer()
    {
        Instantiate(fade, transform.position, transform.rotation, gameObject.transform);
        StartCoroutine("waitforthat");
    }

    private IEnumerator waitforthat()
    {
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene("NewSceneJul");
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

    public void googleTrophies()
    {
        /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
        {*/
            Social.ShowAchievementsUI();
        //}
    }

    public void googleLeaderBoard()
    {
        /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
        {*/
            Social.ShowLeaderboardUI();
        //}
    }
}
