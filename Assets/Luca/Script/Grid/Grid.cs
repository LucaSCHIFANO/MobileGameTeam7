using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int height;
    public int width;

    public int cellSize;
    public Panel panel;

    public Panel[,] gridArray;

    public int levelID;

    public List<Panel> openList = new List<Panel>();
    public List<Panel> closeList = new List<Panel>();



    private static Grid _instance = null;

    public static Grid Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }


    void Start()
    {
        gridArray = new Panel[width, height];
        var myGridPanel = GetComponent<GridPattern>().createPattern(levelID);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Panel newPanel = Instantiate(panel, new Vector3(i * cellSize, -j * cellSize, 0), transform.rotation);
                gridArray[i, j] = newPanel;

                newPanel.setValue(i, j, 0, 0);
                newPanel.name = i + " , " + j;

                switch (myGridPanel[j, i])
                {
                    case GridPattern.panelType.GRASS:
                        newPanel.baseColor = new Color(0.006f, 0.7075f, 0);
                        newPanel.movementCost = 1;
                        break;
                    case GridPattern.panelType.PATH:
                        newPanel.baseColor = new Color(0.8207f, 0.7423f, 0.3832f);
                        newPanel.movementCost = 1;
                        break;
                    case GridPattern.panelType.FOREST:
                        newPanel.baseColor = new Color(0.0165f, 0.3113f, 0);
                        newPanel.movementCost = 3;
                        break;
                    case GridPattern.panelType.WATER:
                        newPanel.baseColor = new Color(0.0342f, 0.401f, 0.6603f);
                        newPanel.movementCost = 4;
                        break;
                    case GridPattern.panelType.WALL:
                        newPanel.baseColor = new Color(0.1792f, 0.0518f, 0);
                        newPanel.movementCost = 255;
                        newPanel.canBeCrossed = false;
                        break;
                    case GridPattern.panelType.BRIDGE:
                        newPanel.baseColor = new Color(0.5943f, 0.1720f, 0);
                        newPanel.movementCost = 1;
                        break;
                    default:
                        Debug.Log("ya un pb");
                        break;
                }

                newPanel.GetComponent<SpriteRenderer>().color = newPanel.baseColor;

            }
        }
    }

    public List<Panel> PathFinding(int xStart, int yStart, int xEnd, int yEnd)
    {
        openList.Clear();
        closeList.Clear();


        Panel startPanel = gridArray[xStart, -yStart];
        Panel endPanel = gridArray[xEnd, yEnd];
        ActuAllPanelCost(xEnd, yEnd);

        openList.Add(startPanel);

        startPanel.GCost = 0;

        while (openList.Count > 0)
        {
            Panel currentPanel = getLowerFCost(openList);

            if (currentPanel == endPanel)
            {
                return calculatePath(endPanel);
            }

            openList.Remove(currentPanel);
            closeList.Add(currentPanel);

            foreach (var voisin in CheckVoisin(currentPanel))
            {
                if (closeList.Contains(voisin))
                {
                    continue;
                }

                if (!voisin.canBeCrossed)
                {
                    closeList.Add(voisin);
                    continue;
                }
                else
                {
                    int tentativeGCost = currentPanel.GCost + CalculateHCost(currentPanel, voisin);
                    if (tentativeGCost < voisin.GCost)
                    {
                        voisin.prevousPanel = currentPanel;
                        voisin.GCost = tentativeGCost;
                        voisin.HCost = CalculateHCost(voisin, endPanel);
                        voisin.ActuFCost();

                        if (!openList.Contains(voisin))
                        {
                            openList.Add(voisin);
                        }
                    }
                }

            }
        }
        return null;
    }

    public void ActuAllPanelCost(int xEnd, int yEnd)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Panel panelToCheck = gridArray[i, j];
                panelToCheck.GCost = int.MaxValue;
                panelToCheck.HCost = CalculateHCost(panelToCheck, gridArray[xEnd, yEnd]);
                panelToCheck.ActuFCost();
                panelToCheck.prevousPanel = null;
                panel.canBeClick = false;
            }
        }
    }

    public void resetClicked()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Panel panelToCheck = gridArray[i, j];
                panelToCheck.canBeClick = false;
                panelToCheck.gameObject.GetComponent<SpriteRenderer>().color = panelToCheck.baseColor;
            }
        }
    }

    private int CalculateHCost(Panel start, Panel end)
    {
        int xDist = Mathf.Abs(start.x - end.x);
        int yDist = Mathf.Abs(start.y - end.y);
        int remaining = Mathf.Abs(xDist - yDist);
        return (14 * Mathf.Min(xDist, yDist) + 10 * remaining) * start.movementCost;
    }

    private Panel getLowerFCost(List<Panel> listToCheck)
    {
        Panel lowestFPanel = listToCheck[0];
        for (int i = 0; i < listToCheck.Count; i++)
        {

            if (lowestFPanel.FCost > listToCheck[i].FCost)
            {
                lowestFPanel = listToCheck[i];
            }
        }

        return lowestFPanel;
    }


    public List<Panel> CheckVoisin(Panel currentPanel)
    {
        List<Panel> voisinList = new List<Panel>();

        if (currentPanel.x - 1 >= 0) // Left
        {
            voisinList.Add(gridArray[currentPanel.x - 1, currentPanel.y]);
        }

        if (currentPanel.x + 1 < width) // Right
        {
            voisinList.Add(gridArray[currentPanel.x + 1, currentPanel.y]);
        }

        if (currentPanel.y - 1 >= 0) // Down
        {
            voisinList.Add(gridArray[currentPanel.x, currentPanel.y - 1]);
        }

        if (currentPanel.y + 1 < height) // Up
        {
            voisinList.Add(gridArray[currentPanel.x, currentPanel.y + 1]);
        }

        return voisinList;
    }

    private List<Panel> calculatePath(Panel end)
    {
        List<Panel> path = new List<Panel>();
        path.Add(end);
        Panel currentPanel = end;
        while (currentPanel.prevousPanel != null)
        {
            path.Add(currentPanel.prevousPanel);
            currentPanel = currentPanel.prevousPanel;
        }
        path.Reverse();
        return path;
    }
}
