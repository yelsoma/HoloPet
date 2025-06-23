using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_InAir : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;

    private float floatSpeed;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform} ¡X no StateMachineBase found in parent.");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform} ¡X no basicSM found in parent.");
        }
    }

    public override void Enter()
    {
        floatSpeed = 0.5f;
    }

    public override void StateUpdate()
    {
        //fall
        basicSM.MovementMg.MoveUp(floatSpeed);

        if (basicSM.BoundaryMg.CheckIsTopBounderyAndResetPos())
        {
            //exit to idle
            stateMachine.ChangeState(basicSM.StateIdle);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
