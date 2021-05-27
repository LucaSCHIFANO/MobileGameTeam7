using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public Stats.ELEMENT elemSave;
    public bool firstTime;

    public bool onlyOneElem = true;

    [Header("Red Fire")]
    public int f2;
    public int f3;
    public int f4;
    public int f5;

    [Header("Blue Water")]
    public int w2;
    public int w3;
    public int w4;
    public int w5;

    [Header("Green Earth")]
    public int e2;
    public int e3;
    public int e4;
    public int e5;

    private static ComboSystem _instance = null;

    public static ComboSystem Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }



    public void resetSave()
    {
        onlyOneElem = true;
        firstTime = false;
    }


    public void comboEffect(Stats.ELEMENT elem, int number)
    {
        if (!firstTime)
        {
            elemSave = elem;
            firstTime = true;
        }
        else
        {
            if (elem != elemSave)
            {
                onlyOneElem = false;
            }
        }


        var player = CharacterManager.Instance.currentPlayer;
        player.stats.boostAtt = 0;
        player.stats.boostDef = 0;
        player.stats.boostAP = 0;
        switch (elem)
        {
            case Stats.ELEMENT.NORMAL:
                break;
            case Stats.ELEMENT.RED:
                if (number >= 2)
                {
                    player.stats.boostAtt = f2;

                    if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                    {
                        Social.ReportProgress(GPGSIds.achievement_the_way_of_fire, 100.0f, null);
                    }
                }
                if (number >= 3)
                {
                    player.stats.boostAtt = f3;
                }
                if (number >= 4)
                {
                    player.stats.boostAtt = f4;
                }
                if (number >= 5)
                {
                    player.stats.boostAtt = f5;

                    if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                    {
                        Social.ReportProgress(GPGSIds.achievement_endless_destruction, 100.0f, null);
                    }
                }
                break;



            case Stats.ELEMENT.BLUE:
                if(number >= 2)
                {
                    player.stats.boostDef = w2;

                    if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                    {
                        Social.ReportProgress(GPGSIds.achievement_the_way_of_water, 100.0f, null);
                    }
                }
                if (number >= 3)
                {
                    player.stats.boostDef = w3;
                }
                if (number >= 4)
                {
                    player.stats.boostDef = w4;
                }
                if (number >= 5)
                {
                    player.stats.boostDef = w5;

                    if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                    {
                        Social.ReportProgress(GPGSIds.achievement_impregnable_fortress, 100.0f, null);
                    }
                }
                break;



            case Stats.ELEMENT.GREEN:
                if (number >= 2)
                {
                    player.stats.boostAP = e2;

                    if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                    {
                        Social.ReportProgress(GPGSIds.achievement_the_way_of_nature, 100.0f, null);
                    }
                }
                if (number >= 3)
                {
                    player.stats.boostAP = e3;
                }
                if (number >= 4)
                {
                    player.stats.boostAP = e4;
                }
                if (number >= 5)
                {
                    player.stats.boostAP = e5;

                    if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                    {
                        Social.ReportProgress(GPGSIds.achievement_speed_of_light, 100.0f, null);
                    }
                }

                player.stats.actionPoint += (player.stats.boostAP - player.stats.boostAPUsed);
                player.stats.boostAPUsed = player.stats.boostAP;

                break;
            default:
                break;
        }
    }
}
