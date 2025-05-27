using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairState_Grab : StateBase
{
    [SerializeField] private ChairStateMachine stateMachine;

    // < State Base >
    public override void Enter()
    {
        //event

        //start
        stateMachine.interactManager.SetIsInteractable(false);
    }

    public override void StateUpdate()
    {
        // logic in event
    }
    public override void StateLateUpdate()
    {
        stateMachine.boundaryManager.CheckAllBouderyAndResetPos();
    }
    public override void Exit()
    {
        stateMachine.interactManager.SetIsInteractable(true);
        //event
    }

    // < Events >
}
