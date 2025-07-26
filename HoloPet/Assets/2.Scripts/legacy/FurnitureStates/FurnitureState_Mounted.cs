using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureState_Mounted : StateBase
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
