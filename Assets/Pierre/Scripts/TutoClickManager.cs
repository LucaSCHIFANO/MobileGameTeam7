using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoClickManager : MonoBehaviour
{
    public Panel currentPanel = null;

    private static TutoClickManager _instance = null;


    public bool canClickPlayer;
    public bool canClickEnemy;

    public int statePanel;


    public static TutoClickManager Instance
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
        if (Input.GetMouseButton(0))
        {
            if (!MenuPause.GameIsPaused)
            {
                //Touch touch = Input.GetTouch(0);
                //Touch touch = Input.GetMouseButton(0);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(/*touch.position*/ Input.mousePosition);

                //if (touch.phase == TouchPhase.Began)
                if (Input.GetMouseButtonDown(0))
                {
                    Collider2D touchedCollier = Physics2D.OverlapPoint(touchPosition);

                    if (touchedCollier != null)
                    {

                        if (TutoPhaseManager.Instance.phase != TutoPhaseManager.actualPhase.BEGIN)
                        {
                            var player = TutoCharacterManager.Instance.currentPlayer.GetComponent<TutoPlayerMovement>();

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
                                            var panelColor = TutoGrid.Instance.gridArrayAlpha[currentPanel.x, currentPanel.y].transform.GetChild(0).GetComponent<SpriteRenderer>();
                                            panelColor.color = new Color(1, 1, 1, 0.5f);
                                            if (player.state == TutoPlayerMovement.States.ACTION || player.state == TutoPlayerMovement.States.AOESELECT)
                                            {
                                                panelColor.sprite = TutoGrid.Instance.listSpritesAlpha[1];
                                            }
                                            else
                                            {
                                                panelColor.sprite = TutoGrid.Instance.listSpritesAlpha[0];
                                            }
                                        }

                                        currentPanel = touchedPanel;

                                        if (player.state == TutoPlayerMovement.States.SELECTED)
                                        {

                                            foreach (var item in TutoGrid.Instance.gridArrayAlpha)
                                            {
                                                TutoGrid.Instance.gridArrayAlpha[item.x, item.y].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TutoGrid.Instance.listSpritesAlpha[0];
                                            }


                                            var pathShine = TutoGrid.Instance.PathFinding(player.xPos, player.yPos, touchedPanel.x, touchedPanel.y, false);
                                            foreach (var item in pathShine)
                                            {
                                                var panelColor = TutoGrid.Instance.gridArrayAlpha[item.x, item.y].transform.GetChild(0).GetComponent<SpriteRenderer>();
                                                panelColor.sprite = TutoGrid.Instance.listSpritesAlpha[2];
                                            }
                                        }
                                    }

                                    else
                                    {

                                        if (player.state == TutoPlayerMovement.States.SELECTED)
                                        {
                                            player.StartCoroutine(player.movement(TutoGrid.Instance.PathFinding(player.xPos, player.yPos, touchedPanel.x, touchedPanel.y, false)));
                                            currentPanel = null;
                                        }

                                        else if (player.state == TutoPlayerMovement.States.ACTION)
                                        {
                                            if (TutoBattleManager.Instance.currentAttackParam.AOE && !TutoBattleManager.Instance.currentAttackParam.around)
                                            {
                                                player.state = TutoPlayerMovement.States.AOESELECT;
                                                TutoUI.Instance.showAttackRange(TutoBattleManager.Instance.currentAttackParam.aoeEffect, currentPanel.x, -currentPanel.y);
                                                //CardManager.Instance.UseCard();
                                                TutoCardManager.Instance.midToHand = false;
                                            }
                                            else
                                            {
                                                if (touchedPanel.unitOn != null)
                                                {
                                                    TutoBattleManager.Instance.attackUnit(player.stats, touchedPanel.unitOn.GetComponent<TutoEnemy>().stats, false);
                                                    TutoCharacterManager.Instance.currentPlayer.stats.actionPoint -= TutoBattleManager.Instance.currentAttackParam.APNeeded;
                                                    currentPanel = null;
                                                    TutoCardManager.Instance.UseCard();
                                                    TutoCardManager.Instance.midToHand = false;
                                                }
                                            }
                                        }
                                        else if (player.state == TutoPlayerMovement.States.AOESELECT)
                                        {
                                            if (touchedPanel.unitOn != null)
                                            {
                                                foreach (var panel in TutoGrid.Instance.gridArray)
                                                {
                                                    if (panel.canBeClick && panel.unitOn != null)
                                                    {
                                                        if (panel.unitOn.GetComponent<TutoEnemy>())
                                                        {
                                                            TutoBattleManager.Instance.attackUnit(player.stats, panel.unitOn.GetComponent<TutoEnemy>().stats, true);
                                                            TutoCardManager.Instance.UseCard();
                                                            Debug.Log("hit enemy");
                                                        }
                                                        else
                                                        {
                                                            TutoBattleManager.Instance.attackUnit(player.stats, player.stats, true);
                                                            Debug.Log("hit myself");
                                                        }

                                                    }
                                                }

                                                TutoCardManager.Instance.UseCard();
                                                TutoCardManager.Instance.midToHand = false;

                                                TutoCharacterManager.Instance.currentPlayer.stats.actionPoint -= TutoBattleManager.Instance.currentAttackParam.APNeeded;
                                                TutoCharacterManager.Instance.StartCoroutine("checkAlive");

                                                TutoGrid.Instance.resetClicked();
                                                player.GetComponent<TutoPlayerMovement>().state = TutoPlayerMovement.States.IDLE;
                                                TutoElementInteract.Instance.changeElement(player.stats.element, TutoBattleManager.Instance.currentAttackParam.element);
                                                TutoUI.Instance.showButton();
                                                TutoUI.Instance.HidePortrait();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (player.state == TutoPlayerMovement.States.IDLE)
                                    {
                                        if (touchedPanel.unitOn != null && touchedPanel.unitOn.GetComponent<TutoPlayerMovement>() && canClickPlayer)
                                        {
                                            TutoGrid.Instance.resetClicked();
                                            TutoBlueRedGrid.Instance.movementsPossible(player.xPos, player.yPos, false);
                                            TutoBlueRedGrid.Instance.blueRedPath(player.stats.actionPoint);

                                            TutoUI.Instance.hideButton();

                                            player.state = TutoPlayerMovement.States.SELECTED;

                                            TutoUI.Instance.EnemyToHero(player.stats);
                                            TutoUI.Instance.ShowPortrait(player.stats);
                                        }
                                        else if (touchedPanel.unitOn != null && touchedPanel.unitOn.GetComponent<TutoEnemy>() && canClickEnemy)
                                        {
                                            //UiActionManager.Instance.ShowPortrait(touchedPanel.unitOn.GetComponent<Enemy>().stats);
                                            Tuto.Instance.Pop5();
                                            foreach (var item in TutoGrid.Instance.gridArrayAlpha)
                                            {
                                                TutoGrid.Instance.gridArrayAlpha[item.x, item.y].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TutoGrid.Instance.listSpritesAlpha[0];
                                                TutoGrid.Instance.gridArrayAlpha[item.x, item.y].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                                            }

                                            TutoUI.Instance.HeroToEnemy(touchedPanel.unitOn.GetComponent<TutoEnemy>().stats);
                                            TutoUI.Instance.ShowPortrait(touchedPanel.unitOn.GetComponent<TutoEnemy>().stats);
                                            var alphaPanel = TutoGrid.Instance.gridArrayAlpha[touchedPanel.x, touchedPanel.y].transform.GetChild(0).GetComponent<SpriteRenderer>();
                                            alphaPanel.color = new Color(1, 1, 1, 0.5f);
                                            alphaPanel.sprite = TutoGrid.Instance.listSpritesAlpha[1];
                                        }
                                    }
                                }

                            }
                            else if (touchedCollier.gameObject.tag == "ClickableChar")
                            {

                                if (touchedCollier.gameObject.GetComponentInParent<TutoPlayerMovement>() && canClickPlayer)
                                {
                                    if (player.state == TutoPlayerMovement.States.IDLE && player.stats.actionPoint > 0)
                                    {
                                        TutoGrid.Instance.resetClicked();
                                        TutoBlueRedGrid.Instance.movementsPossible(player.xPos, player.yPos, false);
                                        TutoBlueRedGrid.Instance.blueRedPath(player.stats.actionPoint);

                                        TutoUI.Instance.hideButton();

                                        player.state = TutoPlayerMovement.States.SELECTED;

                                        TutoUI.Instance.EnemyToHero(player.stats);
                                        TutoUI.Instance.ShowPortrait(player.stats);

                                    }
                                    else if (player.state == TutoPlayerMovement.States.SELECTED)
                                    {
                                        TutoGrid.Instance.resetClicked();
                                        player.state = TutoPlayerMovement.States.IDLE;
                                        TutoUI.Instance.showButton();
                                    }

                                }

                                else if (touchedCollier.gameObject.GetComponentInParent<TutoEnemy>() && canClickEnemy)
                                {
                                    var charact = touchedCollier.gameObject.GetComponentInParent<TutoEnemy>();
                                    var actualPanel = TutoGrid.Instance.gridArray[charact.xPos, -charact.yPos];

                                    if (player.state == TutoPlayerMovement.States.ACTION && actualPanel.canBeClick)
                                    {

                                        if (actualPanel == currentPanel)
                                        {
                                            if (TutoBattleManager.Instance.currentAttackParam.AOE && !TutoBattleManager.Instance.currentAttackParam.around)
                                            {
                                                player.state = TutoPlayerMovement.States.AOESELECT;
                                                TutoUI.Instance.showAttackRange(TutoBattleManager.Instance.currentAttackParam.aoeEffect, currentPanel.x, -currentPanel.y);
                                                //CardManager.Instance.UseCard();
                                                TutoCardManager.Instance.midToHand = false;
                                            }

                                            else
                                            {
                                                TutoBattleManager.Instance.attackUnit(player.stats, charact.stats, false);
                                                TutoCharacterManager.Instance.currentPlayer.stats.actionPoint -= TutoBattleManager.Instance.currentAttackParam.APNeeded;
                                                currentPanel = null;
                                                TutoCardManager.Instance.UseCard();
                                                TutoCardManager.Instance.midToHand = false;
                                            }
                                        }
                                        else
                                        {
                                            currentPanel = actualPanel;
                                        }
                                    }

                                    else if (player.state == TutoPlayerMovement.States.IDLE)
                                    {
                                        TutoUI.Instance.ShowPortrait(charact.stats);

                                        foreach (var item in TutoGrid.Instance.gridArrayAlpha)
                                        {
                                            TutoGrid.Instance.gridArrayAlpha[item.x, item.y].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TutoGrid.Instance.listSpritesAlpha[0];
                                            TutoGrid.Instance.gridArrayAlpha[item.x, item.y].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                                        }

                                        TutoUI.Instance.HeroToEnemy(actualPanel.unitOn.GetComponent<TutoEnemy>().stats);
                                        TutoUI.Instance.ShowPortrait(actualPanel.unitOn.GetComponent<TutoEnemy>().stats);
                                        var alphaPanel = TutoGrid.Instance.gridArrayAlpha[actualPanel.x, actualPanel.y].transform.GetChild(0).GetComponent<SpriteRenderer>();
                                        alphaPanel.color = new Color(1, 1, 1, 0.5f);
                                        alphaPanel.sprite = TutoGrid.Instance.listSpritesAlpha[1];

                                    }

                                    else if (player.state == TutoPlayerMovement.States.AOESELECT)
                                    {
                                        if (actualPanel == currentPanel)
                                        {
                                            foreach (var panel in TutoGrid.Instance.gridArray)
                                            {
                                                if (panel.canBeClick && panel.unitOn != null)
                                                {
                                                    if (panel.unitOn.GetComponent<TutoEnemy>())
                                                    {
                                                        TutoBattleManager.Instance.attackUnit(player.stats, panel.unitOn.GetComponent<TutoEnemy>().stats, true);
                                                        TutoCardManager.Instance.UseCard();
                                                    }
                                                    else
                                                    {
                                                        TutoBattleManager.Instance.attackUnit(player.stats, player.stats, true);
                                                    }
                                                }
                                            }

                                            TutoCardManager.Instance.UseCard();
                                            TutoCardManager.Instance.midToHand = false;

                                            TutoCharacterManager.Instance.currentPlayer.stats.actionPoint -= TutoBattleManager.Instance.currentAttackParam.APNeeded;
                                            TutoCharacterManager.Instance.StartCoroutine("checkAlive");

                                            TutoGrid.Instance.resetClicked();
                                            player.GetComponent<TutoPlayerMovement>().state = TutoPlayerMovement.States.IDLE;
                                            TutoElementInteract.Instance.changeElement(player.stats.element, TutoBattleManager.Instance.currentAttackParam.element);
                                            TutoUI.Instance.showButton();
                                            TutoUI.Instance.HidePortrait();
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
                                Tuto.Instance.Pop3();
                                TutoGrid.Instance.createPlayer(touchedCollier.gameObject.GetComponent<Panel>());
                                TutoUI.Instance.showButton();
                                TutoCharacterManager.Instance.functionStart();
                            }
                        }

                    }
                }
            }

        }


        if (currentPanel != null)
        {
            shinningPannel();
        }
    }

    public void shinningPannel()
    {
        var panelColor = TutoGrid.Instance.gridArrayAlpha[currentPanel.x, currentPanel.y].transform.GetChild(0).GetComponent<SpriteRenderer>();
        panelColor.color = Color.Lerp(new Color(1, 1, 1, 0.5f), new Color(1, 1, 1, 1f), Mathf.PingPong(Time.time * 5, 0.5f));
    }


    public void cancelButton()
    {
        var player = TutoCharacterManager.Instance.currentPlayer.GetComponent<TutoPlayerMovement>();

        if (player.state == TutoPlayerMovement.States.SELECTED)
        {
            var cardM = TutoCardManager.Instance;
            if (!cardM.handToMid && !cardM.midToHand)
            {
                TutoGrid.Instance.resetClicked();
                player.state = TutoPlayerMovement.States.IDLE;
                TutoUI.Instance.showButton();
            }
        }

        else if (player.state == TutoPlayerMovement.States.SELECTCARD)
        {
            var cardM = TutoCardManager.Instance;
            if (!cardM.handToMid && !cardM.midToHand)
            {
                TutoGrid.Instance.resetClicked();
                player.state = TutoPlayerMovement.States.IDLE;
                TutoUI.Instance.hideAll();
                TutoUI.Instance.showButton();
                TutoCardManager.Instance.handPanel.GetComponent<Animator>().SetTrigger("Hide");
                TutoCardManager.Instance.MidToHandLaFonction();
            }
        }

        else if (player.state == TutoPlayerMovement.States.ACTION)
        {
            var cardM = TutoCardManager.Instance;
            if (!cardM.handToMid && !cardM.midToHand)
            {
                TutoGrid.Instance.resetClicked();
                player.state = TutoPlayerMovement.States.SELECTCARD;
                TutoUI.Instance.showDeckForced();
            }
        }
        
        else if (player.state == TutoPlayerMovement.States.AOESELECT)
        {
            var cardM = TutoCardManager.Instance;
            if (!cardM.handToMid && !cardM.midToHand)
            {
                TutoGrid.Instance.resetClicked();
                player.state = TutoPlayerMovement.States.SELECTCARD;
                TutoUI.Instance.showDeck();
            }
        }
    }
}
