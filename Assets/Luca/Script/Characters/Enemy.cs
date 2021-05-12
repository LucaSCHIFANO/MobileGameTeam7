using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int xPos;
    public int yPos;
    public PlayerMovement.States characterState;

    public Stats stats;

    void Start()
    {
        Panel startPos =  Grid.Instance.gridArray[xPos, -yPos];
        transform.position = new Vector2(startPos.transform.position.x, startPos.transform.position.y);
        stats = GetComponent<Stats>();

        stats.actionPoint = stats.maxActionPoint;
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
    }

    public void mouvementCheck() // check le chemin  pour atteindre le joueur
    {
        var allPlayerPos = checkAllPlayerPos();

        List<Panel> allVoisinsPlayers = new List<Panel>();

        List<Panel> finalPath = null;

        List<Panel> finalPath2 = new List<Panel>();

        int minPath = int.MaxValue;

        BlueRedGrid.Instance.movementsPossible(xPos, yPos);

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

            foreach (var voisin in allVoisinsPlayers)
            {
                List<Panel> listPanel = Grid.Instance.PathFinding(xPos, yPos, (int)voisin.x, (int)voisin.y);

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




    public IEnumerator movement(List<Panel> panelsList) // se deplace au plus proche de sa cible
    {
        Grid.Instance.resetClicked();
        var notFirst = 0;

        foreach (var panel in panelsList)
        {
            transform.position = Vector3.MoveTowards(transform.position, panel.gameObject.transform.position, 20f);
            yield return new WaitForSeconds(0.2f);

            if (notFirst != 0)
            {
                stats.actionPoint -= panel.movementCost;
            }
            notFirst++;

        }
        yield return new WaitForSeconds(0.3f);
        xPos = panelsList[panelsList.Count - 1].x;
        yPos = -panelsList[panelsList.Count - 1].y;
        characterState = PlayerMovement.States.WAIT;
        PhaseManager.Instance.checkAllEnemies();
    }
}
