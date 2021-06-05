using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int xPos;
    public int yPos;
    public PlayerMovement.States characterState;

    public Stats stats;
    public AttackMonster attackMonster;

    public GameObject panelToGo;

    public Pattern pattern;
    public bool isMoving;

    public enum Pattern
    {
        RUSH,
        RUSHDISTANCECIRCLE,
        RUSHDISTANCEROWCOLUMN,
        RUN,
    }

    public void Start()
    {
        Panel startPos =  Grid.Instance.gridArray[xPos, -yPos];
        transform.position = new Vector2(startPos.transform.position.x, startPos.transform.position.y);
        stats = GetComponent<Stats>();
        attackMonster = GetComponent<AttackMonster>();

        stats.actionPoint = stats.maxActionPoint;
        stats.HP = stats.maxHP;
    }

    void Update()
    {
        if (characterState == PlayerMovement.States.WAIT)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.3679245f, 0.3679245f, 0.3679245f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.magenta;
        }

        if (panelToGo != null)
        {
            trueMovement();
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    public void mouvementCheck() // check le chemin  pour atteindre le joueur
    {
        var allPlayerPos = checkAllPlayerPos();


        List<Panel> allVoisinsPlayers = new List<Panel>();

        List<Panel> finalPath = null;

        List<Panel> finalPath2 = new List<Panel>();

        int minPath = int.MaxValue;

        BlueRedGrid.Instance.movementsPossible(xPos, yPos, true);

        foreach (var target in allPlayerPos)
        {
            Panel actualPanel = Grid.Instance.gridArray[(int)target.x, -(int)target.y];
            List<Panel> voisinAgain = Grid.Instance.CheckVoisin(actualPanel);

            foreach (var voisin in voisinAgain)
            {
                if (voisin.canBeCrossed)
                {
                    allVoisinsPlayers.Add(voisin);
                }
            }

        }

        if (pattern == Pattern.RUSHDISTANCECIRCLE || pattern == Pattern.RUSHDISTANCEROWCOLUMN)  // -----------------------
        {
            var allOtherPos = checkOtherPosPossible();
            allVoisinsPlayers.AddRange(allOtherPos);
        }

            foreach (var voisin in allVoisinsPlayers)
            {
            List<Panel> listPanel = Grid.Instance.PathFinding(xPos, yPos, (int)voisin.x, (int)voisin.y, true);
                
                if (listPanel != null)
                {
                    if (listPanel[listPanel.Count - 1].actualMovementCost < minPath)
                    {
                        finalPath = listPanel;
                        minPath = listPanel[listPanel.Count - 1].actualMovementCost;
                    }
                }
            }

            foreach (var panel in finalPath)
            {
                if (panel.actualMovementCost <= stats.actionPoint)
                {
                    finalPath2.Add(panel);
                }
            }

            while(finalPath2[finalPath2.Count - 1].unitOn)
            {
                finalPath2.RemoveAt(finalPath2.Count - 1);

                if(finalPath2.Count == 0)
                {
                break;
                }
            }

        /*foreach (var item in finalPath)
        {
            Debug.Log(item.name);
        }*/

            StartCoroutine(movement(finalPath2));
    }



    private List<Vector2> checkAllPlayerPos() // verifie la position de tous les joueurs
    {
        var allCharacters = GameObject.FindGameObjectsWithTag("Characters");

        List<Vector2> playersPosList = new List<Vector2>();
        foreach (var chara in allCharacters)
        {
            var character = chara.GetComponent<PlayerMovement>();
            if (character != null)
            {
                playersPosList.Add(new Vector2(character.xPos, character.yPos));
            }
        }

        return playersPosList;
    }

    private List<Panel> checkOtherPosPossible()
    {
        Grid.Instance.resetClicked();

        var player = CharacterManager.Instance.currentPlayer;

        BlueRedGrid.Instance.actuPanelCount(player.xPos, player.yPos);

        List<Panel> otherPosList = new List<Panel>();
        if (pattern == Pattern.RUSHDISTANCECIRCLE)
        {
            foreach (var panel in Grid.Instance.gridArray)
            {
                if (panel.actualPanelCount <= attackMonster.attackParam.range && !panel.isOccupied)
                {
                    otherPosList.Add(panel);
                    Debug.Log(panel);
                }
            }
        }
        else
        {
            foreach (var panel in Grid.Instance.gridArray)
            {
                if (panel.actualPanelCount <= attackMonster.attackParam.range && (panel.x == player.xPos || panel.y == player.yPos) )
                {
                    otherPosList.Add(panel);
                }
            }
        }

        return otherPosList;
    }




    public IEnumerator movement(List<Panel> panelsList) // se deplace au plus proche de sa cible
    {
        Grid.Instance.resetClicked();
        var notFirst = 0;
        var noNeedToMove = false;

        attackMonster.testAttackRange(xPos, yPos);
        if (attackMonster.seePlayer)
        {
            noNeedToMove = true;
        }

        if (noNeedToMove == false)
        {
            foreach (var panel in panelsList)
            {
                //transform.position = Vector3.MoveTowards(transform.position, panel.gameObject.transform.position, 20f);
                panelToGo = panel.gameObject;
                var canAttack = !panel.isOccupied;

                yield return new WaitForSeconds(0.2f);

                if (notFirst != 0)
                {
                    stats.actionPoint -= panel.movementCost;
                }
                notFirst++;
                xPos = panel.x;
                yPos = -panel.y;

                attackMonster.testAttackRange(panel.x, -panel.y);
                if (attackMonster.seePlayer && canAttack)
                {
                    BattleManager.Instance.attackUnit(GetComponent<Stats>(), CharacterManager.Instance.currentPlayer.GetComponent<Stats>(), false);
                    yield return new WaitForSeconds(1f);
                    break;
                }

            }
        }else
        {
            BattleManager.Instance.attackUnit(GetComponent<Stats>(), CharacterManager.Instance.currentPlayer.GetComponent<Stats>(), false);
            yield return new WaitForSeconds(1f);
        }

        panelToGo = null;
        yield return new WaitForSeconds(0.3f);
        /*if (noNeedToMove == false)
        {
            xPos = panelsList[panelsList.Count - 1].x;
            yPos = -panelsList[panelsList.Count - 1].y;
        }*/

        characterState = PlayerMovement.States.WAIT;
        stats.effectActu();
        PhaseManager.Instance.checkAllEnemies();
    }

    public void trueMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, panelToGo.gameObject.transform.position, 6.5f * Time.deltaTime);
    }



    public IEnumerator isPushOrPull(Panel panelToMove)
    {
        panelToGo = panelToMove.gameObject;
        yield return new WaitForSeconds(0.2f);
        xPos = panelToMove.x;
        yPos = -panelToMove.y;
        panelToGo = null;

    }
}
