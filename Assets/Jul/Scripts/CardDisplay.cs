using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public AttackParam attackParam;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public Image artworkImage;

    public TextMeshProUGUI attackText;
    public TextMeshProUGUI actionCostText;

    void Start()
    {
        nameText.text = card.name;
        descriptionText.text = card.description;

        artworkImage.sprite = card.artwork;

        attackText.text = card.attackDamage.ToString();
        actionCostText.text = card.actionCost.ToString();
    }

    public void UpdateCard()
    {
        nameText.text = card.name;
        descriptionText.text = card.description;

        artworkImage.sprite = card.artwork;

        attackText.text = card.attackDamage.ToString();
        actionCostText.text = card.actionCost.ToString();
    }

    private void Update()
    {
        
    }
}
