using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
    [Header("KDO LES GD")]
    public int nbTirageDebut = 5;
    public int howManyInHand = 5;
    public GameObject cardPrefab;
    public List<Card> cardList = new List<Card>();
    public List<AttackParam> attackParams = new List<AttackParam>();
    public GameObject cardInfos;

    [Header("Random")]
    public Canvas canvas;
    public GameObject pauseButton;
    public Transform bin;
    private List<Card> discard = new List<Card>();

    [Header("RollCard")]
    public bool inChosenTime = false;
    public TextMeshProUGUI chooseCardText;
    public GameObject deckSelection;
    public Transform deckObject;
    private List<Card> deck = new List<Card>();
    private List<GameObject> actualRoll = new List<GameObject>();
    private int index = 0;
    public Transform[] deckPosition;
    private int deckPositionIndex = 0;
    public bool rollCardIsChose = false;

    [Header("In Round")]
    public bool handToMid = false;
    public bool midToHand = false;
    public bool inRound = false;
    private bool isMid = false;
    private List<GameObject> hand = new List<GameObject>();
    public GameObject middleCard = null;
    public GameObject chosenCard = null;
    public GameObject rollCard = null;
 
    [Header("Is RiseUp")]
    public bool isRiseUp = false;
    public bool risingUp = false;
    private Vector3 middlePos;

    [Header("HearthStone Style")]
    public Transform startLocation;
    public float gap;
    public Transform handPanel;
    public Transform cardPosition;
    private Quaternion midRotation;
    private Quaternion previousRotation;
    private Vector3 previousTransform;
    private Vector3 previousScale;

    public GameObject transitionStyle;

    private static CardManager _instance = null;

    public static CardManager Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }


    void Start()
    {
        //gap = 1.5f;
        midRotation = Quaternion.Euler(0f, 180f, 0f);
    }

    void Update()
    {
        if (!handToMid && !midToHand && !risingUp)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!MenuPause.GameIsPaused)
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                    RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

                    if (hit.Length == 0)
                    {
                        return;
                    }

                    GameObject firstCard = hit[0].collider?.gameObject;
                    GameObject hitCard = null;
                    if (hit.Length == 1)
                    {
                        if (hit[0].collider.gameObject.CompareTag("Card"))
                        {
                            hitCard = firstCard;
                        }
                    }
                    else
                    {
                        foreach (RaycastHit2D h in hit)
                        {
                            if (h.collider.gameObject.CompareTag("Card") && h.collider.gameObject.GetComponent<RectTransform>())
                            {
                                if (firstCard != h.collider.gameObject)
                                {
                                    if (firstCard.GetComponent<RectTransform>())
                                    {
                                        if (h.collider.gameObject.GetComponent<RectTransform>().localPosition.z > firstCard.GetComponent<RectTransform>().localPosition.z)
                                        {
                                            hitCard = h.collider.gameObject;
                                        }

                                    }
                                    else
                                    {
                                        hitCard = h.collider.gameObject;
                                    }
                                }
                            }
                        }
                    }

                    if (hitCard != null)
                    {
                        if (hitCard.CompareTag("Card"))
                        {
                            if (inChosenTime)
                            {
                                AudioManager.Instance.Play("Card");
                                var hitDisplay = hitCard.GetComponent<CardDisplay>();

                                deck.Add(hitDisplay.card);
                                hitDisplay.card.attackParam = hitDisplay.attackParam;
                                rollCard = hitCard;
                                rollCard.transform.SetParent(deckObject);
                                rollCardIsChose = true;
                                rollCard.gameObject.tag = "TaMereLaPute";

                                if (index < nbTirageDebut - 1)
                                {
                                    
                                }
                                else
                                {
                                    chooseCardText.gameObject.SetActive(false);
                                    //MapComposent.Instance.Opening();
                                    //MapComposent.Instance.Check();
                                    var cM = CharacterManager.Instance;

                                    for (int i = 0; i < actualRoll.Count; i++)
                                    {
                                        Destroy(actualRoll[i]);
                                    }
                                    actualRoll.Clear();

                                    for (int i = 0; i < deckObject.childCount; i++)
                                    {
                                        Destroy(deckObject.GetChild(i).gameObject);
                                    }
                                    deckSelection.SetActive(false);
                                    rollCardIsChose = false;

                                    if (cM.currentPlayer == null || cM.currentPlayer.state != PlayerMovement.States.WIN)
                                    {
                                        UiActionManager.Instance.StartCoroutine("startButWait");
                                    }
                                    else
                                    {
                                        CharacterManager.Instance.returnToMap();
                                    }
                                }
                            }

                            if (inRound)
                            {
                                if (!isRiseUp)
                                {
                                    middleCard = hitCard;
                                    risingUp = true;
                                    var hitTrans = hitCard.GetComponent<RectTransform>();
                                    middlePos = hitTrans.localPosition + new Vector3(0f, 225f, 0f);
                                    previousTransform = hitTrans.localPosition;
                                    previousRotation = hitTrans.rotation;
                                    previousScale = hitCard.transform.localScale;
                                    isRiseUp = true;
                                }
                                else if (isRiseUp && middleCard != hitCard)
                                {
                                    midToHand = true;
                                }
                                else if (isRiseUp && middleCard == hitCard)
                                {
                                    if (!isMid)
                                    {
                                        AudioManager.Instance.Play("Card");
                                        handToMid = true;
                                        isMid = true;
                                    }
                                    else if (isMid && middleCard == hitCard)
                                    {
                                        midToHand = true;
                                        AudioManager.Instance.Play("Card");
                                    }
                                }
                                  
                            }
                        }
                    }
                }
            }
        }

        if (rollCardIsChose)
        {
            if (!rollCard)
            {
                return;
            }

            for (int i = 0; i < actualRoll.Count; i++)
            {
                if (actualRoll[i] == rollCard)
                {
                   
                }
                else
                {
                    actualRoll[i].transform.position = Vector3.Lerp(actualRoll[i].transform.position, bin.position, 5f * Time.deltaTime);
                    actualRoll[i].transform.localScale = Vector3.Lerp(actualRoll[i].transform.localScale, new Vector3(.01f, .01f, .01f), 5f * Time.deltaTime);
                }

                if (Vector2.Distance(actualRoll[i].transform.position, bin.position) < .1f)
                {
                    Destroy(actualRoll[i]);
                    actualRoll.RemoveAt(i);
                }
            }

            rollCard.transform.position = Vector3.Lerp(rollCard.transform.position, deckPosition[deckPositionIndex].position, 5f * Time.deltaTime);
            rollCard.transform.localScale = Vector3.Lerp(rollCard.transform.localScale, new Vector3(.4f, .4f, .4f), 5f * Time.deltaTime);

            if (Vector2.Distance(rollCard.transform.position, deckPosition[deckPositionIndex].position) < .01f)
            {
                rollCard.transform.position = deckPosition[deckPositionIndex].position;
                rollCard.transform.localScale = new Vector3(.4f, .4f, .4f);
                deckPositionIndex++;
                actualRoll.Clear();
                rollCardIsChose = false;
                rollCard = null;
                if (index < nbTirageDebut - 1)
                {
                    RollCard();
                    index++;
                }
            } 
        }

        if (risingUp)
        {
            var mdlTrans = middleCard.GetComponent<RectTransform>();

            if (middleCard == null)
            {
                return;
            }

            mdlTrans.localPosition = Vector3.Lerp(mdlTrans.localPosition, middlePos, 25f * Time.deltaTime);
            if (Vector2.Distance(mdlTrans.localPosition, middlePos) < .1f)
            {
                mdlTrans.localPosition = middlePos;
                risingUp = false;
            }
        }

        if (handToMid)
        {
            var mdlTrans = middleCard.GetComponent<RectTransform>();
            var mdlDisplay = middleCard.GetComponent<CardDisplay>();
            var mdlInfos = middleCard.transform.GetChild(6).gameObject.GetComponent<CardInfos>();

            if (middleCard == null)
            {
                return;
            }
            mdlTrans.localPosition = Vector3.Lerp(mdlTrans.localPosition, cardPosition.localPosition, 8f * Time.deltaTime);
            mdlTrans.rotation = Quaternion.Lerp(mdlTrans.rotation, midRotation, 7f * Time.deltaTime);
            if (Vector2.Distance(mdlTrans.localPosition, cardPosition.localPosition) <= Vector2.Distance(cardPosition.localPosition, previousTransform) / 2)
            {
                middleCard.transform.localScale = Vector3.Lerp(middleCard.transform.localScale, new Vector3(1.2f, 1.2f, 1.2f), 7f * Time.deltaTime);
                mdlDisplay.actionCostText.text = "";
                mdlDisplay.attackText.text = "";
                mdlDisplay.strong.gameObject.SetActive(false);
                mdlDisplay.weak.gameObject.SetActive(false);
                mdlDisplay.artworkImage.gameObject.SetActive(false);
                cardInfos = middleCard.transform.GetChild(6).gameObject;
                cardInfos.SetActive(true);
                mdlInfos.card = mdlDisplay;
                mdlInfos.UpdateInfos();
            }

            if (Vector2.Distance(mdlTrans.localPosition, cardPosition.localPosition) < .3f) // trouver la bonne valuer avec la speed
            {
                handToMid = false;
                mdlTrans.localPosition = cardPosition.localPosition;
            }
        }

        if (midToHand)
        {
            var mdlTrans = middleCard.GetComponent<RectTransform>();
            var mdlDisplay = middleCard.GetComponent<CardDisplay>();
            var mdlInfos = middleCard.transform.GetChild(6).gameObject.GetComponent<CardInfos>();

            if (middleCard == null)
            {
                return;
            }
            mdlTrans.localPosition = Vector3.Lerp(mdlTrans.localPosition, previousTransform, 8f * Time.deltaTime);
            mdlTrans.rotation = Quaternion.Lerp(mdlTrans.rotation, previousRotation, 7f * Time.deltaTime);
            if (Vector2.Distance(mdlTrans.localPosition, previousTransform) <= Vector2.Distance(cardPosition.localPosition, previousTransform) / 2)
            {
                middleCard.transform.localScale = Vector3.Lerp(middleCard.transform.localScale, previousScale, 7f * Time.deltaTime);
                mdlInfos.card = null;
                mdlDisplay.artworkImage.gameObject.SetActive(true);
                cardInfos = middleCard.transform.GetChild(6).gameObject;
                cardInfos.SetActive(false);
                mdlDisplay.UpdateCard();
            }
            if (Vector2.Distance(mdlTrans.localPosition, previousTransform) < .3f)
            {
                midToHand = false;
                mdlTrans.localPosition = previousTransform;
                middleCard = null;
                isMid = false;
                isRiseUp = false;
            }
        }

        if (deck.Count < 5)
        {
            for (int i = 0; i < discard.Count; i++)
            {
                int rand = Random.Range(0, discard.Count);
                deck.Add(discard[rand]);
                discard.RemoveAt(rand);
            }
        }
    }

    public void FitCards()
    {
        int numberOfCards = hand.Count;

        if (numberOfCards == 0)
        {
            return;
        }


        for (int i = 0; i < numberOfCards; i++)
        {
            hand[i].transform.position = new Vector3(startLocation.position.x + (i * gap), startLocation.position.y, i);
        }

        if (numberOfCards % 2 != 0)
        {
            for (int i = 0; i < numberOfCards; i++)
            {
                hand[i].transform.position -= new Vector3((numberOfCards / 2) * gap, 0, 0);
            }
        }
        else
        {
            for (int i = 0; i < numberOfCards; i++)
            {
                hand[i].transform.position -= new Vector3((numberOfCards / gap) * 2, 0, 0);
            }
        }
    }

    public void startTheGame()
    {
        pauseButton.SetActive(false);
        inChosenTime = true;
        RollCard();
        chooseCardText.gameObject.SetActive(true);
        deckSelection.SetActive(true);
    }

    public void startCombat()
    {
        pauseButton.SetActive(true);
        for (int i = 0; i < howManyInHand; i++)
        {
            var handCard = Instantiate(cardPrefab, startLocation.position, Quaternion.identity, handPanel);
            Vector3 handCardPosition = handCard.GetComponent<RectTransform>().localPosition;
            handCard.GetComponent<RectTransform>().localPosition = new Vector3(handCardPosition.x, handCardPosition.y, i);
            int rand = Random.Range(0, deck.Count);
            handCard.GetComponent<CardDisplay>().card = deck[rand];
            handCard.GetComponent<CardDisplay>().attackParam = deck[rand].attackParam;
            hand.Add(handCard);
            deck.RemoveAt(rand);
        }
        FitCards();
        inRound = true;
    }

    public void RollCard()
    {
        var newCard = Instantiate(cardPrefab, (transform.position + new Vector3(-4, -1.4f, 0)), Quaternion.identity, canvas.transform);
        var number = Random.Range(0, cardList.Count);
        var newDisplay = newCard.GetComponent<CardDisplay>();
        newDisplay.card = cardList[number];
        newDisplay.attackParam = attackParams[number];
        newDisplay.Start();
        actualRoll.Add(newCard);

        number = Random.Range(0, cardList.Count);
        newCard = Instantiate(cardPrefab, (transform.position + new Vector3(0, -1.4f, 0)), Quaternion.identity, canvas.transform);
        newDisplay = newCard.GetComponent<CardDisplay>();
        newDisplay.card = cardList[number];
        newDisplay.attackParam = attackParams[number];
        newDisplay.Start();
        actualRoll.Add(newCard);

        number = Random.Range(0, cardList.Count);
        newCard = Instantiate(cardPrefab, (transform.position + new Vector3(4, -1.4f, 0)), Quaternion.identity, canvas.transform);
        newDisplay = newCard.GetComponent<CardDisplay>();
        newDisplay.card = cardList[number];
        newDisplay.attackParam = attackParams[number];
        newDisplay.Start();
        actualRoll.Add(newCard);
    }

    public void UseCard()
    {
        if (chosenCard != null)
        {
            discard.Add(chosenCard.GetComponent<CardDisplay>().card);
            hand.Remove(chosenCard);
            Destroy(chosenCard);
            chosenCard = null;
            isMid = false;
            FitCards();
        }
    }

    public void EndRound()
    {
        if (middleCard == null)
        {
            int handCount = hand.Count;
            for (int i = 0; i < handCount; i++)
            {
                discard.Add(hand[0].GetComponent<CardDisplay>().card);
                Destroy(hand[0]);
                hand.RemoveAt(0);
            }
        }
    }


    public void MidToHandLaFonction()
    {
        if(middleCard != null)
        {
            midToHand = true;
        }
    }

    public void toChoice()
    {
        inRound = false;
        inChosenTime = true;
        chooseCardText.gameObject.SetActive(true);
    }
}
