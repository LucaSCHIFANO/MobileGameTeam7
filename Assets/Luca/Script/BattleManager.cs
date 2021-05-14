using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public AttackParam currentAttackParam;
    public GameObject damageEffect;

    private static BattleManager _instance = null;

    public static BattleManager Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }

    public void attackUnit(Stats att, Stats def)
    {
        Grid.Instance.resetClicked();

        var totalAtt = att.strenght + currentAttackParam.damage;

        var damage = 0;

        if (totalAtt - def.defense > 0)
        {
            damage = totalAtt - def.defense;
        }

        def.HP -= damage;
        Instantiate(damageEffect, def.gameObject.transform.GetChild(0).position, def.gameObject.transform.rotation);
        CharacterManager.Instance.StartCoroutine("checkAlive");

        if(PhaseManager.Instance.phase == PhaseManager.actualPhase.PLAYER)
        {   
            att.GetComponent<PlayerMovement>().state = PlayerMovement.States.IDLE;
            UiActionManager.Instance.showButton();
        }

    }
}
