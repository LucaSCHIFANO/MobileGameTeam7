using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        switch (tor)
        {
            case typeOfRoom.ENEMY:

                Grid.Instance.levelID = idLevel;
                Grid.Instance.progress = progression;
                Grid.Instance.deleteMap(true);

                MapComposent.Instance.position = positionMap;

                MapComposent.Instance.disableOldBouton();

                MapComposent.Instance.Check();
                MapComposent.Instance.Closing();

                break;
            case typeOfRoom.REST:

                Grid.Instance.levelID = idLevel;
                Grid.Instance.progress = progression;
                //Grid.Instance.deleteMap(true);

                MapComposent.Instance.position = positionMap;

                MapComposent.Instance.disableOldBouton();

                MapComposent.Instance.Check();

                CharacterManager.Instance.sS.HP += (int)(CharacterManager.Instance.sS.maxHP / 2);
                Mathf.Clamp(CharacterManager.Instance.sS.HP, 1, CharacterManager.Instance.sS.maxHP);
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
