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


    public void functionStart() // range les persos en 2 catégorie joueur ou ennemis)
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

    public void enemiesMovement() // fait bougé un ennemi
    {
        var enemy = enemyList[countMoveEnemy];
        enemy.GetComponent<Enemy>().mouvementCheck();
    }

    public void resetAllCharacter() // met tous les persos en etat idle
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


    public IEnumerator checkAlive()
    {
        yield return new WaitForSeconds(0.5f);
        var allCharacters = GameObject.FindGameObjectsWithTag("Characters");
        foreach (var chara in allCharacters)
        {
            if (chara.GetComponent<PlayerMovement>() != null)
            {
                if (chara.GetComponent<Stats>().HP <= 0)
                {
                    playerList.Remove(chara);
                    Destroy(chara);
                }
            }
            else if (chara.GetComponent<Enemy>() != null)
            {
                if (chara.GetComponent<Stats>().HP <= 0)
                {
                    enemyList.Remove(chara);
                    Destroy(chara);
                }
            }
        }

        if(playerList.Count == 0)
        {
            Debug.Log("You died");
        }
        else if(enemyList.Count == 0)
        {
            Debug.Log("You win");
            UiActionManager.Instance.hideAll();

            CardManager.Instance.RollCard();
            CharacterManager.Instance.currentPlayer.state = PlayerMovement.States.WIN;
            CardManager.Instance.toChoice();
        }
    }

    public void returnToMap()
    {
        MapComposent.Instance.Opening();
            MapComposent.Instance.Check();
        Grid.Instance.deleteMap(false);
    }
}
