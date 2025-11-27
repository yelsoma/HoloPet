using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;

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
    }

    public override void Enter()
    {
        if (!basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicMod.StateInAir);
        }
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
