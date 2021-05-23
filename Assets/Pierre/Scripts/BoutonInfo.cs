using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BoutonInfo : MonoBehaviour
{
    public int idLevel;
    public int progression;

    public int positionMap;
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
        if (!MapComposent.Instance.infoText.gameObject.activeInHierarchy)
        {
            switch (tor)
            {
                case typeOfRoom.ENEMY:

                    Grid.Instance.levelID = idLevel;
                    Grid.Instance.progress = progression;
                    //Grid.Instance.deleteMap(true);

                    MapComposent.Instance.position = positionMap;

                    MapComposent.Instance.disableOldBouton();

                    MapComposent.Instance.Check();
                    //MapComposent.Instance.Closing();

                    break;
                case typeOfRoom.REST:

                    Grid.Instance.levelID = idLevel;
                    Grid.Instance.progress = progression;
                    //Grid.Instance.deleteMap(true);

                    MapComposent.Instance.position = positionMap;

                    MapComposent.Instance.disableOldBouton();

                    MapComposent.Instance.Check();

                    CharacterManager.Instance.sS.HP += (int)(CharacterManager.Instance.sS.maxHP / 2);
                    CharacterManager.Instance.sS.HP = Mathf.Clamp(CharacterManager.Instance.sS.HP, 1, CharacterManager.Instance.sS.maxHP);

                    StartCoroutine(infoMap(tor));

                    //MapComposent.Instance.Closing();

                    break;
                case typeOfRoom.TREASURE:

                    Grid.Instance.levelID = idLevel;
                    Grid.Instance.progress = progression;
                    //Grid.Instance.deleteMap(true);

                    MapComposent.Instance.position = positionMap;

                    MapComposent.Instance.disableOldBouton();

                    MapComposent.Instance.OnLATrouve();

                    MapComposent.Instance.Check();


                    var cmi = CharacterManager.Instance;
                    cmi.sS.maxHP += 3;
                    cmi.sS.HP += 3;
                    cmi.sS.strenght += 2;
                    cmi.sS.defense += 2;

                    StartCoroutine(infoMap(tor));

                    //MapComposent.Instance.Closing();
                    break;
                case typeOfRoom.BOSS:

                    Grid.Instance.levelID = idLevel;
                    Grid.Instance.progress = progression;
                    Grid.Instance.deleteMap(true);

                    MapComposent.Instance.position = positionMap;

                    MapComposent.Instance.disableOldBouton();

                    MapComposent.Instance.Check();
                    MapComposent.Instance.Closing();

                    break;
                default:
                    break;
            }
        }
    }


    public IEnumerator infoMap(typeOfRoom room)
    {
        var mapci = MapComposent.Instance;
        mapci.infoText.gameObject.SetActive(true);
        
        if(room == typeOfRoom.REST)
        {
            var hp = (int)(CharacterManager.Instance.sS.maxHP / 2);
            mapci.infoText.text = hp + " HP recovered !";
        }
        else if (room == typeOfRoom.TREASURE)
        {
            mapci.infoText.text = "Str, Def +2, HPMax + 3 !";
        }

        yield return new WaitForSecondsRealtime(1f);

        mapci.infoText.gameObject.SetActive(false);
    }
}
