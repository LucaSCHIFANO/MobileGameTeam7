using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int xPos;
    public int yPos;

    /*public int lastX;
    public int lastY;*/

    public Stats stats;

    private GameObject panelToGo;

    public States state = 0;
    public enum States  
    {
        IDLE,
        SELECTED,
        SELECTCARD,
        ACTION,
        AOESELECT,
        WAIT,
    }

    public void Start()
    {
        Panel startPos = Grid.Instance.gridArray[xPos, -yPos]; // positionne le joueur et set ses MP (mouvement points)
        transform.position = new Vector2(startPos.transform.position.x, startPos.transform.position.y);

        stats = GetComponent<Stats>();

        /*lastX = xPos;
        lastY = yPos;*/
        stats.actionPoint = stats.maxActionPoint;
        stats.HP = stats.maxHP;

        UiActionManager.Instance.setMovePoint(); // affiche a l'écran les mouvement points 
    }

    void Update()
    {
        if (state == States.WAIT) // changement de couleur a la fin d'un tour
        {
            GetComponent<SpriteRenderer>().color = new Color(0.3679245f, 0.3679245f, 0.3679245f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
        }

        if(panelToGo != null)
        {
            trueMovement();
        }
    }


    public IEnumerator movement(List<Panel> panelsList) // envoie une list de panel a traverser 
    {
        UiActionManager.Instance.hideAll();

        Grid.Instance.resetClicked();

        //mouvementPoint++;
        var notFirst = 0;
        
        foreach (var panel in panelsList)
        {
            //transform.position = Vector3.MoveTowards(transform.position, panel.gameObject.transform.position, 20f);
            panelToGo = panel.gameObject;
            yield return new WaitForSeconds(0.2f);
            if(notFirst != 0)
            {
                stats.actionPoint -= panel.movementCost;
            }
            notFirst++;
        }

        panelToGo = null;
        xPos = panelsList[panelsList.Count - 1].x;
        yPos = -panelsList[panelsList.Count - 1].y;


        state = States.IDLE;
        UiActionManager.Instance.showButton();

        UiActionManager.Instance.setMovePoint();
        //PhaseManager.Instance.checkAllPlayer();

    }

    public void trueMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, panelToGo.gameObject.transform.position, 0.095f);
    }

    public IEnumerator isPushOrPull(Panel panelToMove)
    {
        panelToGo = panelToMove.gameObject;
        yield return new WaitForSeconds(0.2f);
        xPos = panelToMove.x;
        yPos = -panelToMove.y;
    }




    public void endTurn() // fin du tour
    {
        state = States.WAIT;
        stats.actionPoint = stats.maxActionPoint;

        stats.effectActu();

        UiActionManager.Instance.hideAll();
        UiActionManager.Instance.setMovePoint();
        PhaseManager.Instance.checkAllPlayer();
        CardManager.Instance.EndRound();
    }
}
