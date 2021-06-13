using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutoCharacterManager : MonoBehaviour
{
    public List<GameObject> playerList = new List<GameObject>();
    public List<GameObject> enemyList = new List<GameObject>();

    public TutoPlayerMovement currentPlayer;
    public TutoSaveStats sS;

    public bool noDamage = true;
    public bool isHealed = false;

    public int countMoveEnemy = 0;


    private static TutoCharacterManager _instance = null;

    public static TutoCharacterManager Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
        sS = GetComponent<TutoSaveStats>();
    }


    public void functionStart() // range les persos en 2 catégorie joueur ou ennemis)
    {
        var allCharacters = GameObject.FindGameObjectsWithTag("Characters");
        foreach (var chara in allCharacters)
        {
            if (chara.GetComponent<TutoPlayerMovement>() != null)
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
        enemy.GetComponent<TutoEnemy>().mouvementCheck();
        //StartCoroutine("checkAlive");
    }

    public void resetAllCharacter() // met tous les persos en etat idle
    {
        var allCharacters = GameObject.FindGameObjectsWithTag("Characters");
        foreach (var chara in allCharacters)
        {
            if (chara.GetComponent<TutoPlayerMovement>() != null)
            {
                chara.GetComponent<TutoPlayerMovement>().state = TutoPlayerMovement.States.IDLE;
            }
            else if (chara.GetComponent<TutoEnemy>() != null)
            {
                chara.GetComponent<TutoEnemy>().characterState = TutoPlayerMovement.States.IDLE;
            }
        }
    }


    public IEnumerator checkAlive()
    {
        yield return new WaitForSeconds(0.5f);
        var allCharacters = GameObject.FindGameObjectsWithTag("Characters");
        foreach (var chara in allCharacters)
        {
            if (chara.GetComponent<TutoPlayerMovement>() != null)
            {
                if (chara.GetComponent<TutoStats>().HP <= 0)
                {
                    playerList.Remove(chara);
                    Destroy(chara);
                }
            }
            else if (chara.GetComponent<TutoEnemy>() != null)
            {
                if (chara.GetComponent<TutoStats>().HP <= 0)
                {
                    enemyList.Remove(chara);
                    Destroy(chara);
                    TutoPhaseManager.Instance.monsterInOneTurn++;

                    if (TutoPhaseManager.Instance.phase == TutoPhaseManager.actualPhase.PLAYER)
                    {
                        if (TutoBattleManager.Instance.currentAttackParam && TutoBattleManager.Instance.currentAttackParam.range == 1)
                        {

                            /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                            {*/
                            Social.ReportProgress(GPGSIds.achievement_savage, 100.0f, null);
                            //}
                        }
                    }

                    /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                    {*/
                    GooglePlayGames.PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_ruthless, 1, null); // 25
                    GooglePlayGames.PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_heartless, 1, null); //50 
                    GooglePlayGames.PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_genocide, 1, null); //100
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
            GooglePlayGames.PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_never_gonna_give_you_up, 1, null); //10
            //}

            //SceneManager.LoadScene("MainMenu");
            TutoUI.Instance.StartCoroutine("backToMenu");
        }

        else if (enemyList.Count == 0)
        {
            Debug.Log("You win");
            TutoUI.Instance.hideAll();
            Tuto.Instance.NoPop63();
            //Tuto.Instance.TruePop7();

            AudioManager.Instance.Stop("BattleMap1");
            AudioManager.Instance.Play("Victory!");

            sS.setValues(currentPlayer.stats);

            if (TutoPhaseManager.Instance.monsterInOneTurn >= 3)
            {
                /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                {*/
                Social.ReportProgress(GPGSIds.achievement_slaughter, 100.0f, null);
                //}
            }

            if (TutoPhaseManager.Instance.numberOfTurnBattle <= 3)
            {
                /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
                {*/
                Social.ReportProgress(GPGSIds.achievement_blitzkrieg, 100.0f, null);
                //}
            }

            if (TutoComboSystem.Instance.onlyOneElem)
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

            if (TutoGrid.Instance.progress != 9)
            {
                TutoCardManager.Instance.RollCard();
                //Tuto.Instance.NoPop8();
                TutoCharacterManager.Instance.currentPlayer.state = TutoPlayerMovement.States.WIN;
                TutoCardManager.Instance.toChoice();
            }
            else
            {
                returnToMap();
            }
        }
    }

    public void returnToMap()
    {
        TutoCardManager.Instance.inChosenTime = false;
        TutoMapComposent.Instance.Opening();
        StartCoroutine("waitforopen");
    }

    private IEnumerator waitforopen()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        TutoMapComposent.Instance.Check();
        TutoGrid.Instance.deleteMap(false);

        /*if (GooglePlayService.Instance.isConnectedToGooglePlayServices)
        {*/
        Social.ReportProgress(GPGSIds.achievement_a_legend_is_born, 100.0f, null);
        GooglePlayGames.PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_grinder, 1, null); //30
        //}
    }
}

