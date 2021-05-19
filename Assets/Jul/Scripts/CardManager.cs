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

    private bool inChosenTime = false;
    private bool inRound = false;
    private bool isMid = false;

    public float totalTwist;

    private List<Card> deck = new List<Card>();

    private List<GameObject> hand = new List<GameObject>();
    private GameObject middleCard = null;

    private List<Card> discard = new List<Card>();

    private GameObject[] actualRoll = new GameObject[3];
    private int index = 0;
    //Hearthstone Style
    public Transform startLocation;
    public float gap;
    public Transform handPanel;
    private Vector3 previousTransform;
    private Quaternion previousRotation;


    void Start()
    {
        gap = 1.5f;
    }

    void Update()
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
                    if (h.collider.gameObject.CompareTag("Card"))
                    { 
                        if (firstCard != h.collider.gameObject)
                        {
                            if (h.collider.gameObject.GetComponent<RectTransform>().localPosition.z > firstCard.GetComponent<RectTransform>().localPosition.z)
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
                        }
                    }
                    
                    if (inRound)
                    {
                        if (!isMid)
                        {
                            previousTransform = hitCard.transform.position;
                            previousRotation = hitCard.transform.rotation;
                            hitCard.transform.position = Vector3.zero;
                            hitCard.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                            middleCard = hitCard;
                            isMid = true;
                        }
                        else if (isMid && middleCard == hitCard)
                        {
                            hitCard.transform.position = previousTransform;
                            hitCard.transform.rotation = previousRotation;
                            middleCard = null;
                            isMid = false;
                        }
                    }    
                }  
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
            var handCard = Instantiate(cardPrefab, startLocation.position, Quaternion.identity, handPanel);
            Vector3 handCardPosition = handCard.GetComponent<RectTransform>().localPosition;
            handCard.GetComponent<RectTransform>().localPosition = new Vector3(handCardPosition.x, handCardPosition.y, i);
            int rand = Random.Range(0, deck.Count);
            handCard.GetComponent<CardDisplay>().card = deck[rand];
            hand.Add(handCard);
            deck.RemoveAt(rand);
        }
        FitCards();
        inRound = true;
    }

    public void RollCard()
    {
        var newCard = Instantiate(cardPrefab, new Vector3(-6, 0, 0), Quaternion.identity, canvas.transform);
        newCard.GetComponent<CardDisplay>().card = cardList[Random.Range(0, cardList.Count)];
        actualRoll[0] = newCard;

        newCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity, canvas.transform);
        newCard.GetComponent<CardDisplay>().card = cardList[Random.Range(0, cardList.Count)];
        actualRoll[1] = newCard;

        newCard = Instantiate(cardPrefab, new Vector3(6, 0, 0), Quaternion.identity, canvas.transform);
        newCard.GetComponent<CardDisplay>().card = cardList[Random.Range(0, cardList.Count)];
        actualRoll[2] = newCard;
    }

    public void UseCard()
    {
        if (middleCard != null)
        {
            discard.Add(middleCard.GetComponent<CardDisplay>().card);
            hand.Remove(middleCard);
            Destroy(middleCard);
            isMid = false;
            FitCards();
        }
    }
}
