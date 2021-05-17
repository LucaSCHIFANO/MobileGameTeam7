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

    private List<Card> deck = new List<Card>();

    private List<GameObject> hand = new List<GameObject>();
    private List<Card> discard = new List<Card>();

    private GameObject[] actualRoll = new GameObject[3];
    private int index = 0;

    //Hearthstone Style
    public Transform startLocation;
    public int nbOfCards;
    public float gap;
    public Transform handPanel;
    private Vector3 previousTransform;

    void Start()
    {
        nbOfCards = 0;
        gap = 1f;
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
                hitCard = firstCard;
            }
            else
            {
                foreach (RaycastHit2D h in hit)
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

            if (hitCard != null)
            {
                Debug.Log(hitCard.GetComponent<CardDisplay>().card.cardName); 
                if (hitCard.CompareTag("Card"))
                {
                    if (inChosenTime)
                    {
                        Debug.Log(hitCard.GetComponent<CardDisplay>().card.cardName) ;

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
                            hitCard.transform.position = Vector3.zero;
                            isMid = true;
                        }
                        else
                        {
                            hitCard.transform.position = previousTransform;
                            isMid = false;
                        }
                    }
                        
                }  
            }
            
        }


    }

    public void FitCards()
    {
        if (hand.Count == 0)
        {
            return;
        }

        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].transform.position += new Vector3(i * gap, 0, 0);
        }

        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].transform.position -= new Vector3(2, 0, 0);
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

    public void CardInDeck()
    {
        
    }
}
