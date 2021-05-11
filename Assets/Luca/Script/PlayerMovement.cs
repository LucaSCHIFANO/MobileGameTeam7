using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int xPos;
    public int yPos;

    public int lastX;
    public int lastY;

    public void Start()
    {
        transform.position = new Vector3(xPos, yPos, -10);
        lastX = xPos;
        lastY = yPos;
    }

    void Update()
    {
        
    }

    public void testMove(Vector2 pos)
    {
        transform.position = pos;
    }



    public IEnumerator movement(List<Panel> panelsList)
    {
        foreach (var panel in panelsList)
        {
            transform.position = Vector3.MoveTowards(transform.position, panel.gameObject.transform.position, 10f);
            yield return new WaitForSeconds(0.2f);
        }
        xPos = panelsList[panelsList.Count - 1].x;
        yPos = -panelsList[panelsList.Count - 1].y;
    }
}
