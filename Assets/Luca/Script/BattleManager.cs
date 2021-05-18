using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public AttackParam currentAttackParam;
    public GameObject damageEffect;

    private static BattleManager _instance = null;

    public static BattleManager Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }

    public void attackUnit(Stats att, Stats def, bool aoe)
    {
        Debug.Log("test attck");
        float multiplicator = ElementInteract.Instance.interaction(currentAttackParam.element, def.element);

        int totalAtt = (int)((att.strenght + currentAttackParam.damage) * multiplicator);
        
        var damage = 0;


        if (totalAtt - def.defense > 0)
        {
            damage = totalAtt - def.defense;
        }

        def.HP -= damage;
        Instantiate(damageEffect, def.gameObject.transform.GetChild(0).position, def.gameObject.transform.rotation);

        if (currentAttackParam.pull || currentAttackParam.push)
        {
            if (att.gameObject.GetComponent<PlayerMovement>())
            {
                var attScript = att.gameObject.GetComponent<PlayerMovement>();
                var defScript = def.gameObject.GetComponent<Enemy>();

                PushPool(attScript, defScript);
            }
            else
            {
                var attScript = att.gameObject.GetComponent<Enemy>();
                var defScript = def.gameObject.GetComponent<PlayerMovement>();

                PushPool(attScript, defScript);
            }
        }

        if (!aoe)
        {
            CharacterManager.Instance.StartCoroutine("checkAlive");

            Grid.Instance.resetClicked();

            if (PhaseManager.Instance.phase == PhaseManager.actualPhase.PLAYER)
            {
                att.GetComponent<PlayerMovement>().state = PlayerMovement.States.IDLE;
                att.GetComponent<Stats>().element = currentAttackParam.element;
                UiActionManager.Instance.showButton();
                UiActionManager.Instance.HidePortrait();
            }
        }

    }

    public void PushPool(PlayerMovement attPos, Enemy defPos)
    {
        var grid = Grid.Instance.gridArray;
        var gridScript = Grid.Instance;

        if (attPos.xPos == defPos.xPos) // check mm colonne
        {
            if (attPos.yPos < defPos.yPos) // si en bas
            {
                if (currentAttackParam.push) // push or pull
                {
                    if (defPos.yPos != 0) // ennemi pas en au bord en haut
                    {
                        if (grid[defPos.xPos, -defPos.yPos - 1].canBeCrossed && !grid[defPos.xPos, -defPos.yPos - 1].isOccupied) // si la case apres est pas occupé
                        {
                            Debug.Log("tu peux aller en haut ");
                            defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos, -defPos.yPos - 1]));
                        }
                        else
                        {
                            Debug.Log("tu ne peux pas aller en haut");
                        }
                    }
                }
                else
                {
                    if (grid[defPos.xPos, -defPos.yPos + 1].canBeCrossed && !grid[defPos.xPos, -defPos.yPos + 1].isOccupied) // si la case apres est pas occupé
                    {
                        Debug.Log("tu peux aller en bas ");
                        defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos, -defPos.yPos + 1]));
                    }
                    else
                    {
                        Debug.Log("tu ne peux pas aller en bas");
                    }
                }
            }


            else // si en haut
            {
                if (currentAttackParam.push) // push or pull
                {
                    if (defPos.yPos != gridScript.height - 1) // ennemi pas en au bord en bas
                    {
                        if (grid[defPos.xPos, -defPos.yPos + 1].canBeCrossed && !grid[defPos.xPos, -defPos.yPos + 1].isOccupied) // si la case apres est pas occupé
                        {
                            Debug.Log("tu peux aller en bas ");
                            defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos, -defPos.yPos + 1]));
                        }
                        else
                        {
                            Debug.Log("tu ne peux pas aller en bas");
                        }
                    }
                }
                else
                {
                    if (grid[defPos.xPos, -defPos.yPos - 1].canBeCrossed && !grid[defPos.xPos, -defPos.yPos - 1].isOccupied) // si la case apres est pas occupé
                    {
                        defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos, -defPos.yPos - 1]));
                        Debug.Log("tu peux aller en haut ");
                    }
                    else
                    {
                        Debug.Log("tu ne peux pas aller en haut");
                    }
                }
            }

        }


        else if (attPos.yPos == defPos.yPos)
        {
            if (attPos.xPos < defPos.xPos) // si a gauche
            {
                if (currentAttackParam.push) // push or pull
                {
                    if (defPos.xPos != gridScript.width - 1) // ennemi pas en au bord a droite
                    {
                        if (grid[defPos.xPos + 1, -defPos.yPos].canBeCrossed && !grid[defPos.xPos + 1, -defPos.yPos].isOccupied) // si la case apres est pas occupé
                        {
                            Debug.Log("tu peux aller a droite ");
                            defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos + 1, -defPos.yPos]));
                        }
                        else
                        {
                            Debug.Log("tu ne peux pas aller a droite");
                        }
                    }
                }
                else
                {
                    if (grid[defPos.xPos - 1, -defPos.yPos].canBeCrossed && !grid[defPos.xPos - 1, -defPos.yPos].isOccupied) // si la case apres est pas occupé
                    {
                        Debug.Log("tu peux aller a gauche ");
                        defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos - 1, -defPos.yPos]));
                    }
                    else
                    {
                        Debug.Log("tu ne peux pas aller a gauche");
                    }
                }
            }
            else
            {
                if (currentAttackParam.push) // push or pull
                {
                    if (defPos.xPos != 0) // ennemi pas en au bord a gauche
                    {
                        if (grid[defPos.xPos - 1, -defPos.yPos].canBeCrossed && !grid[defPos.xPos - 1, -defPos.yPos].isOccupied) // si la case apres est pas occupé
                        {
                            Debug.Log("tu peux aller a gauche ");
                            defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos - 1, -defPos.yPos]));
                        }
                        else
                        {
                            Debug.Log("tu ne peux pas aller a gauche");
                        }
                    }
                }
                else
                {
                    if (grid[defPos.xPos + 1, -defPos.yPos].canBeCrossed && !grid[defPos.xPos + 1, -defPos.yPos].isOccupied) // si la case apres est pas occupé
                    {
                        Debug.Log("tu peux aller a droite ");
                        defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos + 1, -defPos.yPos]));
                    }
                    else
                    {
                        Debug.Log("tu ne peux pas aller a droite");
                    }
                }
            }
        }
    }



    public void PushPool(Enemy attPos, PlayerMovement defPos)
    {

        var grid = Grid.Instance.gridArray;
        var gridScript = Grid.Instance;

        Debug.Log(attPos.xPos + " " + attPos.yPos + " ; " + defPos.xPos + " " + defPos.yPos);

        if (attPos.xPos == defPos.xPos) // check mm colonne
        {

            Debug.Log("enemy push player");
            if (attPos.yPos < defPos.yPos) // si en bas
            {
                if (currentAttackParam.push) // push or pull
                {
                    if (defPos.yPos != 0) // ennemi pas en au bord en haut
                    {
                        if (grid[defPos.xPos, -defPos.yPos - 1].canBeCrossed && !grid[defPos.xPos, -defPos.yPos - 1].isOccupied) // si la case apres est pas occupé
                        {
                            Debug.Log("tu peux aller en haut ");
                            defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos, -defPos.yPos - 1]));
                        }
                        else
                        {
                            Debug.Log("tu ne peux pas aller en haut");
                        }
                    }
                }
                else
                {
                    if (grid[defPos.xPos, -defPos.yPos + 1].canBeCrossed && !grid[defPos.xPos, -defPos.yPos + 1].isOccupied) // si la case apres est pas occupé
                    {
                        Debug.Log("tu peux aller en bas ");
                        defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos, -defPos.yPos + 1]));
                    }
                    else
                    {
                        Debug.Log("tu ne peux pas aller en bas");
                    }
                }
            }


            else // si en haut
            {
                if (currentAttackParam.push) // push or pull
                {
                    if (defPos.yPos != gridScript.height - 1) // ennemi pas en au bord en bas
                    {
                        if (grid[defPos.xPos, -defPos.yPos + 1].canBeCrossed && !grid[defPos.xPos, -defPos.yPos + 1].isOccupied) // si la case apres est pas occupé
                        {
                            Debug.Log("tu peux aller en bas ");
                            defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos, -defPos.yPos + 1]));
                        }
                        else
                        {
                            Debug.Log("tu ne peux pas aller en bas");
                        }
                    }
                }
                else
                {
                    if (grid[defPos.xPos, -defPos.yPos - 1].canBeCrossed && !grid[defPos.xPos, -defPos.yPos - 1].isOccupied) // si la case apres est pas occupé
                    {
                        defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos, -defPos.yPos - 1]));
                        Debug.Log("tu peux aller en haut ");
                    }
                    else
                    {
                        Debug.Log("tu ne peux pas aller en haut");
                    }
                }
            }

        }


        else if (attPos.yPos == defPos.yPos) // mmm ligne
        {
            if (attPos.xPos < defPos.xPos) // si a gauche
            {
                if (currentAttackParam.push) // push or pull
                {
                    if (defPos.xPos != gridScript.width - 1) // ennemi pas en au bord a droite
                    {
                        if (grid[defPos.xPos + 1, -defPos.yPos].canBeCrossed && !grid[defPos.xPos + 1, -defPos.yPos].isOccupied) // si la case apres est pas occupé
                        {
                            Debug.Log("tu peux aller a droite ");
                            defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos + 1, -defPos.yPos]));
                        }
                        else
                        {
                            Debug.Log("tu ne peux pas aller a droite");
                        }
                    }
                }
                else
                {
                    if (grid[defPos.xPos - 1, -defPos.yPos].canBeCrossed && !grid[defPos.xPos - 1, -defPos.yPos].isOccupied) // si la case apres est pas occupé
                    {
                        Debug.Log("tu peux aller a gauche ");
                        defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos - 1, -defPos.yPos]));
                    }
                    else
                    {
                        Debug.Log("tu ne peux pas aller a gauche");
                    }
                }
            }
            else
            {
                if (currentAttackParam.push) // push or pull
                {
                    if (defPos.xPos != 0) // ennemi pas en au bord a gauche
                    {
                        if (grid[defPos.xPos - 1, -defPos.yPos].canBeCrossed && !grid[defPos.xPos - 1, -defPos.yPos].isOccupied) // si la case apres est pas occupé
                        {
                            Debug.Log("tu peux aller a gauche ");
                            defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos - 1, -defPos.yPos]));
                        }
                        else
                        {
                            Debug.Log("tu ne peux pas aller a gauche");
                        }
                    }
                }
                else
                {
                    if (grid[defPos.xPos + 1, -defPos.yPos].canBeCrossed && !grid[defPos.xPos + 1, -defPos.yPos].isOccupied) // si la case apres est pas occupé
                    {
                        Debug.Log("tu peux aller a droite ");
                        defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos + 1, -defPos.yPos]));
                    }
                    else
                    {
                        Debug.Log("tu ne peux pas aller a droite");
                    }
                }
            }
        }
    }
}
