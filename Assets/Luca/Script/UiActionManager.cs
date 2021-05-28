using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiActionManager : MonoBehaviour
{
    [Header("Buttons")]
    public GameObject buttonHand;
    public GameObject buttonCancel;
    public GameObject deck;
    public GameObject use;

    [Header("Random Texts")]
    public Image imageBG;
    public Text unitName;
    public Text unitHP;
    public Text unitSTR;
    public Text unitDEF;
    public Text unitAP;
    public Text element;

    [Header("Hero Infos")]
    public Slider HPBar;
    public TextMeshProUGUI apleft;
    public TextMeshProUGUI currenntHP;
    public TextMeshProUGUI maxHP;
    public GameObject unitPortrait;
    public TextMeshProUGUI defText;
    public TextMeshProUGUI attText;
    public Image elementImage;
    public Sprite[] elementInfos = new Sprite[4];

    [Header("Enemies Board")]
    public GameObject enemiesBoard;
    public GameObject button;
    public Sprite minusButtonImage;
    public Sprite plusButtonImage;

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
        unitPortrait.SetActive(true);
        use.SetActive(false);
        apleft.text = CharacterManager.Instance.currentPlayer.stats.actionPoint.ToString();
        maxHP.text = CharacterManager.Instance.currentPlayer.stats.maxHP.ToString();
        currenntHP.text = CharacterManager.Instance.currentPlayer.stats.HP.ToString();
        defText.text = CharacterManager.Instance.currentPlayer.stats.defense.ToString();
        attText.text = CharacterManager.Instance.currentPlayer.stats.strenght.ToString();
    }

    public void hideButton()
    {
        buttonHand.SetActive(false);
        buttonCancel.SetActive(true);
        deck.SetActive(false);
        use.SetActive(false);
    }

    public void showDeck()
    {
        var cardM = CardManager.Instance;
        if (!cardM.handToMid && !cardM.midToHand)
        {
            buttonHand.SetActive(false);
            buttonCancel.SetActive(true);
            use.SetActive(true);
            apleft.text = CharacterManager.Instance.currentPlayer.stats.actionPoint.ToString();
            currenntHP.text = CharacterManager.Instance.currentPlayer.stats.HP.ToString();

            var player = CharacterManager.Instance.currentPlayer.GetComponent<PlayerMovement>();

            player.state = PlayerMovement.States.SELECTCARD;

            cardM.handPanel.GetComponent<Animator>().SetTrigger("Show");
        }
    }
    public void showDeckForced()
    {
        var cardM = CardManager.Instance;
        buttonHand.SetActive(false);
        buttonCancel.SetActive(true);
        use.SetActive(true);
        //unitPortrait.SetActive(false);
        //apleft.SetActive(true);
        apleft.text = CharacterManager.Instance.currentPlayer.stats.actionPoint.ToString();

        var player = CharacterManager.Instance.currentPlayer.GetComponent<PlayerMovement>();

        player.state = PlayerMovement.States.SELECTCARD;

        cardM.handPanel.GetComponent<Animator>().SetTrigger("Show");
    }

    public void useCard()
    {
        var cardM = CardManager.Instance;
        if (cardM.middleCard != null)
        {
            if (cardM.middleCard.GetComponent<CardDisplay>().attackParam.APNeeded <= CharacterManager.Instance.currentPlayer.GetComponent<Stats>().actionPoint)
            {
                if (!cardM.handToMid && !cardM.midToHand)
                {
                    if (cardM.middleCard != null)
                    {
                        var midCard = cardM.middleCard.GetComponent<CardDisplay>().attackParam;
                        var player = CharacterManager.Instance.currentPlayer;

                        cardM.MidToHandLaFonction();
                        showAttackRange(midCard);
                        cardM.handPanel.GetComponent<Animator>().SetTrigger("Hide");
                        cardM.chosenCard = cardM.middleCard;

                        if (BattleManager.Instance.currentAttackParam.around)
                        {
                            CharacterManager.Instance.currentPlayer.state = PlayerMovement.States.AOESELECT;
                        }

                    }
                }
            }
        }
    }

    public void hideAll()
    {
        buttonHand.SetActive(false);
        buttonCancel.SetActive(false);
        deck.SetActive(false);
        use.SetActive(false);
        unitPortrait.SetActive(false);
    }

    public void endTurn()
    {
        var cardM = CardManager.Instance;
        if (!cardM.handToMid && !cardM.midToHand)
        {
            CharacterManager.Instance.currentPlayer.endTurn();
        }
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
            unitSTR.text = "STR : " + stats.strenght.ToString() + " + " + stats.gameObject.GetComponent<Enemy>().attackMonster.attackParam.damage + "   " + "Range : " + stats.gameObject.GetComponent<Enemy>().attackMonster.attackParam.range;
            imageBG.color = new Color(1, 0.4481132f, 0.4481132f, 0.5f);
        }
        else
        {
            unitSTR.text = "STR : " + (stats.strenght + stats.boostAtt).ToString();
            imageBG.color = new Color(0, 0.5876393f, 1, 0.5f);
        }

        unitDEF.text = "DEF : " + (stats.defense + stats.boostDef).ToString();
        unitAP.text = "AP : " + stats.actionPoint.ToString();

        if (stats.gameObject.GetComponent<PlayerMovement>())
        {
            if (stats.element == Stats.ELEMENT.RED)
            {
                element.text = "Elem : " + stats.element.ToString() + "  STR +" + stats.boostAtt;
            }
            else if (stats.element == Stats.ELEMENT.BLUE)
            {
                element.text = "Elem : " + stats.element.ToString() + "  DEF +" + stats.boostDef;
            }
            else if (stats.element == Stats.ELEMENT.GREEN)
            {
                element.text = "Elem : " + stats.element.ToString() + "  AP +" + stats.boostAP;
            }
            else
            {
                element.text = "Elem : " + stats.element.ToString();
            }
        }
        else
        {
            element.text = "Elem : " + stats.element.ToString();
        }
    }

    public void HidePortrait()
    {
        //unitPortrait.SetActive(false);
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

    public void EnemiesBoardState()
    {
        if (enemiesBoard.activeInHierarchy)
        {
            enemiesBoard.SetActive(false);
            button.GetComponent<Image>().sprite = plusButtonImage;
        }
        else
        {
            enemiesBoard.SetActive(true);
            button.GetComponent<Image>().sprite = minusButtonImage;
            EnemiesBoard.Instance.CheckList();
        }
    }

}
