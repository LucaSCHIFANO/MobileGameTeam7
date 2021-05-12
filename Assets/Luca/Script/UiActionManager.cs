using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiActionManager : MonoBehaviour
{
    public GameObject buttonHand;
    public Text moveLeft;


    private static UiActionManager _instance = null;

    public static UiActionManager Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }



    public void showButton()
    {
        buttonHand.SetActive(true);
    }

    public void hideButton()
    {
        buttonHand.SetActive(false);
    }

    public void endTurn()
    {
        CharacterManager.Instance.currentPlayer.endTurn();
    }

    public void setMovePoint()
    {
        moveLeft.text = "MP : " + CharacterManager.Instance.currentPlayer.stats.actionPoint.ToString();
    }
}
