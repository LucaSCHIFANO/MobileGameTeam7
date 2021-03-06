using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public string characName;
    public int level;
    public int maxHP;
    public int HP;
    public int strenght;
    public int defense;
    public int speed;
    public int maxActionPoint;
    public int actionPoint;


    public int boostAtt;
    public int boostDef;
    public int boostAP;
    public int boostAPUsed;

    public ELEMENT element;
    public int elementCombo;

    public EFFECT effect;
    public int numberOfTurn;
    public int intesity;

    public enum ELEMENT
    {
        NORMAL,
        RED,
        BLUE,
        GREEN,
    }

    public enum EFFECT
    {
        NORMAL,
        POISON,
        REGEN, 
        LIFESTEAL,
    }

    public void effectActu()
    {
        if(numberOfTurn > 0 && effect != EFFECT.NORMAL)
        {
            if(effect == EFFECT.POISON)
            {
                HP -= intesity;
                HP = Mathf.Clamp(HP, 1, maxHP);

                BattleManager.Instance.showDamage(intesity, gameObject.transform.GetChild(0), new Color(0.9f, 0, 0.7f));
                

            }
            else if (effect == EFFECT.REGEN)
            {
                HP += intesity;
                HP = Mathf.Clamp(HP, 0, maxHP);
            }

            numberOfTurn--;
        }
    }

}
