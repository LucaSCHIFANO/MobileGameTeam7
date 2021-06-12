using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoShowRangeAttack : MonoBehaviour
{
    public void testAttackRange(TutoAttackParam attackParam)
    {
        TutoGrid.Instance.resetClicked();

        var player = TutoCharacterManager.Instance.currentPlayer;
        TutoBlueRedGrid.Instance.actuPanelCount(player.xPos, player.yPos);

        if (attackParam.row_column == false)
        {
            AttackColor(attackParam);
        }
        else
        {
            AttackColor(attackParam, player.xPos, player.yPos);
        }

    }


    public void testAttackRange(TutoAttackParam attackParam, int x, int y)
    {
        TutoGrid.Instance.resetClicked();

        var player = TutoCharacterManager.Instance.currentPlayer;
        TutoBlueRedGrid.Instance.actuPanelCount(x, y);

        if (attackParam.row_column == false)
        {
            AttackColor(attackParam);
        }
        else
        {
            AttackColor(attackParam, x, y);
        }

        var actuPanel = TutoGrid.Instance.gridArray[x, -y];
        actuPanel.canBeClick = true;
        var alphaPanel = TutoGrid.Instance.gridArrayAlpha[actuPanel.x, actuPanel.y].transform.GetChild(0).GetComponent<SpriteRenderer>(); ;
        alphaPanel.color = new Color(1, 1, 1, 0.5f);
        alphaPanel.sprite = TutoGrid.Instance.listSpritesAlpha[1];

    }


    public void AttackColor(TutoAttackParam attackParam)
    {
        foreach (var panel in TutoGrid.Instance.gridArray)
        {
            if (panel.actualPanelCount != 0 && panel.actualPanelCount <= attackParam.range)
            {
                if (panel.canBeCrossed)
                {
                    panel.canBeClick = true;
                    var alphaPanel = TutoGrid.Instance.gridArrayAlpha[panel.x, panel.y].transform.GetChild(0).GetComponent<SpriteRenderer>();
                    alphaPanel.color = new Color(1, 1, 1, 0.5f);
                    alphaPanel.sprite = TutoGrid.Instance.listSpritesAlpha[1];
                }
            }
        }

    }

    public void AttackColor(TutoAttackParam attackParam, int xPos, int yPos)
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

                if (actuPanel.actualPanelCount <= attackParam.range)
                {
                    if (attackParam.throughWall || actuPanel.canBeCrossed || actuPanel.canShotThrought)
                    {
                        if (!actuPanel.canShotThrought)
                        {
                            actuPanel.canBeClick = true;
                            var alphaPanel = TutoGrid.Instance.gridArrayAlpha[actuPanel.x, actuPanel.y].transform.GetChild(0).GetComponent<SpriteRenderer>();
                            alphaPanel.color = new Color(1, 1, 1, 0.5f);
                            alphaPanel.sprite = TutoGrid.Instance.listSpritesAlpha[1];

                            if (actuPanel.unitOn != null)
                            {
                                if (actuPanel.unitOn.GetComponent<TutoPlayerMovement>() && TutoBattleManager.Instance.currentAttackParam.around)
                                {
                                    actuPanel.canBeClick = false;
                                }

                                else if (!attackParam.throughWall)
                                {
                                    random = true;
                                }
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
        startPanel = TutoGrid.Instance.gridArray[xPos, -yPos];
        actuPanel = TutoGrid.Instance.gridArray[xPos, -yPos];

        random = false;

        while (random == false)
        {
            if (actuPanel.x - 1 > -1)
            {
                actuPanel = TutoGrid.Instance.gridArray[actuPanel.x - 1, -yPos];

                if (actuPanel.actualPanelCount <= attackParam.range)
                {
                    if (attackParam.throughWall || actuPanel.canBeCrossed || actuPanel.canShotThrought)
                    {
                        if (!actuPanel.canShotThrought)
                        {
                            actuPanel.canBeClick = true;
                            var alphaPanel = TutoGrid.Instance.gridArrayAlpha[actuPanel.x, actuPanel.y].transform.GetChild(0).GetComponent<SpriteRenderer>();
                            alphaPanel.color = new Color(1, 1, 1, 0.5f);
                            alphaPanel.sprite = TutoGrid.Instance.listSpritesAlpha[1];

                            if (actuPanel.unitOn != null)
                            {
                                if (actuPanel.unitOn.GetComponent<TutoPlayerMovement>() && TutoBattleManager.Instance.currentAttackParam.around)
                                {
                                    actuPanel.canBeClick = false;
                                }

                                else if (!attackParam.throughWall)
                                {
                                    random = true;
                                }
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

                if (actuPanel.actualPanelCount <= attackParam.range)
                {
                    if (attackParam.throughWall || actuPanel.canBeCrossed || actuPanel.canShotThrought)
                    {
                        if (!actuPanel.canShotThrought)
                        {
                            actuPanel.canBeClick = true;
                            var alphaPanel = TutoGrid.Instance.gridArrayAlpha[actuPanel.x, actuPanel.y].transform.GetChild(0).GetComponent<SpriteRenderer>();
                            alphaPanel.color = new Color(1, 1, 1, 0.5f);
                            alphaPanel.sprite = TutoGrid.Instance.listSpritesAlpha[1];

                            if (actuPanel.unitOn != null)
                            {
                                if (actuPanel.unitOn.GetComponent<TutoPlayerMovement>() && TutoBattleManager.Instance.currentAttackParam.around)
                                {
                                    actuPanel.canBeClick = false;
                                }

                                else if (!attackParam.throughWall)
                                {
                                    random = true;
                                }
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
        startPanel = TutoGrid.Instance.gridArray[xPos, -yPos];
        actuPanel = TutoGrid.Instance.gridArray[xPos, -yPos];

        random = false;

        while (random == false)
        {
            if (actuPanel.y + 1 < TutoGrid.Instance.height)
            {
                actuPanel = TutoGrid.Instance.gridArray[xPos, actuPanel.y + 1];

                if (actuPanel.actualPanelCount <= attackParam.range)
                {
                    if (attackParam.throughWall || actuPanel.canBeCrossed || actuPanel.canShotThrought)
                    {
                        if (!actuPanel.canShotThrought)
                        {
                            actuPanel.canBeClick = true;
                            var alphaPanel = TutoGrid.Instance.gridArrayAlpha[actuPanel.x, actuPanel.y].transform.GetChild(0).GetComponent<SpriteRenderer>();
                            alphaPanel.color = new Color(1, 1, 1, 0.5f);
                            alphaPanel.sprite = TutoGrid.Instance.listSpritesAlpha[1];

                            if (actuPanel.unitOn != null)
                            {
                                if (actuPanel.unitOn.GetComponent<TutoPlayerMovement>() && TutoBattleManager.Instance.currentAttackParam.around)
                                {
                                    actuPanel.canBeClick = false;
                                }

                                else if (!attackParam.throughWall)
                                {
                                    random = true;
                                }
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