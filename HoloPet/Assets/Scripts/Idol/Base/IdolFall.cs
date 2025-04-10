using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolFall : StateBase
{
    [SerializeField] float fallSpeedIncreese;
    private float fallSpeedNow;
    [SerializeField] private IdolStateMachine stateMachine;  

    // < State Base >
    public override void Enter()
    {
        //cant do
        stateMachine.data.SetIsInteractableState(false);
        stateMachine.data.SetIsMountableState(false);
        
        //event
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
        stateMachine.interactManager.OnInteract += InteractManager_OnInteract;

        //start
        fallSpeedNow = 0f;
    }

    public override void StateUpdate()
    {
        if (!stateMachine.boundaryManager.CheckIsBotBoundery())
        {

            fallSpeedNow += fallSpeedIncreese * Time.deltaTime;
            //keep fall
        }
        if (stateMachine.boundaryManager.CheckIsBotBoundery())
        {
            //exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
    }
    public override void StateLateUpdate()
    {
        stateMachine.movement.MoveDown(fallSpeedNow);
    }
    public override void Exit()
    {
        stateMachine.data.SetIsInteractableState(true);
        stateMachine.data.SetIsMountableState(true);
        //event
        stateMachine.mouseInput.OnDrag -= Input_OnDrag;
        stateMachine.mouseInput.OnClick -= MouseInput_OnClick;
        stateMachine.interactManager.OnInteract -= InteractManager_OnInteract;
    }

    // < Events >
    private void Input_OnDrag(object sender, MouseInput.OnDragEventArgs e)
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
    private void InteractManager_OnInteract(object sender, EventArgs e)
    {
        //exit to interacted
        stateMachine.ChangeState(stateMachine.stateInteracted);
        return;
    }
}
