using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutoUI : MonoBehaviour
{
    [Header("Buttons")]
    public GameObject buttonHand;
    public GameObject buttonCancel;
    public GameObject use;

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

    [Header("UI")]
    public TextMeshProUGUI turnText;

    public Image[] inGame = new Image[4];
    public List<Sprite> heroSprites = new List<Sprite>();
    public List<Sprite> enemySprites = new List<Sprite>();

    public int speed;

    public TutoShowRangeAttack sra;

    private static TutoUI _instance = null;

    public static TutoUI Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;

        Time.timeScale = speed;
    }

    public void showButton()
    {
        buttonHand.SetActive(true);
        buttonCancel.SetActive(false);
        unitPortrait.SetActive(true);
        use.SetActive(false);
        apleft.text = TutoCharacterManager.Instance.currentPlayer.stats.actionPoint.ToString();
        maxHP.text = TutoCharacterManager.Instance.currentPlayer.stats.maxHP.ToString();
        currenntHP.text = TutoCharacterManager.Instance.currentPlayer.stats.HP.ToString();
        defText.text = TutoCharacterManager.Instance.currentPlayer.stats.defense.ToString();
        attText.text = TutoCharacterManager.Instance.currentPlayer.stats.strenght.ToString();
    }

    public void hideButton()
    {
        buttonHand.SetActive(false);
        buttonCancel.SetActive(true);
        use.SetActive(false);
    }

    public void showDeck()
    {
        var cardM = TutoCardManager.Instance;
        if (!cardM.handToMid && !cardM.midToHand)
        {
            buttonHand.SetActive(false);
            buttonCancel.SetActive(true);
            use.SetActive(true);
            apleft.text = TutoCharacterManager.Instance.currentPlayer.stats.actionPoint.ToString();
            currenntHP.text = TutoCharacterManager.Instance.currentPlayer.stats.HP.ToString();

            EnemyToHero(TutoCharacterManager.Instance.currentPlayer.stats);
            ShowPortrait(TutoCharacterManager.Instance.currentPlayer.stats);

            var player = TutoCharacterManager.Instance.currentPlayer.GetComponent<TutoPlayerMovement>();

            player.state = TutoPlayerMovement.States.SELECTCARD;

            cardM.handPanel.GetComponent<Animator>().SetTrigger("Show");
        }
    }
    public void showDeckForced()
    {
        var cardM = TutoCardManager.Instance;
        buttonHand.SetActive(false);
        buttonCancel.SetActive(true);
        use.SetActive(true);
        //unitPortrait.SetActive(false);
        //apleft.SetActive(true);
        apleft.text = TutoCharacterManager.Instance.currentPlayer.stats.actionPoint.ToString();

        var player = TutoCharacterManager.Instance.currentPlayer.GetComponent<TutoPlayerMovement>();

        player.state = TutoPlayerMovement.States.SELECTCARD;

        cardM.handPanel.GetComponent<Animator>().SetTrigger("Show");
    }

    public void useCard()
    {
        var cardM = TutoCardManager.Instance;
        if (cardM.middleCard != null)
        {
            if (cardM.middleCard.GetComponent<TutoCardDisplay>().attackParam.APNeeded <= TutoCharacterManager.Instance.currentPlayer.GetComponent<TutoStats>().actionPoint)
            {
                if (!cardM.handToMid && !cardM.midToHand) 
                {
                    if (cardM.middleCard != null)
                    {
                        var midCard = cardM.middleCard.GetComponent<TutoCardDisplay>().attackParam;

                        cardM.MidToHandLaFonction();
                        showAttackRange(midCard);
                        cardM.handPanel.GetComponent<Animator>().SetTrigger("Hide");
                        cardM.chosenCard = cardM.middleCard;

                        if (TutoBattleManager.Instance.currentAttackParam.around)
                        {
                            TutoCharacterManager.Instance.currentPlayer.state = TutoPlayerMovement.States.AOESELECT;
                        }
                    }
                }
            }
        }
        apleft.text = TutoCharacterManager.Instance.currentPlayer.stats.actionPoint.ToString();
    }

    public void hideAll()
    {
        buttonHand.SetActive(false);
        buttonCancel.SetActive(false);
        use.SetActive(false);
        unitPortrait.SetActive(false);
    }

    public void endTurn()
    {
        var cardM = TutoCardManager.Instance;
        if (!cardM.handToMid && !cardM.midToHand)
        {
            TutoCharacterManager.Instance.currentPlayer.endTurn();
        }
    }

    public void ShowPortrait(TutoStats stats)
    {
        unitPortrait.SetActive(true);
        HPBar.maxValue = stats.maxHP;
        HPBar.value = stats.HP;
    }

    public void HidePortrait()
    {
        //unitPortrait.SetActive(false);
    }

    public void cancelButton()
    {
        TutoClickManager.Instance.cancelButton();
    }


    public void showAttackRange(TutoAttackParam param)
    {
        var playerStats = TutoCharacterManager.Instance.currentPlayer.GetComponent<TutoStats>();
        TutoBattleManager.Instance.currentAttackParam = param;

        if (playerStats.actionPoint >= param.APNeeded)
        {
            var player = TutoCharacterManager.Instance.currentPlayer.GetComponent<TutoPlayerMovement>();
            player.state = TutoPlayerMovement.States.ACTION;

            hideButton();
            sra.testAttackRange(param);
        }
        else
        {
            Debug.Log("You need more AP");
        }
    }

    public void showAttackRange(TutoAttackParam param, int x, int y)
    {
        var playerStats = TutoCharacterManager.Instance.currentPlayer.GetComponent<TutoStats>();
        TutoBattleManager.Instance.currentAttackParam = param;

        hideButton();
        sra.testAttackRange(param, x, y);

    }

    public void EnemyToHero(TutoStats playerStats)
    {
        for (int i = 0; i < inGame.Length; i++)
        {
            inGame[i].sprite = heroSprites[i];
        }

        apleft.text = playerStats.actionPoint.ToString();
        maxHP.text = playerStats.maxHP.ToString();
        currenntHP.text = playerStats.HP.ToString();
        defText.text = (playerStats.defense + playerStats.boostDef).ToString();
        attText.text = (playerStats.strenght + playerStats.boostAtt).ToString();


        switch (playerStats.element)
        {
            case TutoStats.ELEMENT.NORMAL:
                elementImage.sprite = elementInfos[3];
                break;
            case TutoStats.ELEMENT.RED:
                elementImage.sprite = elementInfos[0];
                break;
            case TutoStats.ELEMENT.BLUE:
                elementImage.sprite = elementInfos[1];
                break;
            case TutoStats.ELEMENT.GREEN:
                elementImage.sprite = elementInfos[2];
                break;
            default:
                break;
        }
    }

    public void HeroToEnemy(TutoStats enemyStats)
    {
        for (int i = 0; i < inGame.Length; i++)
        {
            inGame[i].sprite = enemySprites[i];
        }

        apleft.text = enemyStats.actionPoint.ToString();
        maxHP.text = enemyStats.maxHP.ToString();
        currenntHP.text = enemyStats.HP.ToString();
        defText.text = (enemyStats.defense + enemyStats.boostDef).ToString();
        attText.text = (enemyStats.strenght + enemyStats.boostAtt).ToString();

        switch (enemyStats.element)
        {
            case TutoStats.ELEMENT.NORMAL:
                elementImage.sprite = elementInfos[3];
                break;
            case TutoStats.ELEMENT.RED:
                elementImage.sprite = elementInfos[0];
                break;
            case TutoStats.ELEMENT.BLUE:
                elementImage.sprite = elementInfos[1];
                break;
            case TutoStats.ELEMENT.GREEN:
                elementImage.sprite = elementInfos[2];
                break;
            default:
                break;
        }
    }

    public IEnumerator startButWait()
    {
        TutoCardManager.Instance.inChosenTime = false;
        TutoMapComposent.Instance.fadeOutIn();
        yield return new WaitForSeconds(0.6f);
        TutoCardManager.Instance.transitionStyle.SetActive(false);
        TutoGrid.Instance.functionStart();
    }

}
