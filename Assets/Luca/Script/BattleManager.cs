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

    public void attackUnit(Stats att, Stats def)
    {
        Grid.Instance.resetClicked();

        var totalAtt = att.strenght + currentAttackParam.damage;

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

        CharacterManager.Instance.StartCoroutine("checkAlive");

        if (PhaseManager.Instance.phase == PhaseManager.actualPhase.PLAYER)
        {
            att.GetComponent<PlayerMovement>().state = PlayerMovement.States.IDLE;
            UiActionManager.Instance.showButton();
            UiActionManager.Instance.HidePortrait();
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
                        Debug.Log("tu peux aller en haut ");
                        defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos, -defPos.yPos + 1]));
                    }
                    else
                    {
                        Debug.Log("tu ne peux pas aller en haut");
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
                    }
                }
                else
                {
                    if (grid[defPos.xPos, -defPos.yPos - 1].canBeCrossed && !grid[defPos.xPos, -defPos.yPos - 1].isOccupied) // si la case apres est pas occupé
                    {
                        defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos, -defPos.yPos - 1]));
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
                        Debug.Log("tu peux aller a droite ");
                        defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos - 1, -defPos.yPos]));
                    }
                    else
                    {
                        Debug.Log("tu ne peux pas aller a droite");
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
                        Debug.Log("tu peux aller a gauche ");
                        defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos + 1, -defPos.yPos]));
                    }
                    else
                    {
                        Debug.Log("tu ne peux pas aller a gauche");
                    }
                }
            }
        }
    }



    public void PushPool(Enemy attPos, PlayerMovement defPos)
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
                        Debug.Log("tu peux aller en haut ");
                        defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos, -defPos.yPos + 1]));
                    }
                    else
                    {
                        Debug.Log("tu ne peux pas aller en haut");
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
                    }
                }
                else
                {
                    if (grid[defPos.xPos, -defPos.yPos - 1].canBeCrossed && !grid[defPos.xPos, -defPos.yPos - 1].isOccupied) // si la case apres est pas occupé
                    {
                        defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos, -defPos.yPos - 1]));
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
                        Debug.Log("tu peux aller a droite ");
                        defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos - 1, -defPos.yPos]));
                    }
                    else
                    {
                        Debug.Log("tu ne peux pas aller a droite");
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
                        Debug.Log("tu peux aller a gauche ");
                        defPos.StartCoroutine(defPos.isPushOrPull(grid[defPos.xPos + 1, -defPos.yPos]));
                    }
                    else
                    {
                        Debug.Log("tu ne peux pas aller a gauche");
                    }
                }
            }
        }
    }
}
