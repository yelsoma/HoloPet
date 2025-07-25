using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicState_Spawn : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform} �X no StateMachineBase found in parent.");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform} �X no basicSM found in parent.");
        }
    }

    public override void Enter()
    {
        // If call BoundaryManager here will have bug because of code order. BoundaryManager is also  set on start
    }

    public override void StateUpdate()
    {    
    }

    public override void StateLateUpdate()
    {
        if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            Debug.Log("shit i float" + stateMachine.ToString());
            // Exit to StateIdle
            stateMachine.ChangeState(basicSM.StateIdle);
        }
        else
        {
            // Exit to StateInAir
            stateMachine.ChangeState(basicSM.StateInAir);
        }
    }

    public override void Exit()
    {
    }
}
