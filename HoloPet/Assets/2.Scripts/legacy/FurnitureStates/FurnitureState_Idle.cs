using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureState_Idle : StateBase
{
    [SerializeField] private FurnitureStateMachine stateMachine;
    public override void Enter()
    {
        //event       

        //start
        /*
        stateMachine.data.SetHpToMax();
        */
    }
    public override void StateUpdate()
    {
        if (!stateMachine.boundaryManager.CheckIsBotBounderyAndResetPos())
        {
            //exit to fall
            stateMachine.ChangeState(stateMachine.stateFall);
            return;
        }
        if (stateMachine.mountManager.GetIsMounted())
        {
            //exit to mounted
            stateMachine.ChangeState(stateMachine.stateMounted);
        }
    }
    public override void StateLateUpdate()
    {
        //keep idle
    }
    public override void Exit()
    {
        //event

    }
    // < Events >
}
