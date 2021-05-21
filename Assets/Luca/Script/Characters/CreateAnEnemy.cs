using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAnEnemy : MonoBehaviour
{

    public List<AttackParam> listEnAttack = new List<AttackParam>();
    public void creation(Enemy enemy, int idLevel, int progress)
    {

        var upgradeProba = 25 * progress;
        var upgradeValue = 0;

        while(upgradeProba >= 100)
        {
            upgradeValue ++;
            upgradeProba -= 100;
        }
                
        switch (idLevel) // neutre, green, blue, red
        {
            case 0:
                enemy.stats.characName = "Skully";
                enemy.stats.maxHP = 10 + upgradeValue + proba(upgradeProba);
                enemy.stats.HP= enemy.stats.maxHP;
                enemy.stats.strenght = upgradeValue + proba(upgradeProba);
                enemy.stats.defense = upgradeValue + proba(upgradeProba);
                enemy.stats.speed = upgradeValue + proba(upgradeProba);
                enemy.stats.maxActionPoint = 3;
                enemy.stats.actionPoint = enemy.stats.maxActionPoint;
                enemy.stats.element = Stats.ELEMENT.NORMAL;

                enemy.attackMonster.attackParam = listEnAttack[0];
                enemy.pattern = Enemy.Pattern.RUSHDISTANCEROWCOLUMN;
                break;

            case 1:
                enemy.stats.characName = "A plant...";
                enemy.stats.maxHP = 12 + upgradeValue + proba(upgradeProba);
                enemy.stats.HP = enemy.stats.maxHP;
                enemy.stats.strenght = upgradeValue + proba(upgradeProba);
                enemy.stats.defense = 2 + upgradeValue + proba(upgradeProba);
                enemy.stats.speed = upgradeValue + proba(upgradeProba);
                enemy.stats.maxActionPoint = 4;
                enemy.stats.actionPoint = enemy.stats.maxActionPoint;
                enemy.stats.element = Stats.ELEMENT.GREEN;

                enemy.attackMonster.attackParam = listEnAttack[1];
                enemy.pattern = Enemy.Pattern.RUSHDISTANCECIRCLE;
                break;

            case 2:
                enemy.stats.characName = "Bububle";
                enemy.stats.maxHP = 7 + upgradeValue + proba(upgradeProba);
                enemy.stats.HP = enemy.stats.maxHP;
                enemy.stats.strenght = 2 + upgradeValue + proba(upgradeProba);
                enemy.stats.defense = upgradeValue + proba(upgradeProba);
                enemy.stats.speed = upgradeValue + proba(upgradeProba);
                enemy.stats.maxActionPoint = 4;
                enemy.stats.actionPoint = enemy.stats.maxActionPoint;
                enemy.stats.element = Stats.ELEMENT.BLUE;

                enemy.attackMonster.attackParam = listEnAttack[2];
                enemy.pattern = Enemy.Pattern.RUSHDISTANCEROWCOLUMN;
                break;

            case 3:
                enemy.stats.characName = "Fiya Foxu";
                enemy.stats.maxHP = 6 + upgradeValue + proba(upgradeProba);
                enemy.stats.HP = enemy.stats.maxHP;
                enemy.stats.strenght = 1 + upgradeValue + proba(upgradeProba);
                enemy.stats.defense = 1 + upgradeValue + proba(upgradeProba);
                enemy.stats.speed = upgradeValue + proba(upgradeProba);
                enemy.stats.maxActionPoint = 5;
                enemy.stats.actionPoint = enemy.stats.maxActionPoint;
                enemy.stats.element = Stats.ELEMENT.RED;

                enemy.attackMonster.attackParam = listEnAttack[3];
                enemy.pattern = Enemy.Pattern.RUSHDISTANCECIRCLE;
                break;


            default:
                enemy.stats.characName = "Skully";
                enemy.stats.maxHP = 10 + upgradeValue + proba(upgradeProba);
                enemy.stats.HP = enemy.stats.maxHP;
                enemy.stats.strenght = upgradeValue + proba(upgradeProba);
                enemy.stats.defense = upgradeValue + proba(upgradeProba);
                enemy.stats.speed = upgradeValue + proba(upgradeProba);
                enemy.stats.maxActionPoint = 3;
                enemy.stats.actionPoint = enemy.stats.maxActionPoint;
                enemy.stats.element = Stats.ELEMENT.NORMAL;

                enemy.attackMonster.attackParam = listEnAttack[0];
                enemy.pattern = Enemy.Pattern.RUSHDISTANCEROWCOLUMN;
                break;
        }
    }

    public int proba(int value)
    {
        if (Random.Range(1, 101) <= value) 
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
