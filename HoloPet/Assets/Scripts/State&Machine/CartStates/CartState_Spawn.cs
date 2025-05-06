using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartState_Spawn : StateBase
{
    [SerializeField] private CartStateMachine stateMachine;
    // < State Base >
    public override void Enter()
    {
        //cant do
        stateMachine.mountManager.SetIsMountableState(false);
        stateMachine.interactManager.SetIsInteractable(false);
        //event

        //start
        stateMachine.faceDirection.SetFaceRight();
    }

    public override void StateUpdate()
    {
        //exit to idle
        stateMachine.ChangeState(stateMachine.stateIdle);
        return;
    }
    public override void StateLateUpdate()
    {

    }
    public override void Exit()
    {
        //cant do
        stateMachine.mountManager.SetIsMountableState(true);
        stateMachine.interactManager.SetIsInteractable(true);
        //event
    }

    // < Events >
}
