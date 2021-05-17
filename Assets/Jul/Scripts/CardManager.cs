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

    void Start()
    {
        nbOfCards = 0;
        gap = 1;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                if (inChosenTime)
                {
                    if (hit.collider.gameObject.CompareTag("Card"))
                    {
                        Debug.Log(hit.collider.gameObject.name);

                        deck.Add(hit.collider.gameObject.GetComponent<CardDisplay>().card);

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
            int rand = Random.Range(0, deck.Count);
            handCard.GetComponent<CardDisplay>().card = deck[rand];
            hand.Add(handCard);
            deck.RemoveAt(rand);
        }
        FitCards();
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
