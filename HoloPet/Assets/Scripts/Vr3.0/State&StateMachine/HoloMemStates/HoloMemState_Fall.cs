using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_Fall : StateBase
{
    [SerializeField] float fallSpeedIncreese;
    [SerializeField] float fallSpeedMax;
    private float fallSpeedNow;
    [SerializeField] private HoloMemStateMachine stateMachine;

    // < State Base >
    public override void Enter()
    {
        //cant do       
        stateMachine.interactManager.SetIsInteractable(false);      
        stateMachine.mountManager.SetIsMountableState(false); 

        //event
        stateMachine.mouseInput.OnDrag += MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
        

        //start
        fallSpeedNow = 0f;
    }

    

    public override void StateUpdate()
    {
        if (!stateMachine.boundaryManager.CheckIsBotBounderyAndResetPos() && fallSpeedNow < fallSpeedMax)
        {

            fallSpeedNow += fallSpeedIncreese * Time.deltaTime;
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
        stateMachine.mouseInput.OnDrag -= MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick -= MouseInput_OnClick;        
    }

    // < Events >
    private void MouseInput_OnDrag(object sender, MouseInputVr2.OnDragEventArgs e)
    {
        //exit to grab
        stateMachine.ChangeState(stateMachine.stateGrab);
        return;
    }
    private void MouseInput_OnClick(object sender, EventArgs e)
    {
        //exit to knockUp
        stateMachine.ChangeState(stateMachine.stateKnockUp);
        return;
    }   
}
