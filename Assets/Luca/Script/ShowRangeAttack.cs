using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRangeAttack : MonoBehaviour
{
    public void testAttackRange(AttackParam attackParam)
    {
        Grid.Instance.resetClicked();

        var player = CharacterManager.Instance.currentPlayer;
        BlueRedGrid.Instance.actuPanelCount(player.xPos, player.yPos);

        if (attackParam.row_column == false)
        {
            AttackColor(attackParam);
        }
        else
        {
            AttackColor(attackParam, player.xPos, player.yPos);
        }

    }


    public void testAttackRange(AttackParam attackParam, int x, int y)
    {
        Grid.Instance.resetClicked();

        var player = CharacterManager.Instance.currentPlayer;
        BlueRedGrid.Instance.actuPanelCount(x, y);

        if (attackParam.row_column == false)
        {
            AttackColor(attackParam);
        }
        else
        {
            AttackColor(attackParam, x, y);
        }

        var actuPanel = Grid.Instance.gridArray[x, -y];
        actuPanel.canBeClick = true;
        var alphaPanel = Grid.Instance.gridArrayAlpha[actuPanel.x, actuPanel.y];
        alphaPanel.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);

    }


    public void AttackColor(AttackParam attackParam)
    {
        foreach (var panel in Grid.Instance.gridArray)
        {
            if (panel.actualPanelCount != 0 && panel.actualPanelCount <= attackParam.range)
            {
                if (panel.canBeCrossed)
                {
                    panel.canBeClick = true;
                    var alphaPanel = Grid.Instance.gridArrayAlpha[panel.x, panel.y];
                    alphaPanel.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
                }
            }
        }

    }

    public void AttackColor(AttackParam attackParam, int xPos, int yPos)
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

                if (actuPanel.actualPanelCount <= attackParam.range)
                {
                    if (attackParam.throughWall || actuPanel.canBeCrossed || actuPanel.canShotThrought)
                    {
                            actuPanel.canBeClick = true;
                            var alphaPanel = Grid.Instance.gridArrayAlpha[actuPanel.x, actuPanel.y];
                            alphaPanel.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);

                            if(actuPanel.unitOn != null && !attackParam.throughWall)
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

                if (actuPanel.actualPanelCount <= attackParam.range)
                {
                    if (attackParam.throughWall || actuPanel.canBeCrossed || actuPanel.canShotThrought)
                    {
                            actuPanel.canBeClick = true;
                            var alphaPanel = Grid.Instance.gridArrayAlpha[actuPanel.x, actuPanel.y];
                            alphaPanel.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);

                            if (actuPanel.unitOn != null && !attackParam.throughWall)
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

                if (actuPanel.actualPanelCount <= attackParam.range)
                {
                    if (attackParam.throughWall || actuPanel.canBeCrossed || actuPanel.canShotThrought)
                    {
                            actuPanel.canBeClick = true;
                            var alphaPanel = Grid.Instance.gridArrayAlpha[actuPanel.x, actuPanel.y];
                            alphaPanel.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);

                            if (actuPanel.unitOn != null && !attackParam.throughWall)
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

                if (actuPanel.actualPanelCount <= attackParam.range)
                {
                    if (attackParam.throughWall || actuPanel.canBeCrossed || actuPanel.canShotThrought)
                    {
                            actuPanel.canBeClick = true;
                            var alphaPanel = Grid.Instance.gridArrayAlpha[actuPanel.x, actuPanel.y];
                            alphaPanel.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);

                            if (actuPanel.unitOn != null && !attackParam.throughWall)
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
            else
            {
                random = true;
            }
        }
    }
}
