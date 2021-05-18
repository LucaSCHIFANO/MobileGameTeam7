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
    public Image imageBG;
    public Text unitName;
    public Text unitHP;
    public Text unitSTR;
    public Text unitDEF;
    public Text unitAP;
    public Slider HPBar;
    public Text element;

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
        unitHP.text = stats.HP + " / " + stats.maxHP + " PV";
        HPBar.maxValue = stats.maxHP;
        HPBar.value = stats.HP;

        if (stats.gameObject.GetComponent<Enemy>())
        {
            unitSTR.text = "STR : " + stats.strenght.ToString() + " + " + stats.gameObject.GetComponent<Enemy>().attackMonster.attackParam.damage;
            imageBG.color = new Color(1, 0.4481132f, 0.4481132f, 0.5f);
        }
        else
        {
            unitSTR.text = "STR : " + stats.strenght.ToString();
            imageBG.color = new Color(0, 0.5876393f, 1, 0.5f);
        }

        unitDEF.text = "DEF : " + stats.defense.ToString();
        unitAP.text = "AP : " + stats.actionPoint.ToString();
        element.text = "Elem : " + stats.element.ToString();
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

    public void showAttackRange(AttackParam param, int x, int y)
    {
        var playerStats = CharacterManager.Instance.currentPlayer.GetComponent<Stats>();
        BattleManager.Instance.currentAttackParam = param;

        hideButton();
        sra.testAttackRange(param, x, y);

    }

}
