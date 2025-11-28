using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleItemAttack_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private BattleMod battleMod;
    private ItemHolderMod itemHolderMod;
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

        IItemHolderMod iItemHolderMod = GetComponentInParent<IItemHolderMod>();
        if (iItemHolderMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no itemHolderSM found in parent.");
        }
        else
        {
            itemHolderMod = iItemHolderMod.ItemHolderMod;
        }

        IBattleMod iBattleMod = GetComponentInParent<IBattleMod>();
        if (iBattleMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no battleMod found in parent.");
        }
        else
        {
            battleMod = iBattleMod.BattleMod;
        }
    }

    public override void Enter()
    {
        attackSpeedWait = 1;
        Debug.Log("attackSpeedNotSetYet");
        itemHolderMod.ItemHolderMg.GetItem().ChangeToItemUse();
    }

    public override void StateUpdate()
    {
        if (!itemHolderMod.ItemHolderMg.GetIsHolding())
        {
            stateMachine.ChangeState(battleMod.BattleStart);
            return;
        }
        if(attackSpeedWait >= 0f)
        {
            attackSpeedWait -= Time.deltaTime;
            return;
        }
        stateMachine.ChangeState(battleMod.BattleStart);
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
