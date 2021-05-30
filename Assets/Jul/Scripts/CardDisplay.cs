using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public AttackParam attackParam;

    public TextMeshProUGUI descriptionText;

    public Image backGroundImage;
    public Image artworkImage;

    public TextMeshProUGUI attackText;
    public TextMeshProUGUI actionCostText;

    public void Start()
    {
        if (card != null && attackParam != null)
        {
            attackText.text = attackParam.damage.ToString();
            actionCostText.text = attackParam.APNeeded.ToString();

            descriptionText.text = attackParam.description;

            backGroundImage.sprite = attackParam.BackGround;
            artworkImage.sprite = attackParam.artwork;
        }

        if (attackParam.artwork == null)
        {
            artworkImage.gameObject.SetActive(false);
        }
    }

    public void UpdateCard()
    {
        if (card != null && attackParam != null)
        {
            attackText.text = attackParam.damage.ToString();
            actionCostText.text = attackParam.APNeeded.ToString();

            descriptionText.text = card.attackParam.description;

            backGroundImage.sprite = card.attackParam.BackGround;
            artworkImage.sprite = card.attackParam.artwork;
        }

        if (attackParam.artwork == null)
        {
            artworkImage.gameObject.SetActive(false);
        }
    }
}
