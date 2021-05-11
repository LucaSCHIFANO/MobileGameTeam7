using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int xPos;
    public int yPos;

    public int lastX;
    public int lastY;

    public int maxMouvementPoint;
    public int mouvementPoint;

    public States state = 0;
    public enum States
    {
        IDLE,
        SELECTED,
        MOVEMENT,
        ACTION,
        WAIT,
    }

    public void Start()
    {
        transform.position = new Vector3(xPos, yPos, -10);
        lastX = xPos;
        lastY = yPos;
        mouvementPoint = maxMouvementPoint;

        UiActionManager.Instance.setMovePoint();
    }

    void Update()
    {
        if (state == States.WAIT)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.3679245f, 0.3679245f, 0.3679245f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
        }
    }

    public void testMove(Vector2 pos)
    {
        transform.position = pos;
    }



    public IEnumerator movement(List<Panel> panelsList)
    {
        Debug.Log("player mouv");
        Grid.Instance.resetClicked();

        mouvementPoint++;
        
        foreach (var panel in panelsList)
        {
            transform.position = Vector3.MoveTowards(transform.position, panel.gameObject.transform.position, 10f);
            yield return new WaitForSeconds(0.2f);
            mouvementPoint -= panel.movementCost;
        }

        xPos = panelsList[panelsList.Count - 1].x;
        yPos = -panelsList[panelsList.Count - 1].y;

        if(mouvementPoint > 0)
        {
            state = States.IDLE;
            UiActionManager.Instance.showButton();
        }
        else
        {
            state = States.WAIT;
            mouvementPoint = maxMouvementPoint;
            UiActionManager.Instance.hideButton();
        }

        UiActionManager.Instance.setMovePoint();
        PhaseManager.Instance.checkAllPlayer();

    }

    public void endTurn()
    {
        state = States.WAIT;
        mouvementPoint = maxMouvementPoint;
        UiActionManager.Instance.hideButton();
        PhaseManager.Instance.checkAllPlayer();
    }
}
