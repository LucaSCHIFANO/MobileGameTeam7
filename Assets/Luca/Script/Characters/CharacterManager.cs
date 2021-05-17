using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<GameObject> playerList = new List<GameObject>();
    public List<GameObject> enemyList = new List<GameObject>();

    public PlayerMovement currentPlayer;

    public int countMoveEnemy = 0;


    private static CharacterManager _instance = null;

    public static CharacterManager Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }


    void Start()
    {
        var allCharacters = GameObject.FindGameObjectsWithTag("Characters");
        foreach (var chara in allCharacters)
        {
            if (chara.GetComponent<PlayerMovement>() != null)
            {
                playerList.Add(chara);
            }
            else
            {
                enemyList.Add(chara);
            }
        }
    }

    public void enemiesMovement()
    {
        var enemy = enemyList[countMoveEnemy];
        enemy.GetComponent<Enemy>().mouvementCheck();
    }

    public void resetAllCharacter()
    {
        var allCharacters = GameObject.FindGameObjectsWithTag("Characters");
        foreach (var chara in allCharacters)
        {
            if (chara.GetComponent<PlayerMovement>() != null)
            {
                chara.GetComponent<PlayerMovement>().state = PlayerMovement.States.IDLE;
            }
            else if (chara.GetComponent<Enemy>() != null)
            {
                chara.GetComponent<Enemy>().characterState = PlayerMovement.States.IDLE;
            }
        }
    }
}
