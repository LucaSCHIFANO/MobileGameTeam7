using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMonster : MonoBehaviour
{

    public AttackParam attackParam;
    public bool seePlayer = false;

    public void testAttackRange(int xpos, int ypos)
    {
        seePlayer = false;

        Grid.Instance.resetClicked();

        BlueRedGrid.Instance.actuPanelCount(xpos, ypos);

        if (attackParam.row_column == false)
        {
            AttackColor(attackParam.range);
        }
        else
        {
            AttackColor(attackParam.range, xpos, ypos);
        }

    }


    public void AttackColor(int maxPanelCount)
    {
        foreach (var panel in Grid.Instance.gridArray)
        {
            if (panel.actualPanelCount <= maxPanelCount)
            {
                if (panel.canBeCrossed)
                {
                    if(panel.isOccupied == true && panel.unitOn.name == "Player")
                    {
                        seePlayer = true;
                        BattleManager.Instance.currentAttackParam = attackParam;
                    }
                }
            }
        }

    }

    public void AttackColor(int maxPanelCount, int xPos, int yPos)
    {
        // RIGHT --------------------------------------
        var startPanel = Grid.Instance.gridArray[xPos, -yPos];
        var actuPanel = Grid.Instance.gridArray[xPos, -yPos];

        bool random = false;

        while (random == false) // rigth
        {
            if (actuPanel.x + 1 < Grid.Instance.width)
            {
                actuPanel = Grid.Instance.gridArray[actuPanel.x + 1, -yPos];

                if (actuPanel.actualPanelCount <= maxPanelCount)
                {
                    if (attackParam.throughWall || actuPanel.canBeCrossed)
                    {
                        if (actuPanel.canBeCrossed)
                        {
                            if (actuPanel.isOccupied == true && actuPanel.unitOn.name == "Player")
                            {
                                seePlayer = true;
                                BattleManager.Instance.currentAttackParam = attackParam;
                            }
                        }
                    }
                    else
                    {
                        random = true;
                    }
                }
                else
                {
                    random = true;
                }
            }
            else
            {
                random = true;
            }
        }


        // LEFT ---------------------------------------
        startPanel = Grid.Instance.gridArray[xPos, -yPos];
        actuPanel = Grid.Instance.gridArray[xPos, -yPos];

        random = false;

        while (random == false)
        {
            if (actuPanel.x - 1 > -1)
            {
                actuPanel = Grid.Instance.gridArray[actuPanel.x - 1, -yPos];

                if (actuPanel.actualPanelCount <= maxPanelCount)
                {
                    if (attackParam.throughWall || actuPanel.canBeCrossed)
                    {
                        if (actuPanel.canBeCrossed)
                        {
                            if (actuPanel.isOccupied == true && actuPanel.unitOn.name == "Player")
                            {
                                seePlayer = true;
                                BattleManager.Instance.currentAttackParam = attackParam;
                            }
                        }
                    }
                    else
                    {
                        random = true;
                    }
                }
                else
                {
                    random = true;
                }
            }
            else
            {
                random = true;
            }
        }


        //UP ------------------------------------------
        startPanel = Grid.Instance.gridArray[xPos, -yPos];
        actuPanel = Grid.Instance.gridArray[xPos, -yPos];

        random = false;

        while (random == false)
        {
            if (actuPanel.y - 1 > -1)
            {
                actuPanel = Grid.Instance.gridArray[xPos, actuPanel.y - 1];

                if (actuPanel.actualPanelCount <= maxPanelCount)
                {
                    if (attackParam.throughWall || actuPanel.canBeCrossed)
                    {
                        if (actuPanel.canBeCrossed)
                        {
                            if (actuPanel.isOccupied == true && actuPanel.unitOn.name == "Player")
                            {
                                seePlayer = true;
                                BattleManager.Instance.currentAttackParam = attackParam;
                            }
                        }
                    }
                    else
                    {
                        random = true;
                    }
                }
                else
                {
                    random = true;
                }
            }
            else
            {
                random = true;
            }
        }




        //Down ---------------------------------------
        startPanel = Grid.Instance.gridArray[xPos, -yPos];
        actuPanel = Grid.Instance.gridArray[xPos, -yPos];

        random = false;

        while (random == false)
        {
            if (actuPanel.y + 1 < Grid.Instance.height)
            {
                actuPanel = Grid.Instance.gridArray[xPos, actuPanel.y + 1];

                if (actuPanel.actualPanelCount <= maxPanelCount)
                {
                    if (attackParam.throughWall || actuPanel.canBeCrossed)
                    {
                        if (actuPanel.canBeCrossed)
                        {
                            if(actuPanel.isOccupied == true && actuPanel.unitOn.name == "Player")
                            {
                                seePlayer = true;
                                BattleManager.Instance.currentAttackParam = attackParam;
                            }
                        }
                    }
                    else
                    {
                        random = true;
                    }
                }
                else
                {
                    random = true;
                }
            }
            else
            {
                random = true;
            }
        }
    }
}
