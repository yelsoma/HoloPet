using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleItemAttack_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
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

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
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
        attackSpeedWait = basicSM.ObjectStatMg.GetAtkSpeed();
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
