using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public List<GameObject> playerList = new List<GameObject>();
    public List<GameObject> enemyList = new List<GameObject>();

    public PlayerMovement currentPlayer;
    public SaveStats sS;

    public bool noDamage = true;
    public bool isHealed = false;

    public int countMoveEnemy = 0;


    private static CharacterManager _instance = null;

    public static CharacterManager Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
        sS = GetComponent<SaveStats>();
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
        //StartCoroutine("checkAlive");
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
                    PhaseManager.Instance.monsterInOneTurn++;

                    if (PhaseManager.Instance.phase == PhaseManager.actualPhase.PLAYER)
                    {
                        if (BattleManager.Instance.currentAttackParam && BattleManager.Instance.currentAttackParam.range == 1)
                        {

                            /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                            {*/
                                Social.ReportProgress(GPGSIds.achievement_savage, 100.0f, null);
                            //}
                        }
                    }

                    /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                    {*/
                        GooglePlayGames.PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_ruthless, 25, null);
                        GooglePlayGames.PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_heartless, 50, null);
                        GooglePlayGames.PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_genocide, 100, null);
                    //}
                }
            }
        }


        if (playerList.Count == 0)
        {
            Debug.Log("You died");

            /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
            {*/
                //Social.ReportProgress(GPGSIds.achievement_never_gonna_give_you_up, 10.0f, null);
                GooglePlayGames.PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_never_gonna_give_you_up, 10, null);
            //}

            //SceneManager.LoadScene("MainMenu");
            UiActionManager.Instance.StartCoroutine("backToMenu");
        }

        else if (enemyList.Count == 0)
        {
            Debug.Log("You win");
            UiActionManager.Instance.hideAll();

            AudioManager.Instance.Stop("BattleMap1");
            AudioManager.Instance.Play("Victory!");

            sS.setValues(currentPlayer.stats);

            if(PhaseManager.Instance.monsterInOneTurn >= 3)
            {
                /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                {*/
                    Social.ReportProgress(GPGSIds.achievement_slaughter, 100.0f, null);
                //}
            }

            if(PhaseManager.Instance.numberOfTurnBattle <= 3)
            {
                /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                {*/
                    Social.ReportProgress(GPGSIds.achievement_blitzkrieg, 100.0f, null);
                //}
            }

            if (ComboSystem.Instance.onlyOneElem)
            {
                /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                {*/
                    Social.ReportProgress(GPGSIds.achievement_stubborn, 100.0f, null);
                //}
            }

            if (noDamage)
            {
                /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                {*/
                    Social.ReportProgress(GPGSIds.achievement_not_a_scratch, 100.0f, null);
                //}
            }

            if(Grid.Instance.progress != 9)
            {
                CardManager.Instance.RollCard();
                CharacterManager.Instance.currentPlayer.state = PlayerMovement.States.WIN;
                CardManager.Instance.toChoice();
            }
            else
            {
                returnToMap();
            }
        }
    }

    public void returnToMap()
    {
        CardManager.Instance.inChosenTime = false;
        MapComposent.Instance.Opening();
        StartCoroutine("waitforopen");
    }

    private IEnumerator waitforopen()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        MapComposent.Instance.Check();
        Grid.Instance.deleteMap(false);

        /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
        {*/
            Social.ReportProgress(GPGSIds.achievement_a_legend_is_born, 100.0f, null);
            GooglePlayGames.PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_grinder, 30, null);
        //}
    }
}
