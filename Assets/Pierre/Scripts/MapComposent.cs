using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (position == 1)
        {
            //var room1 = Instantiate(Enemy, salle1.position, salle1.rotation, salle1.transform);
            Create(Enemy, salle1);

            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle2);
            //var room2 = Instantiate(TypeRoom[RandomRoom], salle2.position, salle2.rotation, salle2.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle2H);
            //var room2H = Instantiate(TypeRoom[RandomRoom], salle2H.position, salle2H.rotation, salle2H.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle2B);
            //var room2B = Instantiate(TypeRoom[RandomRoom], salle2B.position, salle2B.rotation, salle2B.transform);
        }

        if (position == 2.0)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle3);
            //var room3 = Instantiate(TypeRoom[RandomRoom], salle3.position, salle3.rotation, salle3.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle3H);
            //var room3H = Instantiate(TypeRoom[RandomRoom], salle3H.position, salle3H.rotation, salle3H.transform);
        }
        if (position == 2.1)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle3B);
            //var room3B = Instantiate(TypeRoom[RandomRoom], salle3B.position, salle3B.rotation, salle3B.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle3H);
            //var room3H = Instantiate(TypeRoom[RandomRoom], salle3H.position, salle3H.rotation, salle3H.transform);
        }
        if (position == 2.2)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle3);
            //var room3 = Instantiate(TypeRoom[RandomRoom], salle3.position, salle3.rotation, salle3.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle3B);
            //var room3B = Instantiate(TypeRoom[RandomRoom], salle3B.position, salle3B.rotation, salle3B.transform);
        }

        if (position == 3.0)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle4);
            //var room4 = Instantiate(TypeRoom[RandomRoom], salle4.position, salle4.rotation, salle4.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle4H);
            //var room4H = Instantiate(TypeRoom[RandomRoom], salle4H.position, salle4H.rotation, salle4H.transform);
        }
        if (position == 3.1)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle4B);
            //var room4B = Instantiate(TypeRoom[RandomRoom], salle4B.position, salle4B.rotation, salle4B.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle4H);
            //var room4H = Instantiate(TypeRoom[RandomRoom], salle4H.position, salle4H.rotation, salle4H.transform);
        }
        if (position == 3.2)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle4);
            //var room4 = Instantiate(TypeRoom[RandomRoom], salle4.position, salle4.rotation, salle4.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle4B);
            //var room4B = Instantiate(TypeRoom[RandomRoom], salle4B.position, salle4B.rotation, salle4B.transform);
        }

        if (position == 4.0)
        {
            Create(Boss, salle5);
            //var room5 = Instantiate(Boss, salle5.position, salle5.rotation, salle5.transform);
        }
        if (position == 4.1)
        {
            Create(Boss, salle5);
            //var room5 = Instantiate(Boss, salle5.position, salle5.rotation, salle5.transform);
        }
        if (position == 4.2)
        {
            Create(Boss, salle5);
            //var room5 = Instantiate(Boss, salle5.position, salle5.rotation, salle5.transform);
        }

        if (position == 5)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle6B);
            //var room6B = Instantiate(TypeRoom[RandomRoom], salle6B.position, salle6B.rotation, salle6B.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle6H);
            //var room6H = Instantiate(TypeRoom[RandomRoom], salle6H.position, salle6H.rotation, salle6H.transform);
        }

        if (position == 6.0)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle7);
            //var room7 = Instantiate(TypeRoom[RandomRoom], salle7.position, salle7.rotation, salle7.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle7H);
            //var room7H = Instantiate(TypeRoom[RandomRoom], salle7H.position, salle7H.rotation, salle7H.transform);
        }
        if (position == 6.1)
        {
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle7);
            //var room7 = Instantiate(TypeRoom[RandomRoom], salle7.position, salle7.rotation, salle7.transform);
            RandomRoom = Random.Range(0, TypeRoom.Length);
            Create(TypeRoom[RandomRoom], salle7B);
            //var room7B = Instantiate(TypeRoom[RandomRoom], salle7B.position, salle7B.rotation, salle7B.transform);
        }

        if (position == 7.0)
        {
            Create(TypeRoom[RandomRoom], salle8B);
            Create(TypeRoom[RandomRoom], salle8H);
            //var room8B = Instantiate(Rest, salle8B.position, salle8B.rotation, salle8B.transform);
            //var room8H = Instantiate(Treasure, salle8H.position, salle8H.rotation, salle8H.transform);
        }
        if (position == 7.1)
        {
            Create(TypeRoom[RandomRoom], salle8B);
            Create(TypeRoom[RandomRoom], salle8H);
            //var room8B = Instantiate(Rest, salle8B.position, salle8B.rotation, salle8B.transform);
            //var room8H = Instantiate(Treasure, salle8H.position, salle8H.rotation, salle8H.transform);
        }
        if (position == 7.2)
        {
            Create(TypeRoom[RandomRoom], salle8B);
            Create(TypeRoom[RandomRoom], salle8H);
            //var room8B = Instantiate(Rest, salle8B.position, salle8B.rotation, salle8B.transform);
            //var room8H = Instantiate(Treasure, salle8H.position, salle8H.rotation, salle8H.transform);
        }

        if (position == 8.0)
        {
            Create(TypeRoom[RandomRoom], salle9);
            //var room9 = Instantiate(Enemy, salle9.position, salle9.rotation, salle9.transform);
        }
        if (position == 8.1)
        {
            Create(TypeRoom[RandomRoom], salle9);
            //var room9 = Instantiate(Enemy, salle9.position, salle9.rotation, salle9.transform);
        }

        if (position == 9.0)
        {
            Create(TypeRoom[RandomRoom], salle10);
            //var room10 = Instantiate(Boss, salle10.position, salle10.rotation, salle10.transform);

        }

        if (position == 10.0)
        {

        }
    }

    public void Create(GameObject TypeSalle, Transform Emplacement)
    {
        GameObject NewRoom = (GameObject)Instantiate(TypeSalle, Emplacement.position, Quaternion.identity, Emplacement.transform); 
    }

    public void TestSalle()
    {
        //mets ce que tu veux ici luca
    }

    public void CLIQUESFDP()
    {

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
