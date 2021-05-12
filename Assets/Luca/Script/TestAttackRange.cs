using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttackRange : MonoBehaviour
{
    public int range;
    public bool row_column; // false = tout autour de toi
    public bool throughWall;
    

    public void testAttackRange()
    {
        Grid.Instance.resetClicked();

        var player = CharacterManager.Instance.currentPlayer;
        BlueRedGrid.Instance.actuPanelCount(player.xPos, player.yPos);

        if(row_column == false)
        {
            AttackColor(range);
        }
        else
        {
            AttackColor(range, player.xPos, player.yPos);
        }

    }


    public void AttackColor(int maxPanelCount) 
    {
        foreach (var panel in Grid.Instance.gridArray)
        {
            if (panel.actualPanelCount <= maxPanelCount)
            {
                panel.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }

    }

    public void AttackColor(int maxPanelCount, int xPos, int yPos) 
    {
        /*foreach (var panel in Grid.Instance.gridArray)
        {
            if (panel.actualPanelCount <= maxPanelCount && (panel.x == xPos || panel.y == -yPos))
            {
                panel.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }*/

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
                    if (throughWall || actuPanel.canBeCrossed)
                    {
                        if (actuPanel.canBeCrossed)
                        {
                            actuPanel.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
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
                    if (throughWall || actuPanel.canBeCrossed)
                    {
                        if (actuPanel.canBeCrossed)
                        {
                            actuPanel.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
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
                    if (throughWall || actuPanel.canBeCrossed)
                    {
                        if (actuPanel.canBeCrossed)
                        {
                            actuPanel.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
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
                    if (throughWall || actuPanel.canBeCrossed)
                    {
                        if (actuPanel.canBeCrossed)
                        {
                            actuPanel.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
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
