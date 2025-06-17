using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_Think : StateBase
{
    [SerializeField] private HoloMemStateMachine stateMachine;

    // < State Base >
    public override void Enter()
    {
        //cant do

        //event

        //start

        if (!stateMachine.raycastManager.TrySetRaycastBothSide(10))
        {
            //exit to find nothing
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
        if (!stateMachine.interactManager.TrySetTargetWihtRaycastHits(stateMachine.raycastManager.GetRaycastHits()))
        {
            //exit to to find nothing
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }

        //interact option
        if (stateMachine.interactManager.TryMatchOptionsChooseWithBothChance())
        {
            //exit to to follow target
            stateMachine.ChangeState(stateMachine.stateFollowTarget);
            return;
        }

        //exit to to idle
        stateMachine.ChangeState(stateMachine.stateIdle);
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
        //event
    }

    // < Events >
}
