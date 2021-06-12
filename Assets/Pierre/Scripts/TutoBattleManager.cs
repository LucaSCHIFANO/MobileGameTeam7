using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutoBattleManager : MonoBehaviour
{

    public TutoAttackParam currentAttackParam;
    public GameObject damageEffect;

    public GameObject floatingText;

    private static TutoBattleManager _instance = null;

    public static TutoBattleManager Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }

    public void attackUnit(TutoStats att, TutoStats def, bool aoe)
    {
        Debug.Log("test attck");
        float multiplicator = TutoElementInteract.Instance.interaction(currentAttackParam.element, def.element);

        int totalAtt = (int)((att.strenght + currentAttackParam.damage + att.boostAtt) * multiplicator);

        var damage = 0;


        if (totalAtt - (def.defense + def.boostDef) > 0)
        {
            damage = totalAtt - (def.defense + def.boostDef);
        }

        def.HP -= damage;
        TutoUI.Instance.HPBar.value = TutoCharacterManager.Instance.currentPlayer.stats.HP;
        TutoUI.Instance.currenntHP.text = TutoCharacterManager.Instance.currentPlayer.stats.HP.ToString();

        AudioManager.Instance.Play(currentAttackParam.musicName);

        Instantiate(damageEffect, def.gameObject.transform.GetChild(0).position, def.gameObject.transform.rotation);

        if (att.GetComponent<TutoPlayerMovement>())
        {
            showDamage(damage, def.gameObject.transform.GetChild(0), Color.blue);

        }
        else
        {
            //Tuto.Instance.Pop63();
            showDamage(damage, def.gameObject.transform.GetChild(0), Color.red);

            if (damage > 0)
            {
                TutoCharacterManager.Instance.noDamage = false;
            }
        }


        if (currentAttackParam.pull || currentAttackParam.push)
        {
            if (att.gameObject.GetComponent<TutoPlayerMovement>())
            {
                var attScript = att.gameObject.GetComponent<TutoPlayerMovement>();
                var defScript = def.gameObject.GetComponent<TutoEnemy>();

                PushPool(attScript, defScript);
            }
            else
            {
                var attScript = att.gameObject.GetComponent<TutoEnemy>();
                var defScript = def.gameObject.GetComponent<TutoPlayerMovement>();

                PushPool(attScript, defScript);
            }
        }

        if (currentAttackParam.effect != TutoStats.EFFECT.NORMAL)
        {

            if (currentAttackParam.effect == TutoStats.EFFECT.POISON)
            {
                def.effect = TutoStats.EFFECT.POISON;
            }
            else if (currentAttackParam.effect == TutoStats.EFFECT.LIFESTEAL)
            {
                att.HP += (int)(damage * 0.5f);
                att.HP = Mathf.Clamp(att.HP, 0, att.maxHP);
                if ((int)(damage * 0.5f) > 0)
                {
                    TutoCharacterManager.Instance.isHealed = true;
                }
            }

            def.intesity = currentAttackParam.intensity;
            def.numberOfTurn = currentAttackParam.duration;
        }

        if (!aoe)
        {
            TutoCharacterManager.Instance.StartCoroutine("checkAlive");

            TutoGrid.Instance.resetClicked();

            if (TutoPhaseManager.Instance.phase == TutoPhaseManager.actualPhase.PLAYER)
            {
                att.GetComponent<TutoPlayerMovement>().state = TutoPlayerMovement.States.IDLE;
                TutoElementInteract.Instance.changeElement(att.element, currentAttackParam.element);
                TutoUI.Instance.showButton();
                TutoUI.Instance.HidePortrait();
                TutoComboSystem.Instance.comboEffect(TutoCharacterManager.Instance.currentPlayer.stats.element, TutoCharacterManager.Instance.currentPlayer.stats.elementCombo);

                TutoUI.Instance.EnemyToHero(TutoCharacterManager.Instance.currentPlayer.stats);
                TutoUI.Instance.ShowPortrait(TutoCharacterManager.Instance.currentPlayer.stats);

                foreach (var item in TutoGrid.Instance.gridArrayAlpha)
                {
                    TutoGrid.Instance.gridArrayAlpha[item.x, item.y].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TutoGrid.Instance.listSpritesAlpha[0];
                    TutoGrid.Instance.gridArrayAlpha[item.x, item.y].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                }
            }
            else
            {
                TutoUI.Instance.EnemyToHero(TutoCharacterManager.Instance.currentPlayer.stats);
                TutoUI.Instance.ShowPortrait(TutoCharacterManager.Instance.currentPlayer.stats);
            }
        }

        TutoUI.Instance.apleft.text = TutoCharacterManager.Instance.currentPlayer.stats.actionPoint.ToString();
    }

    public void PushPool(TutoPlayerMovement attPos, TutoEnemy defPos)
    {
        var grid = TutoGrid.Instance.gridArray;
        var gridScript = TutoGrid.Instance;

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



    public void PushPool(TutoEnemy attPos, TutoPlayerMovement defPos)
    {

        var grid = TutoGrid.Instance.gridArray;
        var gridScript = TutoGrid.Instance;

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

    public void showDamage(int damage, Transform position, Color color)
    {
        var txt = Instantiate(floatingText, position.position, position.rotation);
        txt.GetComponent<TextMeshPro>().text = damage.ToString();
        txt.GetComponent<MeshRenderer>().sortingOrder = 100;

        txt.GetComponent<TextMeshPro>().color = color;
    }

}

