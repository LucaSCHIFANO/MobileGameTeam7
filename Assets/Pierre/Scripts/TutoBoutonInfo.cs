using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutoBoutonInfo : MonoBehaviour
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
        if (!TutoMapComposent.Instance.infoText.gameObject.activeInHierarchy)
        {
            switch (tor)
            {
                case typeOfRoom.ENEMY:

                    TutoGrid.Instance.levelID = idLevel;
                    TutoGrid.Instance.progress = progression;
                    //Grid.Instance.deleteMap(true);

                    TutoMapComposent.Instance.position = positionMap;

                    TutoMapComposent.Instance.disableOldBouton();

                    //MapComposent.Instance.Check();
                    TutoMapComposent.Instance.Closing();

                    break;
                case typeOfRoom.REST:

                    TutoGrid.Instance.levelID = idLevel;
                    TutoGrid.Instance.progress = progression;
                    //Grid.Instance.deleteMap(true);

                    TutoMapComposent.Instance.position = positionMap;

                    TutoMapComposent.Instance.disableOldBouton();

                    TutoMapComposent.Instance.Check();

                    TutoCharacterManager.Instance.sS.HP += (int)(TutoCharacterManager.Instance.sS.maxHP / 2);
                    TutoCharacterManager.Instance.sS.HP = Mathf.Clamp(TutoCharacterManager.Instance.sS.HP, 1, TutoCharacterManager.Instance.sS.maxHP);

                    TutoCharacterManager.Instance.isHealed = true;

                    StartCoroutine(infoMap(tor));

                    //MapComposent.Instance.Closing();

                    break;
                case typeOfRoom.TREASURE:

                    TutoGrid.Instance.levelID = idLevel;
                    TutoGrid.Instance.progress = progression;
                    //Grid.Instance.deleteMap(true);

                    TutoMapComposent.Instance.position = positionMap;

                    TutoMapComposent.Instance.disableOldBouton();

                    //MapComposent.Instance.OnLATrouve();

                    TutoMapComposent.Instance.Check();


                    var cmi = TutoCharacterManager.Instance;
                    cmi.sS.maxHP += 3;
                    cmi.sS.HP += 3;
                    cmi.sS.strenght += 2;
                    cmi.sS.defense += 2;

                    StartCoroutine(infoMap(tor));

                    //MapComposent.Instance.Closing();
                    break;
                case typeOfRoom.BOSS:

                    TutoGrid.Instance.levelID = idLevel;
                    TutoGrid.Instance.progress = progression;
                    //Grid.Instance.deleteMap(true);

                    TutoMapComposent.Instance.position = positionMap;

                    TutoMapComposent.Instance.disableOldBouton();

                    //MapComposent.Instance.Check();
                    TutoMapComposent.Instance.Closing();

                    break;
                default:
                    break;
            }
        }
    }


    public IEnumerator infoMap(typeOfRoom room)
    {
        var mapci = TutoMapComposent.Instance;
        mapci.infoText.gameObject.SetActive(true);

        if (room == typeOfRoom.REST)
        {
            var hp = (int)(TutoCharacterManager.Instance.sS.maxHP / 2);
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
