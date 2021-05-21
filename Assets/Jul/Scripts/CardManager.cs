using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [Header("KDO LES GD")]
    public int nbTirageDebut = 5;
    public int howManyInHand = 5;
    public Canvas canvas;
    public GameObject cardPrefab;
    public List<Card> cardList = new List<Card>();
    public List<AttackParam> attackParams = new List<AttackParam>();

    private bool inChosenTime = false;
    private bool inRound = false;
    private bool isMid = false;

    public bool handToMid = false;
    public bool midToHand = false;
    private Quaternion midRotation;

    private float totalTwist;

    private List<Card> deck = new List<Card>();

    private List<GameObject> hand = new List<GameObject>();
    public GameObject middleCard = null;
    public GameObject chosenCard = null;

    private List<Card> discard = new List<Card>();

    private GameObject[] actualRoll = new GameObject[3];
    private int index = 0;
    //Hearthstone Style
    public Transform startLocation;
    public float gap;
    public Transform handPanel;
    public Transform letrucquibouge;
    private Vector3 previousTransform;
    private Quaternion previousRotation;

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
        gap = 1.5f;
        midRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    void Update()
    {
        if(!handToMid && !midToHand)
        {
            if (Input.GetMouseButtonDown(0))
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
                            deck.Add(hitCard.GetComponent<CardDisplay>().card);
                            hitCard.GetComponent<CardDisplay>().card.attackParam = hitCard.GetComponent<CardDisplay>().attackParam;

                            for (int i = 0; i < actualRoll.Length; i++)
                            {
                                Destroy(actualRoll[i]);
                                actualRoll[i] = null;
                            }

                            if (index < nbTirageDebut - 1)
                            {
                                RollCard();
                                index++;
                            }
                            else
                            {
                                inChosenTime = false;
                                MapComposent.Instance.Opening();
                                //Grid.Instance.functionStart();
                            }
                        }

                        if (inRound)
                        {
                            if (!isMid)
                            {
                                previousTransform = hitCard.GetComponent<RectTransform>().localPosition;
                                previousRotation = hitCard.transform.rotation;
                                middleCard = hitCard;
                                handToMid = true;
                                isMid = true;
                            }
                            else if (isMid && middleCard == hitCard)
                            {
                                midToHand = true;
                            }
                        }
                    }
                }
            }
        }

        if (handToMid)
        {
            if (middleCard == null)
            {
                return;
            }
            middleCard.GetComponent<RectTransform>().localPosition = Vector3.Lerp(middleCard.GetComponent<RectTransform>().localPosition, transform.position, .05f);
            middleCard.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(middleCard.GetComponent<RectTransform>().localRotation, midRotation, .05f);
            if (Vector2.Distance(middleCard.GetComponent<RectTransform>().localPosition, transform.position) < 2f)
            {
                handToMid = false;
                middleCard.GetComponent<RectTransform>().localPosition = handPanel.position;
            }
        }

        if (midToHand)
        {
            if (middleCard == null)
            {
                return;
            }
            middleCard.GetComponent<RectTransform>().localPosition = Vector3.Lerp(middleCard.GetComponent<RectTransform>().localPosition, previousTransform, 0.1f);
            middleCard.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(middleCard.GetComponent<RectTransform>().localRotation, previousRotation, .05f);
            if (Vector2.Distance(middleCard.GetComponent<RectTransform>().localPosition, previousTransform) < 2f)
            {
                midToHand = false;
                middleCard.GetComponent<RectTransform>().localPosition = previousTransform;
                middleCard = null;
                isMid = false;
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

        if (numberOfCards <= 5)
        {
            totalTwist = 40f;
            if (numberOfCards <= 4)
            {
                totalTwist = 30f;
                if (numberOfCards <= 3)
                {
                    totalTwist = 20f;
                    if (numberOfCards <= 1)
                    {
                        totalTwist = 0f;
                    }
                }
            }
        }

        float twistedPerCard = totalTwist / numberOfCards;
        float startTwist = -1f * (totalTwist / 2f);

        for (int i = 0; i < numberOfCards; i++)
        {
            hand[i].transform.position = new Vector3(startLocation.position.x + (i * gap), startLocation.position.y, i);
            float twistForThisCard = startTwist + (i * twistedPerCard);
            hand[i].transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            hand[i].transform.Rotate(0f, 0f, -twistForThisCard);

            float scalingFactor = 0.01f;
            float nudgeThisCard = Mathf.Abs(twistForThisCard);
            nudgeThisCard *= scalingFactor;
            hand[0].transform.Translate(0f, -nudgeThisCard, 0f);
            hand[numberOfCards - 1].transform.Translate(0f, -nudgeThisCard, 0f);
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
                hand[i].transform.position -= new Vector3((numberOfCards / gap), 0, 0);
            }
        }
    }

    public void startTheGame()
    {
        inChosenTime = true;
        RollCard();
    }

    public void startCombat()
    {
        for (int i = 0; i < howManyInHand; i++)
        {
            var handCard = Instantiate(cardPrefab, startLocation.position, Quaternion.identity, letrucquibouge);
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
        
        var newCard = Instantiate(cardPrefab, (transform.position + new Vector3(-6, 0, 0)), Quaternion.identity, canvas.transform);
        var number = Random.Range(0, cardList.Count);
        newCard.GetComponent<CardDisplay>().card = cardList[number];
        newCard.GetComponent<CardDisplay>().attackParam = attackParams[number];
        actualRoll[0] = newCard;

        number = Random.Range(0, cardList.Count);
        newCard = Instantiate(cardPrefab, (transform.position + new Vector3(0, 0, 0)), Quaternion.identity, canvas.transform);
        newCard.GetComponent<CardDisplay>().card = cardList[number];
        newCard.GetComponent<CardDisplay>().attackParam = attackParams[number];
        actualRoll[1] = newCard;

        number = Random.Range(0, cardList.Count);
        newCard = Instantiate(cardPrefab, (transform.position + new Vector3(6, 0, 0)), Quaternion.identity, canvas.transform);
        newCard.GetComponent<CardDisplay>().card = cardList[number];
        newCard.GetComponent<CardDisplay>().attackParam = attackParams[number];
        actualRoll[2] = newCard;
    }

    public void UseCard()
    {
        if (chosenCard != null)
        {
            discard.Add(chosenCard.GetComponent<CardDisplay>().card);
            hand.Remove(chosenCard);
            Destroy(chosenCard);
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
}
