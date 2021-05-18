using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTp : MonoBehaviour
{
    public static bool MapOpen = false;
    public GameObject MapUI;

    private float position = 1;

    public GameObject Enemy;
    public GameObject Rest;
    public GameObject Treasure;
    public GameObject Boss;

    public GameObject salle1;
    public GameObject salle2;
    public GameObject salle2H;
    public GameObject salle2B;
    public GameObject salle3;
    public GameObject salle3H;
    public GameObject salle3B;
    public GameObject salle4;
    public GameObject salle4H;
    public GameObject salle4B;
    public GameObject salle5;
    public GameObject salle6H;
    public GameObject salle6B;
    public GameObject salle7;
    public GameObject salle7H;
    public GameObject salle7B;
    public GameObject salle8H;
    public GameObject salle8B;
    public GameObject salle9;
    public GameObject salle10;

    void Start()
    {
        Generate();
        MapUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (MapOpen)
                Opening();
            else
                Closing();
            if (MapOpen)
                Closing();
            else
                Opening();
        }

        if (position == 1)
        {
            //Instantiate(Enemy, salle1.position, salle1.rotation);

        }

        if (position == 2.0)
        {

        }
        if (position == 2.1)
        {

        }
        if (position == 2.2)
        {

        }

        if (position == 3.0)
        {

        }
        if (position == 3.1)
        {

        }
        if (position == 3.2)
        {

        }

        if (position == 4.0)
        {

        }
        if (position == 4.1)
        {

        }
        if (position == 4.2)
        {

        }

        if (position == 5)
        {

        }

        if (position == 6.0)
        {

        }
        if (position == 6.1)
        {

        }

        if (position == 7.0)
        {

        }
        if (position == 7.1)
        {

        }
        if (position == 7.2)
        {

        }

        if (position == 8.0)
        {

        }
        if (position == 8.1)
        {

        }

        if (position == 9.0)
        {

        }

        if (position == 10.0)
        {

        }
    }

    public void Generate()
    {

    }

    public void Opening()
    {
        MapUI.SetActive(true);
        Time.timeScale = 0f;
        MapOpen = true;
    }

    public void Closing()
    {
        MapUI.SetActive(false);
        Time.timeScale = 1f;
        MapOpen = false;
    }

}
