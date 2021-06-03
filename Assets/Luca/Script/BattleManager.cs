using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{

    public AttackParam currentAttackParam;
    public GameObject damageEffect;

    public GameObject floatingText;

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

        int totalAtt = (int)((att.strenght + currentAttackParam.damage + att.boostAtt) * multiplicator);

        var damage = 0;


        if (totalAtt - (def.defense + def.boostDef) > 0)
        {
            damage = totalAtt - (def.defense + def.boostDef);
        }



        if (att.GetComponent<PlayerMovement>())
        {
            StartCoroutine(showDamage(damage, def.gameObject.transform.GetChild(0), Color.blue));
            Instantiate(currentAttackParam.effectAttack, def.gameObject.transform.GetChild(0).transform.GetChild(1).position, def.gameObject.transform.rotation);

        }
        else
        {
            StartCoroutine(showDamage(damage, def.gameObject.transform.GetChild(0), Color.red));
            Instantiate(currentAttackParam.effectAttack, def.gameObject.transform.GetChild(0).position, def.gameObject.transform.rotation);

            if (damage > 0)
            {
                CharacterManager.Instance.noDamage = false;
            }
        }


        def.HP -= damage;
        UiActionManager.Instance.HPBar.value = CharacterManager.Instance.currentPlayer.stats.HP;
        UiActionManager.Instance.currenntHP.text = CharacterManager.Instance.currentPlayer.stats.HP.ToString();

        StartCoroutine(AudioManager.Instance.PlayWDelay(currentAttackParam.musicName, 0.7f));

        StartCoroutine(AttackPart2(att, def, aoe, damage));

    }

    public IEnumerator AttackPart2(Stats att, Stats def, bool aoe, int damage)
    {
        yield return new WaitForSeconds(0.7f);
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

        if (currentAttackParam.effect != Stats.EFFECT.NORMAL)
        {

            if (currentAttackParam.effect == Stats.EFFECT.POISON)
            {
                def.effect = Stats.EFFECT.POISON;
            }
            else if (currentAttackParam.effect == Stats.EFFECT.LIFESTEAL)
            {
                att.HP += (int)(damage * 0.5f);
                att.HP = Mathf.Clamp(att.HP, 0, att.maxHP);

                if ((int)(damage * 0.5f) > 0)
                {
                    CharacterManager.Instance.isHealed = true;
                }
            }

            def.intesity = currentAttackParam.intensity;
            def.numberOfTurn = currentAttackParam.duration;
        }

        if (!aoe)
        {
            CharacterManager.Instance.StartCoroutine("checkAlive");

            Grid.Instance.resetClicked();

            if (att.GetComponent<PlayerMovement>())
            {

                AudioManager.Instance.Play("Hit");


                att.GetComponent<PlayerMovement>().state = PlayerMovement.States.IDLE;
                ElementInteract.Instance.changeElement(att.element,currentAttackParam.element);
                UiActionManager.Instance.showButton();
                UiActionManager.Instance.HidePortrait();
                ComboSystem.Instance.comboEffect(CharacterManager.Instance.currentPlayer.stats.element, CharacterManager.Instance.currentPlayer.stats.elementCombo);

                UiActionManager.Instance.EnemyToHero(CharacterManager.Instance.currentPlayer.stats);
                UiActionManager.Instance.ShowPortrait(CharacterManager.Instance.currentPlayer.stats);

                foreach (var item in Grid.Instance.gridArrayAlpha)
                {
                    Grid.Instance.gridArrayAlpha[item.x, item.y].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Grid.Instance.listSpritesAlpha[0];
                    Grid.Instance.gridArrayAlpha[item.x, item.y].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                }
            }
            else
            {
                UiActionManager.Instance.EnemyToHero(CharacterManager.Instance.currentPlayer.stats);
                UiActionManager.Instance.ShowPortrait(CharacterManager.Instance.currentPlayer.stats);
            }
        }

        UiActionManager.Instance.apleft.text = CharacterManager.Instance.currentPlayer.stats.actionPoint.ToString();
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

   public IEnumerator showDamage(int damage, Transform position, Color color)
   {
        Transform newpos = position; 
        var txt = Instantiate(floatingText, newpos.position, newpos.rotation);
        txt.GetComponent<TextMeshPro>().text = damage.ToString();
        txt.GetComponent<MeshRenderer>().sortingOrder = 100;

        txt.GetComponent<TextMeshPro>().color = color;
        yield return new WaitForSeconds(1f);
    }

}
