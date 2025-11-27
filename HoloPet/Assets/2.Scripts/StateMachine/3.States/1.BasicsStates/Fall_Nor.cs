using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Nor : StateBase
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

        basicMod = GetComponentInParent<IBasicMod>().BasicMod;
        if (basicMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
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
            stateMachine.ChangeState(basicMod.StateIdle);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
