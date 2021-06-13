using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutoAttackParam", menuName = "Attack/TutoAttackParam")]
public class TutoAttackParam : ScriptableObject
{
    public string patternName;
    public string description;

    public int range;
    public bool row_column;
    public bool throughWall;

    public int APNeeded;
    public int damage;

    public bool push;
    public bool pull;

    public TutoStats.ELEMENT element;

    public bool AOE;
    public TutoAttackParam aoeEffect;

    public bool around;


    public TutoStats.EFFECT effect;
    public int duration;
    public int intensity;

    public Sprite artwork;
    public Sprite BackGround;

    public GameObject effectAttack;


    public string musicName;
}
