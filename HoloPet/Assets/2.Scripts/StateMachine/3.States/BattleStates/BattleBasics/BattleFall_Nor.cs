using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFall_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private BattleManager battleManager;

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

        battleManager = GetComponentInParent<BattleManager>();
        if (battleManager == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no battleManager found in parent.");
        }
    }

    public override void Enter()
    {
        basicMod.PhysicsMg.ResetFall();
    }

    public override void StateUpdate()
    {
        //fall
        basicMod.PhysicsMg.KeepFall();
        if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            //exit to idle
            stateMachine.ChangeState(battleManager.BattleStart);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
