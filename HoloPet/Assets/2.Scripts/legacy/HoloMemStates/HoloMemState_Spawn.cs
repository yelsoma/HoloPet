using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_Spawn : StateBase
{
    [SerializeField] private HoloMemStateMachine stateMachine;

    // < State Base >
    public override void Enter()
    {
        //cant do
        stateMachine.interactManager.SetIsInteractable(false);
        //stateMachine.mountManager.SetIsMountableState(false);


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
        stateMachine.interactManager.SetIsInteractable(true);
        //stateMachine.mountManager.SetIsMountableState(true);

        //event
    }

    // < Events >
}
