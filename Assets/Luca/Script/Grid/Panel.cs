using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public int x;
    public int y;

    public int GCost;
    public int HCost;
    public int FCost;

    public Color baseColor;

    public bool canBeCrossed = true;
    public bool canShotThrought = true;

    public Panel prevousPanel;


    public int movementCost;
    public int actualMovementCost;

    public int actualPanelCount;

    public bool canBeClick;

    public bool isOccupied;
    public GameObject unitOn;

    public Collider2D col;

    public bool canBeOpen;
    public bool isOpen;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    public void setValue(int xValue, int yValue, int GValue, int HValue)
    {
        x = xValue;
        y = yValue;
        GCost = GValue;
        HCost = HValue;
        FCost = GCost + HCost;

    }

    public void ActuFCost()
    {
        FCost = GCost + HCost;
    }


    public void OnTriggerEnter2D(Collider2D collision) // check si un perso entre sur la case
    {
        if (collision.gameObject.tag == "Characters")
        {
            if (unitOn == null)
            {
                isOccupied = true;
                unitOn = collision.gameObject;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision) // check si un perso sort de la case
    {
        if (collision.gameObject == unitOn)
        {
            isOccupied = false;
            unitOn = null;
        }
    }

}

