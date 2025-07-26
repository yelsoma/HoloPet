using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_Idle : StateBase
{
    [SerializeField] private float idleTimeMax;
    [SerializeField] private float idleTimeMin;
    [SerializeField] private HoloMemStateMachine stateMachine;
    private float idleTimer;

    // < State Base >
    public override void Enter()
    {

        //event       

        //start
        /*
        stateMachine.data.SetHpToMax();
        */
        idleTimer = UnityEngine.Random.Range(idleTimeMin, idleTimeMax);
    }

    

    public override void StateUpdate()
    {
        if (!stateMachine.boundaryManager.CheckIsBotBounderyAndResetPos())
        {
            //exit to fall
            stateMachine.ChangeState(stateMachine.stateFall);
            return;
        }
        if (IsIdleTimeUp())
        {
            //exit to choose random  
            stateMachine.ChangeState(stateMachine.stateChooseARandom);
            return;
        }
        idleTimer -= Time.deltaTime;
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

    // < Package Method >
    private bool IsIdleTimeUp()
    {
        if (idleTimer > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
}
