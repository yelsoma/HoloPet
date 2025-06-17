using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CartState_Grab : StateBase
{
    [SerializeField] private CartStateMachine stateMachine;
    public event EventHandler OnGrabMounted;
    public event EventHandler OnGrabNormal;


    // < State Base >
    public override void Enter()
    {
        //event

        //start
        if (stateMachine.mountManager.GetIsMounted())
        {
            OnGrabMounted?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnGrabNormal?.Invoke(this, EventArgs.Empty);
        }
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
        //event
    }

    // < Events >
}
