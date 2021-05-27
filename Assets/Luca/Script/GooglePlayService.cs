using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;

public class GooglePlayService : MonoBehaviour
{

    public bool isConnectedToGooglePlayServices;



    private static GooglePlayService _instance = null;

    public static GooglePlayService Instance
    {
        get => _instance;
    }

    private void Awake()
    {
        _instance = this;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("ggplay");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    public void Start()
    {
        SignInToGooglePlayServices();
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
