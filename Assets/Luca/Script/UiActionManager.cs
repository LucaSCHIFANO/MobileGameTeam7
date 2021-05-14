using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiActionManager : MonoBehaviour
{
    public GameObject buttonHand;
    public GameObject buttonCancel;
    public GameObject deck;

    public Text moveLeft;

    public ShowRangeAttack sra;


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
        buttonCancel.SetActive(false);
    }

    public void hideButton()
    {
        buttonHand.SetActive(false);
        buttonCancel.SetActive(true);
        deck.SetActive(false);
    }

    public void showDeck()
    {
        buttonHand.SetActive(false);
        buttonCancel.SetActive(true);
        deck.SetActive(true);

        var player = CharacterManager.Instance.currentPlayer.GetComponent<PlayerMovement>();

        player.state = PlayerMovement.States.SELECTCARD;
    }

    public void hideAll()
    {
        buttonHand.SetActive(false);
        buttonCancel.SetActive(false);
        deck.SetActive(false);
    }

    public void endTurn()
    {
        CharacterManager.Instance.currentPlayer.endTurn();
    }

    public void setMovePoint()
    {
        moveLeft.text = "AC : " + CharacterManager.Instance.currentPlayer.stats.actionPoint.ToString();
    }

    public void cancelButton()
    {
        ClicklManager.Instance.cancelButton();
    }


    public void showAttackRange(AttackParam param)
    {
        var player = CharacterManager.Instance.currentPlayer.GetComponent<PlayerMovement>();
        player.state = PlayerMovement.States.ACTION;

        hideButton();
        sra.testAttackRange(param);
    }

}
