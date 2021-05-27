using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesBoard : MonoBehaviour
{
    public List<Image> boardImage = new List<Image>();

    private static EnemiesBoard _instance = null;

    public static EnemiesBoard Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }

    public void CheckList()
    {
        ClearList();

        
        for (int i = 0; i < CharacterManager.Instance.enemyList.Count; i++)
        {
            if (CharacterManager.Instance.enemyList[i])
            {
                boardImage[i].sprite = CharacterManager.Instance.enemyList[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
                boardImage[i].color = CharacterManager.Instance.enemyList[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
            }
            else
            {
                boardImage[i].sprite = null;
                boardImage[i].color = new Color(0f, 0f, 0f, 0f);
            }
        }
    }

    public void ClearList()
    {
        for (int i = 0; i < boardImage.Count; i++)
        {
            boardImage[i].sprite = null;
            boardImage[i].color = new Color(0f, 0f, 0f, 0f);
        }
    }
}
