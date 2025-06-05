using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FurnitureState_Grab : StateBase
{
    [SerializeField] private FurnitureStateMachine stateMachine;
    public event EventHandler OnLeaveGrab;


    // < State Base >
    public override void Enter()
    {
        //event

        //start
       // stateMachine.layerManager.PullToTop();
       // LayerCenter.ResetAllLayer();
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
        OnLeaveGrab?.Invoke(this, EventArgs.Empty);
        //event
    }

    // < Events >
}
