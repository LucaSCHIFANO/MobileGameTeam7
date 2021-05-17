using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiActionManager : MonoBehaviour
{
    public GameObject buttonHand;
    public GameObject buttonCancel;
    public GameObject deck;

    public Text moveLeft;

    public GameObject unitPortrait;
    public Text unitName;
    public Text unitHP;
    public Text unitSTR;
    public Text unitDEF;
    public Text unitAP;
    public Slider HPBar;

    public ShowRangeAttack sra;


    private static UiActionManager _instance = null;

    public static UiActionManager Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }



    public void showButton()
    {
        buttonHand.SetActive(true);
        buttonCancel.SetActive(false);
        unitPortrait.SetActive(false);
    }

    public void hideButton()
    {
        buttonHand.SetActive(false);
        buttonCancel.SetActive(true);
        deck.SetActive(false);
    }

    public void showDeck()
    {
        buttonHand.SetActive(false);
        buttonCancel.SetActive(true);
        deck.SetActive(true);
        unitPortrait.SetActive(false);

        var player = CharacterManager.Instance.currentPlayer.GetComponent<PlayerMovement>();

        player.state = PlayerMovement.States.SELECTCARD;
    }

    public void hideAll()
    {
        buttonHand.SetActive(false);
        buttonCancel.SetActive(false);
        deck.SetActive(false);
        unitPortrait.SetActive(false);
    }

    public void endTurn()
    {
        CharacterManager.Instance.currentPlayer.endTurn();
    }

    public void setMovePoint()
    {
        moveLeft.text = "AP : " + CharacterManager.Instance.currentPlayer.stats.actionPoint.ToString();
    }

    public void ShowPortrait(Stats stats)
    {
        unitPortrait.SetActive(true);
        unitName.text = stats.characName;
        unitHP.text = stats.HP + " / " + stats.maxHP;
        HPBar.maxValue = stats.maxHP;
        HPBar.value = stats.HP;
        unitSTR.text = "STR : " + stats.strenght.ToString();
        unitDEF.text = "DEF : " + stats.defense.ToString();
        unitAP.text = "AP : " + stats.actionPoint.ToString();
    }

    public void HidePortrait()
    {
        unitPortrait.SetActive(false);
    }

    public void cancelButton()
    {
        ClicklManager.Instance.cancelButton();
    }


    public void showAttackRange(AttackParam param)
    {
        var playerStats = CharacterManager.Instance.currentPlayer.GetComponent<Stats>();
        BattleManager.Instance.currentAttackParam = param;

        if (playerStats.actionPoint >= param.APNeeded)
        {
            var player = CharacterManager.Instance.currentPlayer.GetComponent<PlayerMovement>();
            player.state = PlayerMovement.States.ACTION;

            hideButton();
            sra.testAttackRange(param);
        }
        else
        {
            Debug.Log("You need more AP");
        }
    }

}
