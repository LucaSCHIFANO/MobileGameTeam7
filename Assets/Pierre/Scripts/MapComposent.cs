using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MapComposent : MonoBehaviour
{
    public float position = 1;

    public static bool MapOpen = false;
    public GameObject MapUI;

    private int RandomRoom;

    public GameObject[] TypeRoom;
    public GameObject Enemy;
    public GameObject Rest;
    public GameObject Treasure;
    public GameObject Boss;

    public Transform salle1;
    public Transform salle2;
    public Transform salle2H;
    public Transform salle2B;
    public Transform salle3;
    public Transform salle3H;
    public Transform salle3B;
    public Transform salle4;
    public Transform salle4H;
    public Transform salle4B;
    public Transform salle5;
    public Transform salle6H;
    public Transform salle6B;
    public Transform salle7;
    public Transform salle7H;
    public Transform salle7B;
    public Transform salle8H;
    public Transform salle8B;
    public Transform salle9;
    public Transform salle10;

    private int ToutLOrDuCaptain = 0;
    private int LaTaverneDuCaptain = 0;

    public GameObject winScreen;
    public TextMeshProUGUI actualScore;
    public TextMeshProUGUI highscore;
    public TextMeshProUGUI newHighscore;


    public TextMeshProUGUI infoText;

    public GameObject fade;

    private static MapComposent _instance = null;

    public static MapComposent Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        MapUI.SetActive(false);
        //Opening();
    }


    private void Update()
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
            Check();
            //OnChildClick(salle7H);
        }

    }

    public void Check()
    {
        if (position == 10)
        {
            //var room1 = Instantiate(Enemy, salle1.position, salle1.rotation, salle1.transform);
            Create(Enemy, salle1, 10, 0);

            MapComposent.Instance.disableOldBouton();

            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
            }
            Create(TypeRoom[RandomRoom], salle2, 21, 1);
            if (TypeRoom[RandomRoom] == Treasure)
            {
                OnLATrouve();
            }
            //var room2 = Instantiate(TypeRoom[RandomRoom], salle2.position, salle2.rotation, salle2.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
            }
            Create(TypeRoom[RandomRoom], salle2H, 20, 1);
            if (TypeRoom[RandomRoom] == Treasure)
            {
                OnLATrouve();
            }
            //var room2H = Instantiate(TypeRoom[RandomRoom], salle2H.position, salle2H.rotation, salle2H.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
            }
            Create(TypeRoom[RandomRoom], salle2B, 22, 1);
            if (TypeRoom[RandomRoom] == Treasure)
            {
                OnLATrouve();
            }
            //var room2B = Instantiate(TypeRoom[RandomRoom], salle2B.position, salle2B.rotation, salle2B.transform);
        }

        else if (position == 20)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
            }
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Enemy;
            Create(TypeRoom[RandomRoom], salle3, 31, 2);
            if (TypeRoom[RandomRoom] == Treasure)
            {
                OnLATrouve();
            }
            //var room3 = Instantiate(TypeRoom[RandomRoom], salle3.position, salle3.rotation, salle3.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
            }
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Enemy;
            Create(TypeRoom[RandomRoom], salle3H, 30, 2);
            if (TypeRoom[RandomRoom] == Treasure)
            {
                OnLATrouve();
            }
            //var room3H = Instantiate(TypeRoom[RandomRoom], salle3H.position, salle3H.rotation, salle3H.transform);
        }
        else if (position == 21)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
            }
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Enemy;
            Create(TypeRoom[RandomRoom], salle3B, 32, 2);
            if (TypeRoom[RandomRoom] == Treasure)
            {
                OnLATrouve();
            }
            //var room3B = Instantiate(TypeRoom[RandomRoom], salle3B.position, salle3B.rotation, salle3B.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
            }
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Enemy;
            Create(TypeRoom[RandomRoom], salle3H, 30, 2);
            if (TypeRoom[RandomRoom] == Treasure)
            {
                OnLATrouve();
            }
            //var room3H = Instantiate(TypeRoom[RandomRoom], salle3H.position, salle3H.rotation, salle3H.transform);
        }
        else if (position == 22)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
            }
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Enemy;
            Create(TypeRoom[RandomRoom], salle3, 31, 2);
            if (TypeRoom[RandomRoom] == Treasure)
            {
                OnLATrouve();
            }
            //var room3 = Instantiate(TypeRoom[RandomRoom], salle3.position, salle3.rotation, salle3.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
            }
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Enemy;
            Create(TypeRoom[RandomRoom], salle3B, 32, 2);
            if (TypeRoom[RandomRoom] == Treasure)
            {
                OnLATrouve();
            }
            //var room3B = Instantiate(TypeRoom[RandomRoom], salle3B.position, salle3B.rotation, salle3B.transform);
        }

        else if (position == 30)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
            }
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Enemy;
                Create(TypeRoom[RandomRoom], salle4H, 40, 3);
            }
            else if (ToutLOrDuCaptain == 0 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle4H, 40, 3);
            }
            else if (ToutLOrDuCaptain == 0)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle4H, 40, 3);
            }
            else
            Create(TypeRoom[RandomRoom], salle4H, 40, 3);

            //var room4 = Instantiate(TypeRoom[RandomRoom], salle4.position, salle4.rotation, salle4.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
            }
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Enemy;
                Create(TypeRoom[RandomRoom], salle4, 41, 3);
            }
            else if (ToutLOrDuCaptain == 0 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle4, 41, 3);
            }
            else if (ToutLOrDuCaptain == 0)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle4, 41, 3);
            }
            else
                Create(TypeRoom[RandomRoom], salle4, 41, 3);
            //var room4H = Instantiate(TypeRoom[RandomRoom], salle4H.position, salle4H.rotation, salle4H.transform);
        }
        else if (position == 31)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
            }
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Enemy;
                Create(TypeRoom[RandomRoom], salle4B, 42, 3);
            }
            else if (ToutLOrDuCaptain == 0 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle4B, 42, 3);
            }
            else if (ToutLOrDuCaptain == 0)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle4B, 42, 3);
            }
            else
                Create(TypeRoom[RandomRoom], salle4B, 42, 3);
            //var room4B = Instantiate(TypeRoom[RandomRoom], salle4B.position, salle4B.rotation, salle4B.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
            }
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Enemy;
                Create(TypeRoom[RandomRoom], salle4H, 40, 3);
            }
            else if (ToutLOrDuCaptain == 0 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle4H, 40, 3);
            }
            else if (ToutLOrDuCaptain == 0)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle4H, 40, 3);
            }
            else
                Create(TypeRoom[RandomRoom], salle4H, 40, 3);
            //var room4H = Instantiate(TypeRoom[RandomRoom], salle4H.position, salle4H.rotation, salle4H.transform);
        }
        else if (position == 32)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
            }
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Enemy;
                Create(TypeRoom[RandomRoom], salle4, 41, 3);
            }
            else if (ToutLOrDuCaptain == 0 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle4, 41, 3);
            }
            else if (ToutLOrDuCaptain == 0)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle4, 41, 3);
            }
            else
                Create(TypeRoom[RandomRoom], salle4, 41, 3);
            //var room4 = Instantiate(TypeRoom[RandomRoom], salle4.position, salle4.rotation, salle4.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
            }
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Enemy;
                Create(TypeRoom[RandomRoom], salle4B, 42, 3);
            }
            else if (ToutLOrDuCaptain == 0 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle4B, 42, 3);
            }
            else if (ToutLOrDuCaptain == 0)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle4B, 42, 3);
            }
            else
                Create(TypeRoom[RandomRoom], salle4B, 42, 3);
            //var room4B = Instantiate(TypeRoom[RandomRoom], salle4B.position, salle4B.rotation, salle4B.transform);
        }

        else if (position == 40)
        {
            Create(Boss, salle5, 50, 4);
            //var room5 = Instantiate(Boss, salle5.position, salle5.rotation, salle5.transform);
        }
        else if (position == 41)
        {
            Create(Boss, salle5, 50, 4);
            //var room5 = Instantiate(Boss, salle5.position, salle5.rotation, salle5.transform);
        }
        else if (position == 42)
        {
            Create(Boss, salle5, 50, 4);
            //var room5 = Instantiate(Boss, salle5.position, salle5.rotation, salle5.transform);
        }

        else if (position == 50)
        {
            ToutLOrDuCaptain = 0;
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TerreEnVue();
            }
            else
                if (TypeRoom[RandomRoom] == Treasure)
            {
                OnLATrouve();
            }
            Create(TypeRoom[RandomRoom], salle6B, 61, 5);
            //var room6B = Instantiate(TypeRoom[RandomRoom], salle6B.position, salle6B.rotation, salle6B.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TerreEnVue();
            }
            else
                if (TypeRoom[RandomRoom] == Treasure)
            {
                OnLATrouve();
            }
            Create(TypeRoom[RandomRoom], salle6H, 60, 5);
            //var room6H = Instantiate(TypeRoom[RandomRoom], salle6H.position, salle6H.rotation, salle6H.transform);
        }

        else if (position == 60)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Enemy;
                Create(TypeRoom[RandomRoom], salle7, 71, 6);
            }
            else if (ToutLOrDuCaptain == 0 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle7, 71, 6);
            }
            else if (ToutLOrDuCaptain == 0)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle7, 71, 6);
            }
            else
                Create(TypeRoom[RandomRoom], salle7, 71, 6);

            if (TypeRoom[RandomRoom] == Rest)
            {
                TerreEnVue();
            }
            //var room7 = Instantiate(TypeRoom[RandomRoom], salle7.position, salle7.rotation, salle7.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Enemy;
                Create(TypeRoom[RandomRoom], salle7H, 70, 6);
            }
            else if (ToutLOrDuCaptain == 0 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle7H, 70, 6);
            }
            else if (ToutLOrDuCaptain == 0)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle7H, 70, 6);
            }
            else
                Create(TypeRoom[RandomRoom], salle7H, 70, 6);

            if (TypeRoom[RandomRoom] == Rest)
            {
                TerreEnVue();
            }
            //var room7H = Instantiate(TypeRoom[RandomRoom], salle7H.position, salle7H.rotation, salle7H.transform);
        }
        else if (position == 61)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Enemy;
                Create(TypeRoom[RandomRoom], salle7, 71, 6);
            }
            else if (ToutLOrDuCaptain == 0 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle7, 71, 6);
            }
            else if (ToutLOrDuCaptain == 0)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle7, 71, 6);
            }
            else
                Create(TypeRoom[RandomRoom], salle7, 71, 6);
            if (TypeRoom[RandomRoom] == Rest)
            {
                TerreEnVue();
            }
            //var room7 = Instantiate(TypeRoom[RandomRoom], salle7.position, salle7.rotation, salle7.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Enemy;
                Create(TypeRoom[RandomRoom], salle7B, 72, 6);
            }
            else if (ToutLOrDuCaptain == 0 && TypeRoom[RandomRoom] == Treasure)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle7B, 72, 6);
            }
            else if (ToutLOrDuCaptain == 0)
            {
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle7B, 72, 6);
            }
            else
                Create(TypeRoom[RandomRoom], salle7B, 72, 6);

            if (TypeRoom[RandomRoom] == Rest)
            {
                TerreEnVue();
            }
            //var room7B = Instantiate(TypeRoom[RandomRoom], salle7B.position, salle7B.rotation, salle7B.transform);
        }

        else if (position == 70)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (LaTaverneDuCaptain == 1 && TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
                Create(TypeRoom[RandomRoom], salle8H, 80, 7);
            }
            else if (LaTaverneDuCaptain == 0 && TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Rest;
                Create(TypeRoom[RandomRoom], salle8H, 80, 7);
            }
            else if (LaTaverneDuCaptain == 0)
            {
                TypeRoom[RandomRoom] = Rest;
                Create(TypeRoom[RandomRoom], salle8H, 80, 7);
            }
            else
                Create(TypeRoom[RandomRoom], salle8H, 80, 7);
            //var room8B = Instantiate(Rest, salle8B.position, salle8B.rotation, salle8B.transform);
            //var room8H = Instantiate(Treasure, salle8H.position, salle8H.rotation, salle8H.transform);
        }
        else if (position == 71)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (LaTaverneDuCaptain == 1 && TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
                Create(TypeRoom[RandomRoom], salle8B, 81, 7);
            }
            else if (LaTaverneDuCaptain == 0 && TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Rest;
                Create(TypeRoom[RandomRoom], salle8B, 81, 7);
            }
            else if (LaTaverneDuCaptain == 0)
            {
                TypeRoom[RandomRoom] = Rest;
                Create(TypeRoom[RandomRoom], salle8B, 81, 7);
            }
            else
                Create(TypeRoom[RandomRoom], salle8B, 81, 7);

            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (LaTaverneDuCaptain == 1 && TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
                Create(TypeRoom[RandomRoom], salle8H, 80, 7);
            }
            else if (LaTaverneDuCaptain == 0 && TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Rest;
                Create(TypeRoom[RandomRoom], salle8H, 80, 7);
            }
            else if (LaTaverneDuCaptain == 0)
            {
                TypeRoom[RandomRoom] = Rest;
                Create(TypeRoom[RandomRoom], salle8H, 80, 7);
            }
            else
                Create(TypeRoom[RandomRoom], salle8H, 80, 7);
            
            //var room8B = Instantiate(Rest, salle8B.position, salle8B.rotation, salle8B.transform);
            //var room8H = Instantiate(Treasure, salle8H.position, salle8H.rotation, salle8H.transform);
        }
        else if (position == 72)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (LaTaverneDuCaptain == 1 && TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Enemy;
                Create(TypeRoom[RandomRoom], salle8B, 81, 7);
            }
            else if (LaTaverneDuCaptain == 0 && TypeRoom[RandomRoom] == Rest)
            {
                TypeRoom[RandomRoom] = Rest;
                Create(TypeRoom[RandomRoom], salle8B, 81, 7);
            }
            else if (LaTaverneDuCaptain == 0)
            {
                TypeRoom[RandomRoom] = Rest;
                Create(TypeRoom[RandomRoom], salle8B, 81, 7);
            }
            else
                Create(TypeRoom[RandomRoom], salle8B, 81, 7);
            //var room8B = Instantiate(Rest, salle8B.position, salle8B.rotation, salle8B.transform);
            //var room8H = Instantiate(Treasure, salle8H.position, salle8H.rotation, salle8H.transform);
        }

        else if (position == 80)
        {
            Create(Enemy, salle9, 90, 8);
            //var room9 = Instantiate(Enemy, salle9.position, salle9.rotation, salle9.transform);
        }
        else if (position == 81)
        {
            Create(Enemy, salle9, 90, 8);
            //var room9 = Instantiate(Enemy, salle9.position, salle9.rotation, salle9.transform);
        }

        else if (position == 90)
        {
            Create(Boss, salle10, 100, 9);
            //var room10 = Instantiate(Boss, salle10.position, salle10.rotation, salle10.transform);

        }
    }

    public void Create(GameObject TypeSalle, Transform Emplacement, int position, int progression)
    {
        GameObject NewRoom = (GameObject)Instantiate(TypeSalle, Emplacement.position, Quaternion.identity, Emplacement.transform);
        NewRoom.GetComponent<BoutonInfo>().positionMap = position;
        NewRoom.GetComponent<BoutonInfo>().progression = progression;

        NewRoom.GetComponent<Button>().interactable = true;
    }

    public void OnLATrouve()
    {
        ToutLOrDuCaptain = 1;
    }

   public void TerreEnVue()
    {
        LaTaverneDuCaptain = 1;
    }

    public void disableOldBouton()
    {
        var allBoutons = GameObject.FindGameObjectsWithTag("BoutonLevel");

        foreach (var bouton in allBoutons)
        {
            bouton.GetComponent<Button>().interactable = false;
        }
    }

    public void UpdatePosition(Transform NomSalle)
    {
        position++;
    }

    public void Opening()
    {
        Instantiate(fade, transform.position, transform.rotation, gameObject.transform);
        StartCoroutine("waitforopen");
    }

    private IEnumerator waitforopen()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        MapUI.SetActive(true);
        Time.timeScale = 0f;
        MapOpen = true;
        AudioManager.Instance.Play("GlobalMap");

        if (position == 100)
        {
            MapUI.SetActive(false);
            winScreen.SetActive(true);
            actualScore.text = "Number of turn : " + PhaseManager.Instance.numberOfTurn;

           /* if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
            {*/
                Social.ReportProgress(GPGSIds.achievement_end_of_the_road, 100.0f, null);
                GooglePlayGames.PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_why_not, 1, null); //2
            //}

            if (!CharacterManager.Instance.isHealed)
            {
                /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                {*/
                    Social.ReportProgress(GPGSIds.achievement_god_among_gods, 100.0f, null);
                //}
            }

            if (PhaseManager.Instance.numberOfTurn < PhaseManager.Instance.numberOfTurnRecord || PhaseManager.Instance.numberOfTurnRecord == 0)
            {
                PlayerPrefs.SetInt("NumberOfTurn", PhaseManager.Instance.numberOfTurn);

                highscore.text = "Highscore :" + PlayerPrefs.GetInt("NumberOfTurn").ToString();
                newHighscore.gameObject.SetActive(true);

                /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                {*/
                    Social.ReportScore(PhaseManager.Instance.numberOfTurn, GPGSIds.leaderboard_best_time, (success) =>
                    {
                        if (!success) Debug.LogError("Unable to post highScore");
                    });
               // }
            }
            else
            {
                highscore.text = "Highscore :" + PlayerPrefs.GetInt("NumberOfTurn").ToString();
                newHighscore.gameObject.SetActive(false);
            }

        }else if (position == 50)
        {
            /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
            {*/
                Social.ReportProgress(GPGSIds.achievement_halfway_through_hell, 100.0f, null);
            //}
        }
    }

    public void Closing()
    {
        Instantiate(fade, transform.position, transform.rotation, gameObject.transform);
        StartCoroutine("waitforclose");
    }

    private IEnumerator waitforclose()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        MapUI.SetActive(false);
        Time.timeScale = 1f;
        MapOpen = false;

        AudioManager.Instance.Stop("GlobalMap");
        AudioManager.Instance.Play("BattleMap1");

        Check();
        Grid.Instance.deleteMap(true);
        Camera.main.GetComponent<MoveCam>().replace();
    }

    public void fadeOutIn()
    {
        Instantiate(fade, transform.position, transform.rotation, gameObject.transform);
    }
}
