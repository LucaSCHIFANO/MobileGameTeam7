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


    public void blueRedPath(int maxMovementPlayer) // affiche les cases en bleu si le joueur peut s'y rendre
    {
        foreach (var panel in Grid.Instance.gridArray)
        {
            if (panel.actualMovementCost <= maxMovementPlayer && panel.prevousPanel != null)
            {
                var alphaPanel = Grid.Instance.gridArrayAlpha[panel.x, panel.y];
                alphaPanel.gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,1,0.5f);
                panel.canBeClick = true;
            }
        }

    }



    public void movementsPossible(int xStart, int yStart, bool enemy)
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
                if(!closeList.Contains(panel))
                closeList.Add(panel);

                foreach (var voisin in CheckVoisin(panel))
                {
                    if (closeList.Contains(voisin) || openList.Contains(voisin))
                    {
                        if (voisin.actualMovementCost > (panel.actualMovementCost + voisin.movementCost))
                        {
                            if (voisin.canBeCrossed && voisin.unitOn == null)
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
                        if ((!voisin.canBeCrossed))
                        {
                            closeList.Add(voisin);
                            voisin.actualMovementCost = int.MaxValue;
                            continue;
                        }

                        else if (voisin.unitOn != null && enemy)
                        {
                            voisin.prevousPanel = panel;
                            voisin.actualMovementCost = voisin.movementCost + panel.actualMovementCost;

                            if (!openList.Contains(voisin))
                            {
                                openList.Add(voisin);
                            }
                        }

                        else if (voisin.unitOn != null && !enemy)
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


    public void actuPanelCount(int xStart, int yStart)
    {
        openList.Clear();
        closeList.Clear();

        Panel startPanel = Grid.Instance.gridArray[xStart, -yStart];

        startPanel.actualPanelCount = 0;

        openList.Add(startPanel);

        while (openList.Count > 0)
        {
            foreach (var panel in openList.ToArray())
            {

                openList.Remove(panel);
                if (!closeList.Contains(panel))
                    closeList.Add(panel);

                foreach (var voisin in CheckVoisin(panel))
                {
                    if (closeList.Contains(voisin) || openList.Contains(voisin))
                    {
                        if (voisin.actualPanelCount > (panel.actualPanelCount + 1))
                        {
                            if ((voisin.actualPanelCount == 0 && voisin != startPanel) || voisin.actualPanelCount > panel.actualPanelCount)
                            {
                                voisin.actualPanelCount = panel.actualPanelCount + 1;
                            }

                            if (!openList.Contains(voisin))
                            {
                                openList.Add(voisin);
                            }
                        }
                    }                


                    if ((voisin.actualPanelCount == 0 && voisin != startPanel) || voisin.actualPanelCount > panel.actualPanelCount)
                    {
                        voisin.actualPanelCount = panel.actualPanelCount + 1;

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
