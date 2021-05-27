using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoGrid : MonoBehaviour
{
    public int height;
    public int width;

    public int cellSize;

    public Panel panel;
    public Panel panelAlpha;

    public Panel[,] gridArray;

    public Panel[,] gridArrayAlpha;

    public int levelID;
    public int progress;

    public List<Panel> openList = new List<Panel>();
    public List<Panel> closeList = new List<Panel>();

    public List<Vector2> locationEnemy = new List<Vector2>();
    public List<Vector2> playerSpawn = new List<Vector2>();
    
    public GameObject playerPrefab;
    public GameObject enemy;
    
    private CreateAnEnemy cae;

    public MoveCam mC;



    private static TutoGrid _instance = null;

    public static TutoGrid Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;

        /*awake2();
        awake2Alpha();
        createEnemies();
        setPos();

        mC.functionStart();*/
    }

    public void Start()
    {
        CardManager.Instance.startTheGame();
    }

    public void functionStart()
    {
        awake2();
        awake2Alpha();
        createEnemies();
        setPos();

        mC.functionStart();
        CardManager.Instance.EndRound();
        PhaseManager.Instance.oneTime = false;
    }


    void awake2() // crée la grid avec tt les cases en fct du pattern
    {
        var myGridPanel = GetComponent<GridPattern>().createPattern(levelID);
        gridArray = new Panel[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Panel newPanel = Instantiate(panel, new Vector3(i * cellSize * 0.866f, -j * cellSize, 0), transform.rotation);
                //newPanel.gameObject.transform.rotation = Quaternion.Euler(60, 30, 0);


                if (j == 0 && i != 0)
                {
                    Panel panelPrevious = gridArray[i - 1, j];
                    newPanel.gameObject.transform.position = new Vector2(panelPrevious.transform.position.x + 0.5f * cellSize, panelPrevious.transform.position.y - 0.25f * cellSize);
                }
                if (j > 0)
                {
                    Panel panelPrevious = gridArray[i, j-1];
                    newPanel.gameObject.transform.position = new Vector2(panelPrevious.transform.position.x - 0.5f * cellSize, panelPrevious.transform.position.y -0.25f * cellSize);
                }

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
                        newPanel.canShotThrought = false;
                        break;
                    case GridPattern.panelType.BRIDGE:
                        newPanel.baseColor = new Color(0.5943f, 0.1720f, 0);
                        newPanel.movementCost = 1;
                        break;
                    case GridPattern.panelType.HOLE:
                        newPanel.baseColor = new Color(0.45f, 0.45f, 0.45f);
                        newPanel.movementCost = 255;
                        newPanel.canBeCrossed = false;
                        break;
                    case GridPattern.panelType.CHEST:
                        newPanel.baseColor = new Color(0.7f, 0.5f, 0.5f);
                        newPanel.movementCost = 1;
                        newPanel.canBeOpen = true;
                        newPanel.isOpen = false;
                        break;
                    default:
                        Debug.Log("ya un pb");
                        break;
                }

                newPanel.GetComponent<SpriteRenderer>().color = newPanel.baseColor;
                newPanel.transform.parent = GameObject.Find("TheGrid").transform;

            }
        }
    }

    void awake2Alpha() // crée la grid en transparent
    {
        gridArrayAlpha = new Panel[width, height];
        //var myGridPanel = GetComponent<GridPattern>().createPattern(levelID);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Panel newPanel = Instantiate(panelAlpha, new Vector3(i * cellSize * 0.866f, -j * cellSize, 0), transform.rotation);
                //newPanel.gameObject.transform.rotation = Quaternion.Euler(60, 30, 0);


                if (j == 0 && i != 0)
                {
                    Panel panelPrevious = gridArrayAlpha[i - 1, j];
                    newPanel.gameObject.transform.position = new Vector2(panelPrevious.transform.position.x + 0.5f * cellSize, panelPrevious.transform.position.y - 0.25f * cellSize);
                }
                if (j > 0)
                {
                    Panel panelPrevious = gridArrayAlpha[i, j - 1];
                    newPanel.gameObject.transform.position = new Vector2(panelPrevious.transform.position.x - 0.5f * cellSize, panelPrevious.transform.position.y - 0.25f * cellSize);
                }

                gridArrayAlpha[i, j] = newPanel;

                newPanel.setValue(i, j, 0, 0);
                newPanel.name = i + " , " + j + " alpha mode";

                /*switch (myGridPanel[j, i])
                {
                    case GridPattern.panelType.GRASS:
                        newPanel.baseColor = new Color(0f, 0f, 0f, 0f);
                        newPanel.movementCost = 1;
                        break;
                    case GridPattern.panelType.PATH:
                        newPanel.baseColor = new Color(0f, 0f, 0f, 0f);
                        newPanel.movementCost = 1;
                        break;
                    case GridPattern.panelType.FOREST:
                        newPanel.baseColor = new Color(0f, 0f, 0f, 0f);
                        newPanel.movementCost = 3;
                        break;
                    case GridPattern.panelType.WATER:
                        newPanel.baseColor = new Color(0f, 0f, 0f, 0f);
                        newPanel.movementCost = 4;
                        break;
                    case GridPattern.panelType.WALL:
                        newPanel.baseColor = new Color(0f, 0f, 0f, 0f);
                        newPanel.movementCost = 255;
                        newPanel.canBeCrossed = false;
                        break;
                    case GridPattern.panelType.BRIDGE:
                        newPanel.baseColor = new Color(0f, 0f, 0f, 0f);
                        newPanel.movementCost = 1;
                        break;
                    default:
                        Debug.Log("ya un pb alpha");
                        break;
                }*/

                newPanel.GetComponent<SpriteRenderer>().color = newPanel.baseColor;
                newPanel.transform.parent = GameObject.Find("TheGridAlpha").transform;

            }
        }
    }

    void createEnemies()
    {
        cae = GetComponent<CreateAnEnemy>();
        foreach (var VECTOR in locationEnemy)
        {
            var en = Instantiate(enemy, Vector2.zero, transform.rotation);
            var enE = en.GetComponent<Enemy>();

            enE.xPos = (int)VECTOR.x;
            enE.yPos = (int)VECTOR.y;
            enE.Start();

            cae.creation(enE, levelID, progress);
        }
        
        locationEnemy.Clear();
    }

    public void createPlayer(Panel panel)
    {
        Debug.Log("creatkon du joueur");
        var en = Instantiate(playerPrefab, Vector2.zero, transform.rotation);
        var enE = en.GetComponent<PlayerMovement>();
        CharacterManager.Instance.currentPlayer = enE;

        enE.xPos = panel.x;
        enE.yPos = -panel.y;
        enE.Start();

        playerSpawn.Clear();

        PhaseManager.Instance.phase = PhaseManager.actualPhase.PLAYER;
        en.name = "Player";

        if(CharacterManager.Instance.sS.firstTime == false)
        {
            CharacterManager.Instance.sS.setValues(enE.stats);
            enE.stats.HP = enE.stats.maxHP;
        }
        else
        {
            setPlayerStats(enE.stats, CharacterManager.Instance.sS.loadValue());
        }

        resetClicked();
    }

    void setPos()
    {
        foreach (var VECTOR in playerSpawn)
        {
            var alphaPanel = Grid.Instance.gridArrayAlpha[(int)VECTOR.x, -(int)VECTOR.y];
            alphaPanel.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 0.5f);
            Grid.Instance.gridArray[(int)VECTOR.x, -(int)VECTOR.y].canBeClick = true;
        }
    }


    public void deleteMap(bool respawn)
    {
        if (width != 0 || height != 0)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    var panel = gridArray[i, j];
                    Destroy(panel.gameObject);

                    panel = gridArrayAlpha[i, j];
                    Destroy(panel.gameObject);
                }
            }
        }

        width = 0;
        height = 0;

        ClicklManager.Instance.currentPanel = null;

        CharacterManager.Instance.currentPlayer = null;
        foreach (var item in CharacterManager.Instance.playerList)
        {
            Destroy(item);
        }
        CharacterManager.Instance.playerList.Clear();

        foreach (var item in CharacterManager.Instance.enemyList)
        {
            Destroy(item);
        }
        CharacterManager.Instance.enemyList.Clear();

        if(respawn == true)
        {
            PhaseManager.Instance.phase = PhaseManager.actualPhase.BEGIN;

            functionStart();

        }

    }

    public List<Panel> PathFinding(int xStart, int yStart, int xEnd, int yEnd)  // trouve le chemins le plus court pour allez sur une case
    {
        openList.Clear();
        closeList.Clear();


        Panel startPanel = gridArray[xStart, -yStart];
        Panel endPanel = gridArray[xEnd, yEnd];
        ActuAllPanelCost(xEnd, yEnd);

        openList.Add(startPanel);

        startPanel.GCost = 0;
        startPanel.actualPanelCount = 0;

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

                if (!voisin.canBeCrossed || voisin.unitOn != null)
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

    public void ActuAllPanelCost(int xEnd, int yEnd) // verifie le coup en deplacement d'une case
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
                Panel alphaPanel = gridArrayAlpha[i, j];
                panelToCheck.canBeClick = false;
                alphaPanel.gameObject.GetComponent<SpriteRenderer>().color = panelToCheck.baseColor;
                panelToCheck.actualPanelCount = 0;
            }
        }

        ClicklManager.Instance.currentPanel = null;
    }

    private int CalculateHCost(Panel start, Panel end) // verifie la distance d'un chemin
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


    public List<Panel> CheckVoisin(Panel currentPanel) // verifie si les voisins d'une case et les renvoie
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


    private void setPlayerStats(Stats stats, Stats stats2) // a changer aussi dans saveStats
    {
        stats.level = stats2.level;
        stats.maxHP = stats2.maxHP;
        stats.HP = stats2.HP;
        stats.strenght = stats2.strenght;
        stats.defense = stats2.defense;
        stats.speed = stats2.speed;
        stats.maxActionPoint = stats2.maxActionPoint;
        stats.actionPoint = stats2.maxActionPoint;
        stats.element = stats2.element;
        stats.elementCombo = stats2.elementCombo;
    }
}
