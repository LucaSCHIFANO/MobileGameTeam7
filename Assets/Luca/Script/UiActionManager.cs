using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UiActionManager : MonoBehaviour
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

    private void Update()
    {

        Time.timeScale = speed;
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
        use.SetActive(false);
    }

    public void showDeck()
    {
        var cardM = CardManager.Instance;
        if (!cardM.handToMid && !cardM.midToHand && !cardM.risingUp)
        {
            buttonHand.SetActive(false);
            buttonCancel.SetActive(true);
            use.SetActive(true);
            apleft.text = CharacterManager.Instance.currentPlayer.stats.actionPoint.ToString();
            currenntHP.text = CharacterManager.Instance.currentPlayer.stats.HP.ToString();

            EnemyToHero(CharacterManager.Instance.currentPlayer.stats);
            ShowPortrait(CharacterManager.Instance.currentPlayer.stats);

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
                if (!cardM.handToMid && !cardM.midToHand && !cardM.risingUp)
                {
                    var midCard = cardM.middleCard.GetComponent<CardDisplay>().attackParam;

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
        apleft.text = CharacterManager.Instance.currentPlayer.stats.actionPoint.ToString();
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
        var cardM = CardManager.Instance;
        if (!cardM.handToMid && !cardM.midToHand && !cardM.risingUp)
        {
            CharacterManager.Instance.currentPlayer.endTurn();
        }
    }

    public void ShowPortrait(Stats stats)
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

    public void EnemyToHero(Stats playerStats)
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
            case Stats.ELEMENT.NORMAL:
                elementImage.sprite =elementInfos[3];
                break;
            case Stats.ELEMENT.RED:
                elementImage.sprite = elementInfos[0];
                break;
            case Stats.ELEMENT.BLUE:
               elementImage.sprite = elementInfos[1];
                break;
            case Stats.ELEMENT.GREEN:
                elementImage.sprite = elementInfos[2];
                break;
            default:
                break;
        }
    }

    public void HeroToEnemy(Stats enemyStats)
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
            case Stats.ELEMENT.NORMAL:
                elementImage.sprite = elementInfos[3];
                break;
            case Stats.ELEMENT.RED:
                elementImage.sprite = elementInfos[0];
                break;
            case Stats.ELEMENT.BLUE:
                elementImage.sprite = elementInfos[1];
                break;
            case Stats.ELEMENT.GREEN:
                elementImage.sprite = elementInfos[2];
                break;
            default:
                break;
        }
    }

    public IEnumerator startButWait()
    {
        CardManager.Instance.inChosenTime = false;
        MapComposent.Instance.fadeOutIn();
        yield return new WaitForSeconds(0.6f);
        CardManager.Instance.transitionStyle.SetActive(false);
        Grid.Instance.functionStart();
    }


    public IEnumerator backToMenu()
    {
        MapComposent.Instance.fadeOutIn();
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator goToDeath()
    {
        MapComposent.Instance.fadeOutIn();
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene("GameOver");
    }
}
