using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicklManager : MonoBehaviour
{
    public Panel currentPanel = null;

    private static ClicklManager _instance = null;

    public static ClicklManager Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }



    void Update() // check les differentes possibilité de click
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollier = Physics2D.OverlapPoint(touchPosition);
                var player = CharacterManager.Instance.currentPlayer.GetComponent<PlayerMovement>();

                if (touchedCollier != null) {

                    if (touchedCollier.gameObject.tag == "Panel")
                    {
                        Debug.Log("panel touched");
                        var touchedPanel = touchedCollier.gameObject.GetComponent<Panel>();

                        if (touchedPanel.canBeClick)
                        {
                            if (currentPanel != touchedPanel)
                            {
                                if (currentPanel != null)
                                {
                                    var panelColor = Grid.Instance.gridArrayAlpha[currentPanel.x, currentPanel.y].GetComponent<SpriteRenderer>();
                                    panelColor.color = new Color(panelColor.color.r, panelColor.color.g, panelColor.color.b, 0.5f);
                                }
                                currentPanel = touchedPanel;
                            }

                            else
                            {

                                if (player.state == PlayerMovement.States.SELECTED)
                                {
                                    player.StartCoroutine(player.movement(Grid.Instance.PathFinding(player.xPos, player.yPos, touchedPanel.x, touchedPanel.y)));
                                    currentPanel = null;
                                }

                                else if (player.state == PlayerMovement.States.ACTION)
                                {
                                    if (touchedPanel.unitOn != null)
                                    {
                                        BattleManager.Instance.attackUnit(player.stats, touchedPanel.unitOn.GetComponent<Enemy>().stats);
                                        CharacterManager.Instance.currentPlayer.stats.actionPoint -= BattleManager.Instance.currentAttackParam.APNeeded;
                                        UiActionManager.Instance.setMovePoint();
                                        currentPanel = null;
                                    }
                                }
                            }
                        }

                    }
                    else if (touchedCollier.gameObject.tag == "ClickableChar")
                    {

                        if (touchedCollier.gameObject.GetComponentInParent<PlayerMovement>())
                        {
                            if (player.state == PlayerMovement.States.IDLE && player.stats.actionPoint > 0)
                            {
                                Grid.Instance.resetClicked();
                                BlueRedGrid.Instance.movementsPossible(player.xPos, player.yPos);
                                BlueRedGrid.Instance.blueRedPath(player.stats.actionPoint);

                                UiActionManager.Instance.hideButton();

                                player.state = PlayerMovement.States.SELECTED;

                                UiActionManager.Instance.ShowPortrait(player.stats);

                            }else if (player.state == PlayerMovement.States.SELECTED)
                            {
                                Grid.Instance.resetClicked();
                                player.state = PlayerMovement.States.IDLE;
                                UiActionManager.Instance.showButton();
                            }

                        }
                        
                        else if (touchedCollier.gameObject.GetComponentInParent<Enemy>())
                        {
                            var charact = touchedCollier.gameObject.GetComponentInParent<Enemy>();
                            var actualPanel = Grid.Instance.gridArray[charact.xPos, -charact.yPos];

                            if (player.state == PlayerMovement.States.ACTION && actualPanel.canBeClick)
                            {
                                Debug.Log("test");

                                if(actualPanel == currentPanel)
                                {
                                    BattleManager.Instance.attackUnit(player.stats, charact.stats);
                                    CharacterManager.Instance.currentPlayer.stats.actionPoint -= BattleManager.Instance.currentAttackParam.APNeeded;
                                    UiActionManager.Instance.setMovePoint();
                                    currentPanel = null;
                                }
                                else
                                {
                                    currentPanel = actualPanel;
                                }
                            }
                            if (player.state == PlayerMovement.States.IDLE)
                            {
                                UiActionManager.Instance.ShowPortrait(charact.stats);
                            }
                        }
                    }
                }

            }
        }


        if(currentPanel != null)
        {
            shinningPannel();
        }
    }

    public void shinningPannel()
    {
        var panelColor = Grid.Instance.gridArrayAlpha[currentPanel.x, currentPanel.y].GetComponent<SpriteRenderer>();
        panelColor.color = Color.Lerp(new Color(panelColor.color.r, panelColor.color.g, panelColor.color.b, 0.5f), new Color(panelColor.color.r, panelColor.color.g, panelColor.color.b, 1f), Mathf.PingPong(Time.time * 5, 0.5f));
    }


    public void cancelButton()
    {
        var player = CharacterManager.Instance.currentPlayer.GetComponent<PlayerMovement>();

        if (player.state == PlayerMovement.States.SELECTED)
        {
            Grid.Instance.resetClicked();
            player.state = PlayerMovement.States.IDLE;
            UiActionManager.Instance.showButton();
        }

        else if (player.state == PlayerMovement.States.SELECTCARD)
        {
            Grid.Instance.resetClicked();
            player.state = PlayerMovement.States.IDLE;
            UiActionManager.Instance.hideAll();
            UiActionManager.Instance.showButton();
        }

        else if (player.state == PlayerMovement.States.ACTION)
        {
            Grid.Instance.resetClicked();
            player.state = PlayerMovement.States.SELECTCARD;
            UiActionManager.Instance.showDeck();
        }
    }
}
