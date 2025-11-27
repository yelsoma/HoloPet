using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleItemAttack_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private BattleManager battleManager;
    private IItemHolderSM itemHolderSM;
    private float attackSpeedWait;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        }

        IBasicMod ibasicMod = GetComponentInParent<IBasicMod>();
        if (ibasicMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
        else
        {
            basicMod = ibasicMod.BasicMod;
        }

        itemHolderSM = GetComponentInParent<IItemHolderSM>();
        if (itemHolderSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no IItemHolderSM found in parent.");
        }

        battleManager = GetComponentInParent<BattleManager>();
        if (battleManager == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no battleManager found in parent.");
        }
    }

    public override void Enter()
    {
        attackSpeedWait = basicMod.ObjectStatMg.GetAtkSpeed();
        itemHolderSM.ItemHolderMg.GetItem().ChangeToItemUse();
    }

    public override void StateUpdate()
    {
        if (!itemHolderSM.ItemHolderMg.GetIsHolding())
        {
            stateMachine.ChangeState(battleManager.BattleStart);
            return;
        }
        if(attackSpeedWait >= 0f)
        {
            attackSpeedWait -= Time.deltaTime;
            return;
        }
        stateMachine.ChangeState(battleManager.BattleStart);
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
