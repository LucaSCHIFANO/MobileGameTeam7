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
                return 2f;
            }
            else if (att == Stats.ELEMENT.RED && def == Stats.ELEMENT.BLUE)
            {
                return 0.5f;
            }
            else if (att == Stats.ELEMENT.RED && def == Stats.ELEMENT.GREEN)
            {
                return 2f;
            }
            else if (att == Stats.ELEMENT.GREEN && def == Stats.ELEMENT.BLUE)
            {
                return 2f;
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
    }
}
