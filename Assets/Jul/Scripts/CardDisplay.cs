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
        nameText.text = card.attackParam.patternName;
        descriptionText.text = card.attackParam.description;

        artworkImage.sprite = card.attackParam.artwork;

        attackText.text = card.attackParam.damage.ToString();
        actionCostText.text = card.attackParam.APNeeded.ToString();
    }

    public void UpdateCard()
    {
        nameText.text = card.attackParam.patternName;
        descriptionText.text = card.attackParam.description;

        artworkImage.sprite = card.attackParam.artwork;

        attackText.text = card.attackParam.damage.ToString();
        actionCostText.text = card.attackParam.APNeeded.ToString();
    }

    private void Update()
    {
        
    }
}
