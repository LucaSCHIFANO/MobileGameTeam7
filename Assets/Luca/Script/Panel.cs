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

    public Panel prevousPanel;


    public int movementCost;
    public int actualMovementCost;
    public bool canBeClick;

    public bool isOccupied;
    public GameObject unitOn;

    public Collider2D col;

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


    private void Update()
    {
        
    }

}

