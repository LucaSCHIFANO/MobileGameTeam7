using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoAttackMonster : MonoBehaviour
{

    public TutoAttackParam attackParam;
    public bool seePlayer = false;

    public void testAttackRange(int xpos, int ypos)
    {

        seePlayer = false;

        TutoGrid.Instance.resetClicked();

        TutoBlueRedGrid.Instance.actuPanelCount(xpos, ypos);

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
        foreach (var panel in TutoGrid.Instance.gridArray)
        {
            if (panel.actualPanelCount <= maxPanelCount)
            {
                if (panel.canBeCrossed || panel.canShotThrought)
                {
                    if (panel.isOccupied == true && panel.unitOn.name == "Player")
                    {
                        seePlayer = true;
                        TutoBattleManager.Instance.currentAttackParam = attackParam;
                    }
                }
            }
        }

    }

    public void AttackColor(int maxPanelCount, int xPos, int yPos)
    {
        // RIGHT --------------------------------------
        var startPanel = TutoGrid.Instance.gridArray[xPos, -yPos];
        var actuPanel = TutoGrid.Instance.gridArray[xPos, -yPos];

        bool random = false;

        while (random == false) // rigth
        {
            if (actuPanel.x + 1 < TutoGrid.Instance.width)
            {
                actuPanel = TutoGrid.Instance.gridArray[actuPanel.x + 1, -yPos];

                if (actuPanel.actualPanelCount <= maxPanelCount)
                {
                    if (attackParam.throughWall || actuPanel.canBeCrossed || actuPanel.canShotThrought)
                    {
                        if (actuPanel.isOccupied == true && actuPanel.unitOn.name == "Player")
                        {
                            seePlayer = true;
                            TutoBattleManager.Instance.currentAttackParam = attackParam;
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
        startPanel = TutoGrid.Instance.gridArray[xPos, -yPos];
        actuPanel = TutoGrid.Instance.gridArray[xPos, -yPos];

        random = false;

        while (random == false)
        {
            if (actuPanel.x - 1 > -1)
            {
                actuPanel = TutoGrid.Instance.gridArray[actuPanel.x - 1, -yPos];

                if (actuPanel.actualPanelCount <= maxPanelCount)
                {
                    if (attackParam.throughWall || actuPanel.canBeCrossed)
                    {
                        if (actuPanel.canBeCrossed || actuPanel.canShotThrought)
                        {
                            if (actuPanel.isOccupied == true && actuPanel.unitOn.name == "Player")
                            {
                                seePlayer = true;
                                TutoBattleManager.Instance.currentAttackParam = attackParam;
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
        startPanel = TutoGrid.Instance.gridArray[xPos, -yPos];
        actuPanel = TutoGrid.Instance.gridArray[xPos, -yPos];

        random = false;

        while (random == false)
        {
            if (actuPanel.y - 1 > -1)
            {
                actuPanel = TutoGrid.Instance.gridArray[xPos, actuPanel.y - 1];

                if (actuPanel.actualPanelCount <= maxPanelCount)
                {
                    if (attackParam.throughWall || actuPanel.canBeCrossed)
                    {
                        if (actuPanel.isOccupied == true && actuPanel.unitOn.name == "Player")
                        {
                            seePlayer = true;
                            TutoBattleManager.Instance.currentAttackParam = attackParam;
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
        startPanel = TutoGrid.Instance.gridArray[xPos, -yPos];
        actuPanel = TutoGrid.Instance.gridArray[xPos, -yPos];

        random = false;

        while (random == false)
        {
            if (actuPanel.y + 1 < TutoGrid.Instance.height)
            {
                actuPanel = TutoGrid.Instance.gridArray[xPos, actuPanel.y + 1];

                if (actuPanel.actualPanelCount <= maxPanelCount)
                {
                    if (attackParam.throughWall || actuPanel.canBeCrossed || actuPanel.canShotThrought)
                    {
                        if (actuPanel.isOccupied == true && actuPanel.unitOn.name == "Player")
                        {
                            seePlayer = true;
                            TutoBattleManager.Instance.currentAttackParam = attackParam;
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

