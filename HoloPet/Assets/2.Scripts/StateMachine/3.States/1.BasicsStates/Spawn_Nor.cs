using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Nor : StateBase
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
        // If spawnState call BoundaryManager here will have bug because of code order. BoundaryManager is also  set on start
        //So call it at lateupdate
    }

    public override void StateUpdate()
    {
        if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            // Exit to StateIdle
            stateMachine.ChangeState(basicMod.StateIdle);
        }
        else
        {
            // Exit to StateInAir
            stateMachine.ChangeState(basicMod.StateInAir);
        }
    }

    public override void StateLateUpdate()
    {     
    }

    public override void Exit()
    {
    }
}
