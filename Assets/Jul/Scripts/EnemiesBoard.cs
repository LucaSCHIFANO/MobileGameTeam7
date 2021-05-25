using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesBoard : MonoBehaviour
{
    public List<GameObject> boardList = new List<GameObject>();

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
            boardList.Add(CharacterManager.Instance.enemyList[i]);
        }
    }

    public void ClearList()
    {
        boardList.Clear();
    }
}
