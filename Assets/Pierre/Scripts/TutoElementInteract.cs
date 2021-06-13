using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoElementInteract : MonoBehaviour
{

    private static TutoElementInteract _instance = null;

    public static TutoElementInteract Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;

    }


    public float interaction(TutoStats.ELEMENT att, TutoStats.ELEMENT def)
    {
        if (att == TutoStats.ELEMENT.NORMAL || def == TutoStats.ELEMENT.NORMAL)
        {
            return 1;
        }
        else if (att == def)
        {
            return 1;
        }
        else
        {
            if (att == TutoStats.ELEMENT.BLUE && def == TutoStats.ELEMENT.GREEN)
            {
                return 0.5f;
            }
            else if (att == TutoStats.ELEMENT.BLUE && def == TutoStats.ELEMENT.RED)
            {
                return 1.5f;
            }
            else if (att == TutoStats.ELEMENT.RED && def == TutoStats.ELEMENT.BLUE)
            {
                return 0.5f;
            }
            else if (att == TutoStats.ELEMENT.RED && def == TutoStats.ELEMENT.GREEN)
            {
                return 1.5f;
            }
            else if (att == TutoStats.ELEMENT.GREEN && def == TutoStats.ELEMENT.BLUE)
            {
                return 1.5f;
            }
            else if (att == TutoStats.ELEMENT.GREEN && def == TutoStats.ELEMENT.RED)
            {
                return 0.5f;
            }
            else
            {
                return 1;
            }
        }

    }

    public void changeElement(TutoStats.ELEMENT chara, TutoStats.ELEMENT att)
    {
        if (chara == att)
        {
            TutoCharacterManager.Instance.currentPlayer.stats.elementCombo += 1;
        }
        else
        {
            TutoCharacterManager.Instance.currentPlayer.stats.element = att;
            TutoCharacterManager.Instance.currentPlayer.stats.elementCombo = 1;
        }

        switch (TutoCharacterManager.Instance.currentPlayer.stats.element)
        {
            case TutoStats.ELEMENT.NORMAL:
                TutoUI.Instance.elementImage.sprite = TutoUI.Instance.elementInfos[3];
                break;
            case TutoStats.ELEMENT.RED:
                TutoUI.Instance.elementImage.sprite = TutoUI.Instance.elementInfos[0];
                break;
            case TutoStats.ELEMENT.BLUE:
                TutoUI.Instance.elementImage.sprite = TutoUI.Instance.elementInfos[1];
                break;
            case TutoStats.ELEMENT.GREEN:
                TutoUI.Instance.elementImage.sprite = TutoUI.Instance.elementInfos[2];
                break;
            default:
                break;
        }
    }
}
