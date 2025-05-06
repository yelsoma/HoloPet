using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairState_Idle : StateBase
{
    [SerializeField] private ChairStateMachine stateMachine;
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
