using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart_HoloMem : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private BattleManager battleManager;
    private IItemHolderSM itemHolderSM;

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
            stateMachine.ChangeState(basicMod.StateIdle);
            return;
        }
        //check mount here
        if (!basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
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
