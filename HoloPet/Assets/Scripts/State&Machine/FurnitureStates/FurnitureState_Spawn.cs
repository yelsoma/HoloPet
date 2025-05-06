using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureState_Spawn : StateBase
{
    [SerializeField] private FurnitureStateMachine stateMachine;
    // < State Base >
    public override void Enter()
    {
        //cant do
        stateMachine.mountManager.SetIsMountableState(false);

        //event

        //start
        stateMachine.faceDirection.SetFaceRight();
        stateMachine.layerManager.SetNewLayer();
        LayerCenter.ResetAllLayer();
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

        //event
    }

    // < Events >
}
