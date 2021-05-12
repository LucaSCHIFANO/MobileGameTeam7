using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackParam", menuName = "Attack/AttackParam")]
public class AttackParam : ScriptableObject
{
    public string patternName;
    public int range;
    public bool row_column;
    public bool throughWall;
}
