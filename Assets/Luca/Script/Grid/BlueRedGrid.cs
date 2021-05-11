using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueRedGrid : MonoBehaviour
{
    public List<Panel> openList = new List<Panel>();
    public List<Panel> closeList = new List<Panel>();



    private static BlueRedGrid _instance = null;

    public static BlueRedGrid Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }


    public void blueRedPath(int maxMovementPlayer)
    {
        foreach (var panel in Grid.Instance.gridArray)
        {
            if (panel.actualMovementCost <= maxMovementPlayer)
            {
                panel.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                panel.canBeClick = true;
            }
        }

    }


    public void movementsPossible(int xStart, int yStart)
    {
        openList.Clear();
        closeList.Clear();

        Panel startPanel = Grid.Instance.gridArray[xStart, -yStart];

        startPanel.actualMovementCost = 0;

        openList.Add(startPanel);

        while (openList.Count > 0)
        {
            foreach (var panel in openList.ToArray())
            {
                openList.Remove(panel);
                closeList.Add(panel);

                foreach (var voisin in CheckVoisin(panel))
                {
                    if (closeList.Contains(voisin) || openList.Contains(voisin))
                    {
                        if (voisin.actualMovementCost > (panel.actualMovementCost + voisin.movementCost))
                        {
                            if (voisin.canBeCrossed)
                            {
                                voisin.prevousPanel = panel;
                                voisin.actualMovementCost = voisin.movementCost + panel.actualMovementCost;

                                if (!openList.Contains(voisin))
                                {
                                    openList.Add(voisin);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!voisin.canBeCrossed)
                        {
                            closeList.Add(voisin);
                            voisin.actualMovementCost = int.MaxValue;
                            continue;
                        }

                        else
                        {
                            voisin.prevousPanel = panel;
                            voisin.actualMovementCost = voisin.movementCost + panel.actualMovementCost;

                            if (!openList.Contains(voisin))
                            {
                                openList.Add(voisin);
                            }
                        }

                    }


                }

            }
        }
    }

    public void attackPossible()
    {
        List<Panel> listOfBlue = new List<Panel>();

        for (int i = 0; i < Grid.Instance.width; i++)
        {
            for (int j = 0; j < Grid.Instance.height; j++)
            {
                Panel panelToCheck = Grid.Instance.gridArray[i, j];
                if (panelToCheck.canBeClick)
                {
                    listOfBlue.Add(panelToCheck);
                }
            }

            foreach (var panel in listOfBlue)
            {
                foreach (var panel2 in CheckVoisin(panel))
                {
                    if (!panel2.canBeClick)
                    {
                        panel2.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                }
            }
        }

    }



    private List<Panel> CheckVoisin(Panel currentPanel)
    {
        List<Panel> voisinList = new List<Panel>();

        if (currentPanel.x - 1 >= 0) // Left
        {
            voisinList.Add(Grid.Instance.gridArray[currentPanel.x - 1, currentPanel.y]);
        }

        if (currentPanel.x + 1 < Grid.Instance.width) // Right
        {
            voisinList.Add(Grid.Instance.gridArray[currentPanel.x + 1, currentPanel.y]);
        }

        if (currentPanel.y - 1 >= 0) // Down
        {
            voisinList.Add(Grid.Instance.gridArray[currentPanel.x, currentPanel.y - 1]);
        }

        if (currentPanel.y + 1 < Grid.Instance.height) // Up
        {
            voisinList.Add(Grid.Instance.gridArray[currentPanel.x, currentPanel.y + 1]);
        }

        return voisinList;
    }
}
