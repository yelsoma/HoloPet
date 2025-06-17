using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_Fall : StateBase
{
    private float fallSpeedNow;
    [SerializeField] private float fallSpeedIncrease;
    [SerializeField] private float fallSpeedMax;
    [SerializeField] private HoloMemStateMachine stateMachine;

    // < State Base >
    public override void Enter()
    {
        //cant do       
        stateMachine.interactManager.SetIsInteractable(false);      
        stateMachine.mountManager.SetIsMountableState(false);

        //event

        //start
        fallSpeedNow = 0f;
    }

    

    public override void StateUpdate()
    {
        if (!stateMachine.boundaryManager.CheckIsBotBounderyAndResetPos() && fallSpeedNow < fallSpeedMax)
        {

            fallSpeedNow += fallSpeedIncrease * Time.deltaTime;
            //keep fall
        }     
    }
    public override void StateLateUpdate()
    {
        stateMachine.movement.MoveDown(fallSpeedNow);

        if (stateMachine.boundaryManager.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.boundaryManager.CheckAllBouderyAndResetPos();
            //exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
        
    }
    public override void Exit()
    {
        //cant do 
        stateMachine.interactManager.SetIsInteractable(true);
        stateMachine.mountManager.SetIsMountableState(true);
        //event    
    }

    // < Events >
}
