using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoSaveStats : MonoBehaviour
{
    public int level;
    public int maxHP;
    public int HP;
    public int strenght;
    public int defense;
    public int speed;
    public int maxActionPoint;

    public TutoStats.ELEMENT element;
    public int elementCombo;

    public bool firstTime = false;


    public void setValues(TutoStats stats)
    {
        level = stats.level;
        maxHP = stats.maxHP;
        HP = stats.HP;
        strenght = stats.strenght;
        defense = stats.defense;
        speed = stats.speed;
        maxActionPoint = stats.maxActionPoint;
        element = stats.element;
        elementCombo = stats.elementCombo;

        firstTime = true;
    }

    public TutoStats loadValue() // a changer aussi dans grid setPLayerStats
    {
        TutoStats the = new TutoStats();

        the.level = level;
        the.maxHP = maxHP;
        the.HP = HP;
        the.strenght = strenght;
        the.defense = defense;
        the.speed = speed;
        the.maxActionPoint = maxActionPoint;
        the.actionPoint = maxActionPoint;
        the.element = element;
        the.elementCombo = elementCombo;

        return the;
    }
}

