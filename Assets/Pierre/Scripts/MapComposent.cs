using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            Create(Enemy, salle1, 10);
            
            MapComposent.Instance.disableOldBouton();

            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle2, 21);
            //var room2 = Instantiate(TypeRoom[RandomRoom], salle2.position, salle2.rotation, salle2.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle2H, 20);
            //var room2H = Instantiate(TypeRoom[RandomRoom], salle2H.position, salle2H.rotation, salle2H.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle2B, 22);
            //var room2B = Instantiate(TypeRoom[RandomRoom], salle2B.position, salle2B.rotation, salle2B.transform);
        }

        else if (position == 20)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Enemy;
            Create(TypeRoom[RandomRoom], salle3, 31);
            //var room3 = Instantiate(TypeRoom[RandomRoom], salle3.position, salle3.rotation, salle3.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Enemy;
            Create(TypeRoom[RandomRoom], salle3H, 30);
            //var room3H = Instantiate(TypeRoom[RandomRoom], salle3H.position, salle3H.rotation, salle3H.transform);
        }
        else if(position == 21)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Enemy;
            Create(TypeRoom[RandomRoom], salle3B, 32);
            //var room3B = Instantiate(TypeRoom[RandomRoom], salle3B.position, salle3B.rotation, salle3B.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Enemy;
            Create(TypeRoom[RandomRoom], salle3H, 30);
            //var room3H = Instantiate(TypeRoom[RandomRoom], salle3H.position, salle3H.rotation, salle3H.transform);
        }
        else if(position == 22)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Enemy;
            Create(TypeRoom[RandomRoom], salle3, 31);
            //var room3 = Instantiate(TypeRoom[RandomRoom], salle3.position, salle3.rotation, salle3.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Enemy;
            Create(TypeRoom[RandomRoom], salle3B, 32);
            //var room3B = Instantiate(TypeRoom[RandomRoom], salle3B.position, salle3B.rotation, salle3B.transform);
        }

        else if (position == 30)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Enemy;
            else if (ToutLOrDuCaptain == 0 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Treasure;
            else 
                Create(TypeRoom[RandomRoom], salle4, 41);
            //var room4 = Instantiate(TypeRoom[RandomRoom], salle4.position, salle4.rotation, salle4.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Enemy;
            else if (ToutLOrDuCaptain == 0 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Treasure;
            else
                Create(TypeRoom[RandomRoom], salle4H, 41);
            //var room4H = Instantiate(TypeRoom[RandomRoom], salle4H.position, salle4H.rotation, salle4H.transform);
        }
        else if (position == 31)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Enemy;
            else if (ToutLOrDuCaptain == 0 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Treasure;
            else
                Create(TypeRoom[RandomRoom], salle4B, 41);
            //var room4B = Instantiate(TypeRoom[RandomRoom], salle4B.position, salle4B.rotation, salle4B.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Enemy;
            else if (ToutLOrDuCaptain == 0 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Treasure;
            else
                Create(TypeRoom[RandomRoom], salle4H, 41);
            //var room4H = Instantiate(TypeRoom[RandomRoom], salle4H.position, salle4H.rotation, salle4H.transform);
        }
        else if (position == 32)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Enemy;
            else if (ToutLOrDuCaptain == 0 && TypeRoom[RandomRoom] == Treasure)
                TypeRoom[RandomRoom] = Treasure;
            else
                Create(TypeRoom[RandomRoom], salle4, 41);
            //var room4 = Instantiate(TypeRoom[RandomRoom], salle4.position, salle4.rotation, salle4.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            if (ToutLOrDuCaptain == 1 && TypeRoom[RandomRoom] == Treasure) { 
                TypeRoom[RandomRoom] = Enemy;
                Create(TypeRoom[RandomRoom], salle4B, 41);
            }
            else if (ToutLOrDuCaptain == 0 && TypeRoom[RandomRoom] == Treasure) { 
                TypeRoom[RandomRoom] = Treasure;
                Create(TypeRoom[RandomRoom], salle4B, 41);
            }
            else
            { Create(TypeRoom[RandomRoom], salle4B, 41); }
            //var room4B = Instantiate(TypeRoom[RandomRoom], salle4B.position, salle4B.rotation, salle4B.transform);
        }

        else if (position == 40)
        {
            Create(Boss, salle5, 50);
            //var room5 = Instantiate(Boss, salle5.position, salle5.rotation, salle5.transform);
        }
        else if (position == 41)
        {
            Create(Boss, salle5, 50);
            //var room5 = Instantiate(Boss, salle5.position, salle5.rotation, salle5.transform);
        }
        else if (position == 42)
        {
            Create(Boss, salle5, 50);
            //var room5 = Instantiate(Boss, salle5.position, salle5.rotation, salle5.transform);
        }

        else if (position == 50)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle6B, 61);
            //var room6B = Instantiate(TypeRoom[RandomRoom], salle6B.position, salle6B.rotation, salle6B.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle6H, 60);
            //var room6H = Instantiate(TypeRoom[RandomRoom], salle6H.position, salle6H.rotation, salle6H.transform);
        }

        else if (position == 60)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle7, 71);
            //var room7 = Instantiate(TypeRoom[RandomRoom], salle7.position, salle7.rotation, salle7.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle7H, 70);
            //var room7H = Instantiate(TypeRoom[RandomRoom], salle7H.position, salle7H.rotation, salle7H.transform);
        }
        else if (position == 61)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle7, 71);
            //var room7 = Instantiate(TypeRoom[RandomRoom], salle7.position, salle7.rotation, salle7.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle7B, 72);
            //var room7B = Instantiate(TypeRoom[RandomRoom], salle7B.position, salle7B.rotation, salle7B.transform);
        }

        else if (position == 70)
        {
            Create(Treasure, salle8H, 81);
            //var room8B = Instantiate(Rest, salle8B.position, salle8B.rotation, salle8B.transform);
            //var room8H = Instantiate(Treasure, salle8H.position, salle8H.rotation, salle8H.transform);
        }
        else if (position == 71)
        {
            Create(Rest, salle8B, 81);
            Create(Treasure, salle8H, 80);
            //var room8B = Instantiate(Rest, salle8B.position, salle8B.rotation, salle8B.transform);
            //var room8H = Instantiate(Treasure, salle8H.position, salle8H.rotation, salle8H.transform);
        }
        else if (position == 72)
        {
            Create(Rest, salle8B, 81);
            //var room8B = Instantiate(Rest, salle8B.position, salle8B.rotation, salle8B.transform);
            //var room8H = Instantiate(Treasure, salle8H.position, salle8H.rotation, salle8H.transform);
        }

        else if (position == 80)
        {
            Create(Enemy, salle9, 90);
            //var room9 = Instantiate(Enemy, salle9.position, salle9.rotation, salle9.transform);
        }
        else if (position == 81)
        {
            Create(Enemy, salle9, 90);
            //var room9 = Instantiate(Enemy, salle9.position, salle9.rotation, salle9.transform);
        }

        else if (position == 90)
        {
            Create(Boss, salle10, 100);
            //var room10 = Instantiate(Boss, salle10.position, salle10.rotation, salle10.transform);

        }

        else if (position == 100)
        {

        }
    }

    public void Create(GameObject TypeSalle, Transform Emplacement, int position)
    {
        GameObject NewRoom = (GameObject)Instantiate(TypeSalle, Emplacement.position, Quaternion.identity, Emplacement.transform);
        NewRoom.GetComponent<BoutonInfo>().positionMap = position;

        NewRoom.GetComponent<Button>().interactable = true;
    }

    public void OnLATrouve()
    {
        ToutLOrDuCaptain = 1;
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
