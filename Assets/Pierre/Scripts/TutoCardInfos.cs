using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutoCardInfos : MonoBehaviour
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

    public Animator aoeAnimator;
    public Animator poisonAnimator;
    public Animator pushAnimator;
    public Animator pullAnimator;

    public TutoCardDisplay card;

    public void UpdateInfos()
    {
        portee.text = "Range : " + card.attackParam.range.ToString() + " blocks";
        puissance.text = "Power : " + card.attackParam.damage.ToString();
        cout.text = "Cost : " + card.attackParam.APNeeded.ToString() + " AP";
        description.text = card.attackParam.description;
        cardName.text = cardName.text.ToUpper();
        cardName.text = card.attackParam.patternName;

        if (card.attackParam.AOE)
        {
            aoePanel.GetComponent<Image>().color = new Color(aoePanel.GetComponent<Image>().color.r, aoePanel.GetComponent<Image>().color.g, aoePanel.GetComponent<Image>().color.b, 0f);
            aoeAnimator.speed = 1f;
        }
        else
        {
            aoePanel.GetComponent<Image>().color = new Color(aoePanel.GetComponent<Image>().color.r, aoePanel.GetComponent<Image>().color.g, aoePanel.GetComponent<Image>().color.b, .7f);
            aoeAnimator.speed = 0f;
        }

        if (card.attackParam.pull)
        {
            pullPanel.GetComponent<Image>().color = new Color(pullPanel.GetComponent<Image>().color.r, pullPanel.GetComponent<Image>().color.g, pullPanel.GetComponent<Image>().color.b, 0f);
            pullAnimator.speed = 1f;
        }
        else
        {
            pullPanel.GetComponent<Image>().color = new Color(pullPanel.GetComponent<Image>().color.r, pullPanel.GetComponent<Image>().color.g, pullPanel.GetComponent<Image>().color.b, .7f);
            pullAnimator.speed = 0f;
        }

        if (card.attackParam.push)
        {
            pushPanel.GetComponent<Image>().color = new Color(pushPanel.GetComponent<Image>().color.r, pushPanel.GetComponent<Image>().color.g, pushPanel.GetComponent<Image>().color.b, 0f);
            pushAnimator.speed = 1f;
        }
        else
        {
            pushPanel.GetComponent<Image>().color = new Color(pushPanel.GetComponent<Image>().color.r, pushPanel.GetComponent<Image>().color.g, pushPanel.GetComponent<Image>().color.b, .7f);
            pushAnimator.speed = 0f;
        }

        if (card.attackParam.effect == TutoStats.EFFECT.POISON)
        {
            poisonPanel.GetComponent<Image>().color = new Color(poisonPanel.GetComponent<Image>().color.r, poisonPanel.GetComponent<Image>().color.g, poisonPanel.GetComponent<Image>().color.b, 0f);
            poisonAnimator.speed = 1f;
        }
        else
        {
            poisonPanel.GetComponent<Image>().color = new Color(poisonPanel.GetComponent<Image>().color.r, poisonPanel.GetComponent<Image>().color.g, poisonPanel.GetComponent<Image>().color.b, .7f);
            poisonAnimator.speed = 0f;
        }
    }
}
