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
                if (panel.canBeCrossed)
                {
                    var alphaPanel = Grid.Instance.gridArrayAlpha[panel.x, panel.y].transform.GetChild(0).GetComponent<SpriteRenderer>();
                    alphaPanel.color = new Color(1, 1, 1, 0.5f);
                    alphaPanel.sprite = Grid.Instance.listSpritesAlpha[1];

                }
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
                    if (throughWall || actuPanel.canBeCrossed || actuPanel.canShotThrought)
                    {

                        var alphaPanel = Grid.Instance.gridArrayAlpha[actuPanel.x, actuPanel.y].transform.GetChild(0).GetComponent<SpriteRenderer>();
                        alphaPanel.color = new Color(alphaPanel.color.r, alphaPanel.color.g, alphaPanel.color.b, 0.5f);
                        alphaPanel.sprite = Grid.Instance.listSpritesAlpha[1];
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
                    if (throughWall || actuPanel.canBeCrossed || actuPanel.canShotThrought)
                    {
                        var alphaPanel = Grid.Instance.gridArrayAlpha[actuPanel.x, actuPanel.y].transform.GetChild(0).GetComponent<SpriteRenderer>();
                        alphaPanel.color = new Color(alphaPanel.color.r, alphaPanel.color.g, alphaPanel.color.b, 0.5f);
                        alphaPanel.sprite = Grid.Instance.listSpritesAlpha[1];
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
                    if (throughWall || actuPanel.canBeCrossed || actuPanel.canShotThrought)
                    {
                        var alphaPanel = Grid.Instance.gridArrayAlpha[actuPanel.x, actuPanel.y].transform.GetChild(0).GetComponent<SpriteRenderer>();
                        alphaPanel.color = new Color(alphaPanel.color.r, alphaPanel.color.g, alphaPanel.color.b, 0.5f);
                        alphaPanel.sprite = Grid.Instance.listSpritesAlpha[1];
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
                    if (throughWall || actuPanel.canBeCrossed || actuPanel.canShotThrought)
                    {
                        var alphaPanel = Grid.Instance.gridArrayAlpha[actuPanel.x, actuPanel.y].transform.GetChild(0).GetComponent<SpriteRenderer>();
                        alphaPanel.color = new Color(0, 0, 0, 0.5f);
                        alphaPanel.sprite = Grid.Instance.listSpritesAlpha[1];
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
