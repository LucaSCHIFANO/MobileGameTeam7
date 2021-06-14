using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoPhaseManager : MonoBehaviour
{
    public actualPhase phase;
    public bool oneTime = true;

    public bool selectFoe = false;

    public GameObject uiColor;

    public int numberOfTurn;

    public int numberOfTurnRecord;

    public int monsterInOneTurn;
    public int numberOfTurnBattle;

    public enum actualPhase
    {
        PLAYER,
        ENEMY,
        BEGIN,
    }


    private static TutoPhaseManager _instance = null;

    public static TutoPhaseManager Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
        numberOfTurnRecord = PlayerPrefs.GetInt("NumberOfTurn");
    }


    void Start()
    {
        phase = actualPhase.BEGIN;
        Debug.Log("Player Phase");
    }

    void Update() // demarre la phase du joueur ou des ennemis
    {
        if (phase == actualPhase.ENEMY && !oneTime)
        {
            oneTime = true;
            TutoCharacterManager.Instance.enemiesMovement();
            uiColor.GetComponent<Image>().color = Color.red;

        }
        else if (phase == actualPhase.PLAYER && !oneTime)
        {
            numberOfTurn++;
            numberOfTurnBattle++;
            TutoUI.Instance.turnText.text = "TURN " + numberOfTurnBattle.ToString();
            oneTime = true;
            TutoCharacterManager.Instance.resetAllCharacter();
            uiColor.GetComponent<Image>().color = Color.blue;
            TutoUI.Instance.showButton();
            TutoCardManager.Instance.startCombat();

            monsterInOneTurn = 0;

            if (TutoCharacterManager.Instance.currentPlayer != null)
            {
                TutoCharacterManager.Instance.currentPlayer.stats.boostAPUsed = 0;

                TutoComboSystem.Instance.comboEffect(TutoCharacterManager.Instance.currentPlayer.stats.element, TutoCharacterManager.Instance.currentPlayer.stats.elementCombo);
                TutoComboSystem.Instance.resetSave();
            }

        }
    }


    public void checkAllPlayer() // check si des players n'ont pas joueur
    {
        int number = 0;
        foreach (var player in TutoCharacterManager.Instance.playerList)
        {
            if (player.GetComponent<TutoPlayerMovement>().state != TutoPlayerMovement.States.WAIT)
            {
                number++;
            }
        }
        if (number == 0)
        {
            phase = actualPhase.ENEMY;
            oneTime = false;
            Debug.Log("Enemy Phase");
        }
    }

    public void checkAllEnemies() // check si tous les ennemies ont joués
    {
        TutoCharacterManager.Instance.StartCoroutine("checkAlive");

        int number = 0;
        foreach (var enemy in TutoCharacterManager.Instance.enemyList)
        {
            if (enemy.GetComponent<TutoEnemy>().characterState != TutoPlayerMovement.States.WAIT)
            {
                number++;
            }
        }


        if (number == 0)
        {
            phase = actualPhase.PLAYER;
            oneTime = false;
            Debug.Log("Player Phase");
            TutoCharacterManager.Instance.countMoveEnemy = 0;

            foreach (var enemy in TutoCharacterManager.Instance.enemyList)
            {
                var ene = enemy.GetComponent<TutoEnemy>();
                ene.stats.actionPoint = ene.stats.maxActionPoint;
            }
        }
        else
        {

            int numberbefore = TutoCharacterManager.Instance.enemyList.Count;

            if (TutoCharacterManager.Instance.enemyList.Count >= numberbefore)
            {
                TutoCharacterManager.Instance.countMoveEnemy++;
            }

            TutoCharacterManager.Instance.enemiesMovement();
        }
    }
}
