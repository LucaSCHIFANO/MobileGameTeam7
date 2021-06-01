using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfos : MonoBehaviour
{
    public TextMeshProUGUI portee;
    public TextMeshProUGUI puissance;
    public TextMeshProUGUI cout;
    public TextMeshProUGUI description;
    public TextMeshProUGUI cardName;

    public GameObject aoePanel;
    public GameObject poisonPanel;
    public GameObject pushPanel;
    public GameObject pullPanel;

    public CardDisplay card;

    private static CardInfos _instance = null;

    public static CardInfos Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }

    public void UpdateInfos()
    {
        portee.text = "Range : " + card.attackParam.range.ToString() + " blocks";
        puissance.text = "Power : " + card.attackParam.damage.ToString();
        cout.text =  "Cost : " + card.attackParam.APNeeded.ToString() + " AP";
        description.text = "Description : " + card.attackParam.description;
        cardName.text = cardName.text.ToUpper();
        cardName.text = card.attackParam.patternName;

        if (card.attackParam.AOE)
        {
            aoePanel.SetActive(false);
        }
        else
        {
            aoePanel.SetActive(true);
        }

        if (card.attackParam.pull)
        {
            pullPanel.SetActive(false);
        }
        else
        {
            pullPanel.SetActive(true);
        }

        if (card.attackParam.push)
        {
            pushPanel.SetActive(false);
        }
        else
        {
            pushPanel.SetActive(true);
        }

        if (card.attackParam.effect == Stats.EFFECT.POISON)
        {
            poisonPanel.SetActive(false);
        }
        else
        {
            poisonPanel.SetActive(true);
        }
    }
}
