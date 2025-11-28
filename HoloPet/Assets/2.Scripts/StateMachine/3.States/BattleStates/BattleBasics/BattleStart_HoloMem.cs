using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart_HoloMem : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private BattleMod battleMod;
    private ItemHolderMod itemHolderMod;

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
        if (!battleMod.GetIsInbattle())
        {
            stateMachine.ChangeState(basicMod.StateIdle);
            return;
        }
        //check mount here
        if (!basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            //exit to idle
            stateMachine.ChangeState(battleMod.BattleFall);
            return;
        }
        if (!itemHolderMod.ItemHolderMg.GetIsHolding())
        {
            //find item
        }
        stateMachine.ChangeState(battleMod.BattleSearch);
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
