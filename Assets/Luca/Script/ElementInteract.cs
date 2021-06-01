using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementInteract : MonoBehaviour
{

    private static ElementInteract _instance = null;

    public static ElementInteract Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;

    }

    
    public float interaction(Stats.ELEMENT att, Stats.ELEMENT def)
    {
        if (att == Stats.ELEMENT.NORMAL || def == Stats.ELEMENT.NORMAL)
        {
            return 1;
        }
        else if (att == def)
        {
            return 1;
        }
        else
        {
            if (att == Stats.ELEMENT.BLUE && def == Stats.ELEMENT.GREEN)
            {
                return 0.5f;
            }
            else if (att == Stats.ELEMENT.BLUE && def == Stats.ELEMENT.RED)
            {
                return 1.5f;
            }
            else if (att == Stats.ELEMENT.RED && def == Stats.ELEMENT.BLUE)
            {
                return 0.5f;
            }
            else if (att == Stats.ELEMENT.RED && def == Stats.ELEMENT.GREEN)
            {
                return 1.5f;
            }
            else if (att == Stats.ELEMENT.GREEN && def == Stats.ELEMENT.BLUE)
            {
                return 1.5f;
            }
            else if (att == Stats.ELEMENT.GREEN && def == Stats.ELEMENT.RED)
            {
                return 0.5f;
            }
            else
            {
                return 1;
            }
        }
        
    }

    public void changeElement(Stats.ELEMENT chara, Stats.ELEMENT att)
    {
        if (chara == att)
        {
            CharacterManager.Instance.currentPlayer.stats.elementCombo += 1;
        }
        else
        {
            CharacterManager.Instance.currentPlayer.stats.element = att;
            CharacterManager.Instance.currentPlayer.stats.elementCombo = 1;
        }

        switch (CharacterManager.Instance.currentPlayer.stats.element)
        {
            case Stats.ELEMENT.NORMAL:
                UiActionManager.Instance.elementImage.sprite = UiActionManager.Instance.elementInfos[3];
                break;
            case Stats.ELEMENT.RED:
                UiActionManager.Instance.elementImage.sprite = UiActionManager.Instance.elementInfos[0];
                break;
            case Stats.ELEMENT.BLUE:
                UiActionManager.Instance.elementImage.sprite = UiActionManager.Instance.elementInfos[1];
                break;
            case Stats.ELEMENT.GREEN:
                UiActionManager.Instance.elementImage.sprite = UiActionManager.Instance.elementInfos[2];
                break;
            default:
                break;
        }
    }
}
