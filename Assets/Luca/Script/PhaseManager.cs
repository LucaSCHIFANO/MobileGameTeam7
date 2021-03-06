using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseManager : MonoBehaviour
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


    private static PhaseManager _instance = null;

    public static PhaseManager Instance
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
            CharacterManager.Instance.enemiesMovement();
            uiColor.GetComponent<Image>().color = Color.red;

        }
        else if (phase == actualPhase.PLAYER && !oneTime)
        {
            numberOfTurn++;
            numberOfTurnBattle++;
            oneTime = true;
            CharacterManager.Instance.resetAllCharacter();
            uiColor.GetComponent<Image>().color = Color.blue;
            UiActionManager.Instance.showButton();
            CardManager.Instance.startCombat();

            monsterInOneTurn = 0;

            if(CharacterManager.Instance.currentPlayer != null)
            {
                CharacterManager.Instance.currentPlayer.stats.boostAPUsed = 0;

                ComboSystem.Instance.comboEffect(CharacterManager.Instance.currentPlayer.stats.element, CharacterManager.Instance.currentPlayer.stats.elementCombo);
                ComboSystem.Instance.resetSave();
            }

        }
    }


    public void checkAllPlayer() // check si des players n'ont pas joueur
    {
        int number = 0;
        foreach (var player in CharacterManager.Instance.playerList)
        {
            if (player.GetComponent<PlayerMovement>().state != PlayerMovement.States.WAIT)
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

    public void checkAllEnemies() // check si tous les ennemies ont jou?s
    {
        int number = 0;
        foreach (var enemy in CharacterManager.Instance.enemyList)
        {
            if (enemy.GetComponent<Enemy>().characterState != PlayerMovement.States.WAIT)
            {
                number++;
            }
        }

        if (number == 0)
        {
            phase = actualPhase.PLAYER;
            oneTime = false;
            Debug.Log("Player Phase");
            CharacterManager.Instance.countMoveEnemy = 0;

            foreach (var enemy in CharacterManager.Instance.enemyList)
            {
                var ene = enemy.GetComponent<Enemy>();
                ene.stats.actionPoint = ene.stats.maxActionPoint;
            }
        }
        else
        {
            CharacterManager.Instance.countMoveEnemy++;
            CharacterManager.Instance.enemiesMovement();
        }
    }
}