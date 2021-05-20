using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonInfo : MonoBehaviour
{
    public int idLevel;
    public int progression;
    public typeOfRoom tor;

    public enum typeOfRoom
    {
        ENEMY,
        REST,
        TREASURE,
        BOSS,
    }

    public void sendInfo()
    {
        if(tor == typeOfRoom.ENEMY)
        {
            Grid.Instance.levelID = idLevel;
            Grid.Instance.progress = progression;
            Grid.Instance.deleteMap();
            MapComposent.Instance.Closing();
        }
    }
}
