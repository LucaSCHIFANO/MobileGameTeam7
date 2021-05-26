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
        ///if (Input.touchCount > 0)
        if(Input.GetMouseButton(0))
        {
            if (!MenuPause.GameIsPaused)
            {
                //Touch touch = Input.GetTouch(0);
                //Touch touch = Input.GetMouseButton(0);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(/*touch.position*/ Input.mousePosition);

                //if (touch.phase == TouchPhase.Began)
                if(Input.GetMouseButtonDown(0))
                {
                    Collider2D touchedCollier = Physics2D.OverlapPoint(touchPosition);

                    if (touchedCollier != null)
                    {

                        if (PhaseManager.Instance.phase != PhaseManager.actualPhase.BEGIN)
                        {
                            var player = CharacterManager.Instance.currentPlayer.GetComponent<PlayerMovement>();

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
                                            var panelColor = Grid.Instance.gridArrayAlpha[currentPanel.x, currentPanel.y].transform.GetChild(0).GetComponent<SpriteRenderer>();
                                            panelColor.color = new Color(1, 1, 1, 0.5f);
                                            panelColor.sprite = Grid.Instance.listSpritesAlpha[0];
                                        }
                                        currentPanel = touchedPanel;
                                    }

                                    else
                                    {

                                        if (player.state == PlayerMovement.States.SELECTED)
                                        {
                                            player.StartCoroutine(player.movement(Grid.Instance.PathFinding(player.xPos, player.yPos, touchedPanel.x, touchedPanel.y, false)));
                                            currentPanel = null;
                                        }

                                        else if (player.state == PlayerMovement.States.ACTION)
                                        {
                                            if (BattleManager.Instance.currentAttackParam.AOE && !BattleManager.Instance.currentAttackParam.around)
                                            {
                                                player.state = PlayerMovement.States.AOESELECT;
                                                UiActionManager.Instance.showAttackRange(BattleManager.Instance.currentAttackParam.aoeEffect, currentPanel.x, -currentPanel.y);
                                                CardManager.Instance.UseCard();
                                                CardManager.Instance.midToHand = false;
                                            }
                                            else
                                            {
                                                if (touchedPanel.unitOn != null)
                                                {
                                                    BattleManager.Instance.attackUnit(player.stats, touchedPanel.unitOn.GetComponent<Enemy>().stats, false);
                                                    CharacterManager.Instance.currentPlayer.stats.actionPoint -= BattleManager.Instance.currentAttackParam.APNeeded;
                                                    UiActionManager.Instance.setMovePoint();
                                                    currentPanel = null;
                                                    CardManager.Instance.UseCard();
                                                    CardManager.Instance.midToHand = false;
                                                }
                                            }
                                        }
                                        else if (player.state == PlayerMovement.States.AOESELECT)
                                        {
                                            if (touchedPanel.unitOn != null)
                                            {
                                                foreach (var panel in Grid.Instance.gridArray)
                                                {
                                                    if (panel.canBeClick && panel.unitOn != null)
                                                    {
                                                        if (panel.unitOn.GetComponent<Enemy>())
                                                        {
                                                            BattleManager.Instance.attackUnit(player.stats, panel.unitOn.GetComponent<Enemy>().stats, true);
                                                            Debug.Log("hit enemy");
                                                        }
                                                        else
                                                        {
                                                            BattleManager.Instance.attackUnit(player.stats, player.stats, true);
                                                            Debug.Log("hit myself");
                                                        }

                                                    }
                                                }

                                                CardManager.Instance.UseCard();
                                                CardManager.Instance.midToHand = false;

                                                CharacterManager.Instance.currentPlayer.stats.actionPoint -= BattleManager.Instance.currentAttackParam.APNeeded;
                                                CharacterManager.Instance.StartCoroutine("checkAlive");

                                                Grid.Instance.resetClicked();
                                                player.GetComponent<PlayerMovement>().state = PlayerMovement.States.IDLE;
                                                ElementInteract.Instance.changeElement(player.stats.element, BattleManager.Instance.currentAttackParam.element);
                                                UiActionManager.Instance.showButton();
                                                UiActionManager.Instance.HidePortrait();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (player.state == PlayerMovement.States.IDLE)
                                    {
                                        if (touchedPanel.unitOn != null && touchedPanel.unitOn.GetComponent<PlayerMovement>())
                                        {
                                            Grid.Instance.resetClicked();
                                            BlueRedGrid.Instance.movementsPossible(player.xPos, player.yPos, false);
                                            BlueRedGrid.Instance.blueRedPath(player.stats.actionPoint);

                                            UiActionManager.Instance.hideButton();

                                            player.state = PlayerMovement.States.SELECTED;

                                            UiActionManager.Instance.ShowPortrait(player.stats);
                                        }
                                        else if (touchedPanel.unitOn != null && touchedPanel.unitOn.GetComponent<Enemy>())
                                        {
                                            UiActionManager.Instance.ShowPortrait(touchedPanel.unitOn.GetComponent<Enemy>().stats);
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
                                        BlueRedGrid.Instance.movementsPossible(player.xPos, player.yPos, false);
                                        BlueRedGrid.Instance.blueRedPath(player.stats.actionPoint);

                                        UiActionManager.Instance.hideButton();

                                        player.state = PlayerMovement.States.SELECTED;

                                        UiActionManager.Instance.ShowPortrait(player.stats);

                                    }
                                    else if (player.state == PlayerMovement.States.SELECTED)
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

                                        if (actualPanel == currentPanel)
                                        {
                                            if (BattleManager.Instance.currentAttackParam.AOE && !BattleManager.Instance.currentAttackParam.around)
                                            {
                                                player.state = PlayerMovement.States.AOESELECT;
                                                UiActionManager.Instance.showAttackRange(BattleManager.Instance.currentAttackParam.aoeEffect, currentPanel.x, -currentPanel.y);
                                                CardManager.Instance.UseCard();
                                                CardManager.Instance.midToHand = false;
                                            }
                                            
                                            else
                                            {
                                                    BattleManager.Instance.attackUnit(player.stats, charact.stats, false);
                                                    CharacterManager.Instance.currentPlayer.stats.actionPoint -= BattleManager.Instance.currentAttackParam.APNeeded;
                                                    currentPanel = null;
                                                    CardManager.Instance.UseCard();
                                                    CardManager.Instance.midToHand = false;
                                            }
                                        }
                                        else
                                        {
                                            currentPanel = actualPanel;
                                        }
                                    }

                                    else if (player.state == PlayerMovement.States.IDLE)
                                    {
                                        UiActionManager.Instance.ShowPortrait(charact.stats);

                                    }

                                    else if (player.state == PlayerMovement.States.AOESELECT)
                                    {
                                        if (actualPanel == currentPanel)
                                        {
                                            foreach (var panel in Grid.Instance.gridArray)
                                            {
                                                if (panel.canBeClick && panel.unitOn != null)
                                                {
                                                    if (panel.unitOn.GetComponent<Enemy>())
                                                    {
                                                        BattleManager.Instance.attackUnit(player.stats, panel.unitOn.GetComponent<Enemy>().stats, true);
                                                    }
                                                    else
                                                    {
                                                        BattleManager.Instance.attackUnit(player.stats, player.stats, true);
                                                    }
                                                }
                                            }

                                            CardManager.Instance.UseCard();
                                            CardManager.Instance.midToHand = false;

                                            CharacterManager.Instance.currentPlayer.stats.actionPoint -= BattleManager.Instance.currentAttackParam.APNeeded;
                                            CharacterManager.Instance.StartCoroutine("checkAlive");

                                            Grid.Instance.resetClicked();
                                            player.GetComponent<PlayerMovement>().state = PlayerMovement.States.IDLE;
                                            ElementInteract.Instance.changeElement(player.stats.element, BattleManager.Instance.currentAttackParam.element);
                                            UiActionManager.Instance.showButton();
                                            UiActionManager.Instance.HidePortrait();
                                        }
                                        else
                                        {
                                            currentPanel = actualPanel;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (touchedCollier.gameObject.tag == "Panel" && touchedCollier.gameObject.GetComponent<Panel>().canBeClick)
                            {
                                Grid.Instance.createPlayer(touchedCollier.gameObject.GetComponent<Panel>());
                                UiActionManager.Instance.showButton();
                                CharacterManager.Instance.functionStart();
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
        var panelColor = Grid.Instance.gridArrayAlpha[currentPanel.x, currentPanel.y].transform.GetChild(0).GetComponent<SpriteRenderer>();
        panelColor.color = Color.Lerp(new Color(1, 1, 1, 0.5f), new Color(1, 1, 1, 1f), Mathf.PingPong(Time.time * 5, 0.5f));
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
            var cardM = CardManager.Instance;
            if (!cardM.handToMid && !cardM.midToHand)
            {
                Grid.Instance.resetClicked();
                player.state = PlayerMovement.States.IDLE;
                UiActionManager.Instance.hideAll();
                UiActionManager.Instance.showButton();
                CardManager.Instance.handPanel.GetComponent<Animator>().SetTrigger("Hide");
                CardManager.Instance.MidToHandLaFonction();
            }
        }

        else if (player.state == PlayerMovement.States.ACTION)
        {
            Grid.Instance.resetClicked();
            player.state = PlayerMovement.States.SELECTCARD;
            UiActionManager.Instance.showDeck();
        }
        
        else if (player.state == PlayerMovement.States.AOESELECT)
        {
            Grid.Instance.resetClicked();
            player.state = PlayerMovement.States.SELECTCARD;
            UiActionManager.Instance.showDeck();
        }
    }
}
