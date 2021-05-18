using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAnEnemy : MonoBehaviour
{

    public List<AttackParam> listEnAttack = new List<AttackParam>();
    public void creation(Enemy enemy, int idLevel, int progress)
    {
        switch (idLevel) // neutre, green, blue, red
        {
            case 0:
                enemy.stats.characName = "Skully";
                enemy.stats.maxHP = 10;
                enemy.stats.HP= enemy.stats.maxHP;
                enemy.stats.strenght= 0;
                enemy.stats.defense = 0;
                enemy.stats.speed = 0;
                enemy.stats.maxActionPoint = 3;
                enemy.stats.actionPoint = enemy.stats.maxActionPoint;
                enemy.stats.element = Stats.ELEMENT.NORMAL;

                enemy.attackMonster.attackParam = listEnAttack[0];
                break;

            case 1:
                enemy.stats.characName = "A plant...";
                enemy.stats.maxHP = 12;
                enemy.stats.HP = enemy.stats.maxHP;
                enemy.stats.strenght = 0;
                enemy.stats.defense = 2;
                enemy.stats.speed = 0;
                enemy.stats.maxActionPoint = 4;
                enemy.stats.actionPoint = enemy.stats.maxActionPoint;
                enemy.stats.element = Stats.ELEMENT.GREEN;

                enemy.attackMonster.attackParam = listEnAttack[1];
                break;

            case 2:
                enemy.stats.characName = "Bububle";
                enemy.stats.maxHP = 7;
                enemy.stats.HP = enemy.stats.maxHP;
                enemy.stats.strenght = 2;
                enemy.stats.defense = 0;
                enemy.stats.speed = 0;
                enemy.stats.maxActionPoint = 4;
                enemy.stats.actionPoint = enemy.stats.maxActionPoint;
                enemy.stats.element = Stats.ELEMENT.BLUE;

                enemy.attackMonster.attackParam = listEnAttack[2];
                break;

            case 3:
                enemy.stats.characName = "Fiya Foxu";
                enemy.stats.maxHP = 6;
                enemy.stats.HP = enemy.stats.maxHP;
                enemy.stats.strenght = 1;
                enemy.stats.defense = 1;
                enemy.stats.speed = 0;
                enemy.stats.maxActionPoint = 5;
                enemy.stats.actionPoint = enemy.stats.maxActionPoint;
                enemy.stats.element = Stats.ELEMENT.RED;

                enemy.attackMonster.attackParam = listEnAttack[3];
                break;


            default:
                break;
        }
    }
}
