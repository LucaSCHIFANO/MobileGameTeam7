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

    public Image strong;
    public Image weak;
    public Sprite[] elementSprite;

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

        switch (attackParam.element)
        {
            case Stats.ELEMENT.NORMAL:
                attackText.color = new Color(0.509804f, 0.509804f, 0.509804f);
                actionCostText.color = new Color(0.509804f, 0.509804f, 0.509804f);
                strong.sprite = elementSprite[3];
                weak.sprite = elementSprite[3];
                break;
            case Stats.ELEMENT.RED:
                attackText.color = new Color(0.6078432f, 0.1647059f, 0.1058824f);
                actionCostText.color = new Color(0.6078432f, 0.1647059f, 0.1058824f);
                strong.sprite = elementSprite[2];
                weak.sprite = elementSprite[0];
                break;
            case Stats.ELEMENT.BLUE:
                attackText.color = new Color(0.1647059f, 0.3921569f, 0.6039216f);
                actionCostText.color = new Color(0.1647059f, 0.3921569f, 0.6039216f);
                strong.sprite = elementSprite[1];
                weak.sprite = elementSprite[2];
                break;
            case Stats.ELEMENT.GREEN:
                attackText.color = new Color(0f, 0.6470588f, 0.3294118f);
                actionCostText.color = new Color(0f, 0.6470588f, 0.3294118f);
                strong.sprite = elementSprite[0];
                weak.sprite = elementSprite[1];
                break;
            default:
                break;
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

        switch (attackParam.element)
        {
            case Stats.ELEMENT.NORMAL:
                attackText.color = new Color(0.509804f, 0.509804f, 0.509804f);
                actionCostText.color = new Color(0.509804f, 0.509804f, 0.509804f);
                strong.sprite = elementSprite[3];
                weak.sprite = elementSprite[3];
                break;
            case Stats.ELEMENT.RED:
                attackText.color = new Color(0.6078432f, 0.1647059f, 0.1058824f);
                actionCostText.color = new Color(0.6078432f, 0.1647059f, 0.1058824f);
                strong.sprite = elementSprite[2];
                weak.sprite = elementSprite[0];
                break;
            case Stats.ELEMENT.BLUE:
                attackText.color = new Color(0.1647059f, 0.3921569f, 0.6039216f);
                actionCostText.color = new Color(0.1647059f, 0.3921569f, 0.6039216f);
                strong.sprite = elementSprite[1];
                weak.sprite = elementSprite[2];
                break;
            case Stats.ELEMENT.GREEN:
                attackText.color = new Color(0f, 0.6470588f, 0.3294118f);
                actionCostText.color = new Color(0f, 0.6470588f, 0.3294118f);
                strong.sprite = elementSprite[0];
                weak.sprite = elementSprite[1];
                break;
            default:
                break;
        }
    }
}
