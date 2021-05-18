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
    public ELEMENT element;

    public EFFECT effect;
    public int numberOfTurn;

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
    }


    public void effectActu()
    {
        if(numberOfTurn > 0 && effect != EFFECT.NORMAL)
        {
            if(effect == EFFECT.POISON)
            {
                if(HP > 1)
                {
                    HP--; 
                }
            }else if (effect == EFFECT.REGEN)
            {
                HP++;
                HP = Mathf.Clamp(HP, 0, maxHP);
            }

            numberOfTurn--;
        }
    }

}
