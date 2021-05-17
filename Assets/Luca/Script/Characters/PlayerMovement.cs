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
        Grid.Instance.resetClicked();

        foreach (var panel in panelsList)
        {
            transform.position = Vector3.MoveTowards(transform.position, panel.gameObject.transform.position, 10f);
            yield return new WaitForSeconds(0.2f);
        }

        state = States.WAIT;
        xPos = panelsList[panelsList.Count - 1].x;
        yPos = -panelsList[panelsList.Count - 1].y;

        PhaseManager.Instance.checkAllPlayer();

    }
}
