using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart_HoloMem : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private BattleManager battleManager;
    private IItemHolderSM itemHolderSM;

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
        if(itemHolderSM == null)
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
        if (!battleManager.GetIsInbattle())
        {
            stateMachine.ChangeState(basicSM.StateIdle);
            return;
        }
        //check mount here
        if (!basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            //exit to idle
            stateMachine.ChangeState(battleManager.BattleFall);
            return;
        }
        if (!itemHolderSM.ItemHolderMg.GetIsHolding())
        {
            //find item
        }
        stateMachine.ChangeState(battleManager.BattleSearch);
        return;
    }

    public override void StateUpdate()
    {
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
