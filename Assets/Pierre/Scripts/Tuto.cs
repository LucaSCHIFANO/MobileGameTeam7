using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tuto : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject Popup1;
    public GameObject Popup11;
    public GameObject Popup12;
    public GameObject Popup13;
    //public GameObject Popup2;
    public GameObject Popup3;
    public GameObject Popup4;
    public GameObject Popup41;
    public GameObject Popup42;
    public GameObject Popup43;
    public GameObject Popup44;
    public GameObject Popup45;
    public GameObject Popup5;
    public GameObject Popup51;
    public GameObject Popup511;
    public GameObject Popup52;
    public GameObject Popup6;
    public GameObject Popup61;
    //public GameObject Popup611;
    //public GameObject Popup612;
    public GameObject Popup62;
    public GameObject Popup63;
    public GameObject Popup7;
    public GameObject Popup8;
    public GameObject Popup81;
    public GameObject Popup82;
    public GameObject Popup83;

    public GameObject buttonCard;
    public GameObject buttonTurn;

    private bool pop6 = true;

    private static Tuto _instance = null;

    public static Tuto Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Popup1.SetActive(true);
        Popup11.SetActive(false);
        Popup12.SetActive(false);
        Popup13.SetActive(false);
        //Popup2.SetActive(false);
        Popup3.SetActive(false);
        Popup4.SetActive(false);
        Popup41.SetActive(false);
        Popup42.SetActive(false);
        Popup43.SetActive(false);
        Popup44.SetActive(false);
        Popup45.SetActive(false);
        Popup5.SetActive(false);
        Popup51.SetActive(false);
        Popup511.SetActive(false);
        Popup52.SetActive(false);
        Popup6.SetActive(false);
        Popup61.SetActive(false);
        //Popup611.SetActive(false);
        //Popup612.SetActive(false);
        Popup62.SetActive(false);
        Popup63.SetActive(false);
        Popup7.SetActive(false);
        Popup8.SetActive(false);
        Popup81.SetActive(false);
        Popup82.SetActive(false);
        Popup83.SetActive(false);
    }

    public void Resume()
    {
        Popup1.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        Popup1.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Pop1()
    {
        Popup1.SetActive(false);
        Popup11.SetActive(true);
    }

    public void Pop11()
    {
        Popup11.SetActive(false);
        Popup12.SetActive(true);
    }

    public void Pop12()
    {
        Popup12.SetActive(false);
        Popup13.SetActive(true);
    }

    public void Pop13()
    {
        Popup13.SetActive(false);
        //Popup2.SetActive(true);
    }

    public void Pop2()
    {
        //Popup2.SetActive(false);
        //Popup3.SetActive(true);
    }

    public void Pop2_1()
    {
        Popup3.SetActive(true);
    }

    public void Pop3()
    {
        Popup3.SetActive(false);
        Popup4.SetActive(true);
        buttonCard.SetActive(false);
        buttonTurn.SetActive(false);
    }

    public void Pop4()
    {
        Popup4.SetActive(false);
        Popup41.SetActive(true);
    }

    public void Pop41()
    {
        Popup41.SetActive(false);
        Popup42.SetActive(true);
    }

    public void Pop42()
    {
        Popup42.SetActive(false);
        Popup43.SetActive(true);
    }

    public void Pop43()
    {
        Popup43.SetActive(false);
        Popup44.SetActive(true);
    }

    public void Pop44()
    {
        Popup44.SetActive(false);
        Popup45.SetActive(true);
    }

    public void Pop45()
    {
        Popup45.SetActive(false);
        Popup5.SetActive(true);
        TutoClickManager.Instance.canClickEnemy = true;
    }

    public void Pop5()
    {
        Popup5.SetActive(false);
        Popup51.SetActive(true);
        TutoClickManager.Instance.canClickPlayer = true;
    }

    public void Pop51()
    {
        Popup51.SetActive(false);
        Popup511.SetActive(true);
        buttonTurn.SetActive(true);
    }

    public void Pop511()
    {
        Popup51.SetActive(false);
        Popup511.SetActive(false);
        Popup52.SetActive(true);
        buttonCard.SetActive(true);
    }

    public void Pop52()
    {
        Popup52.SetActive(false);
        Popup6.SetActive(true);
    }

    public void Pop6()
    {
        if (pop6)
        {
            Popup6.SetActive(false);
            Popup61.SetActive(true);
            pop6 = false;
        }
    }

    public void Pop61()
    {
        Popup61.SetActive(false);
        Popup62.SetActive(true);
    }

    /*public void Pop611()
    {
        Popup611.SetActive(false);
        Popup612.SetActive(true);
    }

    public void Pop612()
    {
        Popup612.SetActive(false);
        Popup62.SetActive(true);
    }*/

    public void Pop62()
    {
        Popup62.SetActive(false);
        Popup63.SetActive(true);
    }

    public void Pop63()
    {
        Popup41.SetActive(false);
        Popup42.SetActive(false);
        Popup43.SetActive(false);
        Popup44.SetActive(false);
        Popup45.SetActive(false);
        Popup5.SetActive(false);
        Popup51.SetActive(false);
        Popup52.SetActive(false);
        Popup6.SetActive(false);
        Popup61.SetActive(false);
        Popup62.SetActive(false);
        Popup63.SetActive(false);
        Popup7.SetActive(true);
    }

    public void NoPop63()
    {
        Popup41.SetActive(false);
        Popup42.SetActive(false);
        Popup43.SetActive(false);
        Popup44.SetActive(false);
        Popup45.SetActive(false);
        Popup5.SetActive(false);
        Popup51.SetActive(false);
        Popup52.SetActive(false);
        Popup6.SetActive(false);
        Popup61.SetActive(false);
        Popup62.SetActive(false);
        Popup63.SetActive(false);
        Popup63.SetActive(false);
    } 

    public void Pop7()
    {
        Popup1.SetActive(false);
        Popup11.SetActive(false);
        Popup12.SetActive(false);
        Popup13.SetActive(false);
        //Popup2.SetActive(false);
        Popup3.SetActive(false);
        Popup4.SetActive(false);
        Popup41.SetActive(false);
        Popup42.SetActive(false);
        Popup43.SetActive(false);
        Popup44.SetActive(false);
        Popup45.SetActive(false);
        Popup5.SetActive(false);
        Popup51.SetActive(false);
        Popup52.SetActive(false);
        Popup6.SetActive(false);
        Popup61.SetActive(false);
        Popup62.SetActive(false);
        Popup63.SetActive(false);
        Popup7.SetActive(false);
        Popup8.SetActive(false);
        //Popup81.SetActive(true);
    }

    public void TruePop7()
    {
        Popup41.SetActive(false);
        Popup42.SetActive(false);
        Popup43.SetActive(false);
        Popup44.SetActive(false);
        Popup45.SetActive(false);
        Popup5.SetActive(false);
        Popup51.SetActive(false);
        Popup52.SetActive(false);
        Popup6.SetActive(false);
        Popup61.SetActive(false);
        Popup62.SetActive(false);
        Popup63.SetActive(false);
        Popup7.SetActive(false);
        Popup8.SetActive(true);
    }
    public void Pop8()
    {
        Popup81.SetActive(true);
    }

    public void NoPop8()
    {
        Popup8.SetActive(false);
    }

    public void Pop81()
    {
        Popup81.SetActive(false);
        Popup82.SetActive(true);
    }

    public void Pop82()
    {
        Popup82.SetActive(false);
        Popup83.SetActive(true);
    }

    public void Pop83()
    {
        Popup83.SetActive(false);
        SceneManager.LoadScene("NewSceneJul");
    }
}
