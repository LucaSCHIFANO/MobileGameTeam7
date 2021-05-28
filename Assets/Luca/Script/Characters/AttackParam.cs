using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackParam", menuName = "Attack/AttackParam")]
public class AttackParam : ScriptableObject
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

    public Stats.ELEMENT element;

    public bool AOE;
    public AttackParam aoeEffect;

    public bool around;


    public Stats.EFFECT effect;
    public int duration;
    public int intensity;

    public Sprite artwork;
    public Sprite BackGround;
}
