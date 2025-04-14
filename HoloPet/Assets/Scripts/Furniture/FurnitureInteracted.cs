using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureInteracted : StateBase
{
    [SerializeField] private FurnitureStateMachineOld stateMachine;
    private bool interacterWantKnockUp;

    // < State Base >
    public override void Enter()
    {
        //Event
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
        stateMachine.interactManager.OnInteract += InteractManager_OnInteract;
        stateMachine.interactManager.OnTargetKnockUp += InteractManager_OnTargetKnockUp;

        //start
        stateMachine.mountManager.LayerModifyUp();
        stateMachine.data.SetIsInteracting(true);
    }
    public override void StateUpdate()
    {
        if (!stateMachine.boundaryManager.CheckIsBotBoundery())
        {
            stateMachine.ChangeState(stateMachine.stateFall);
            return;
        }
    }
    public override void StateLateUpdate()
    {

    }
    public override void Exit()
    {
        stateMachine.data.SetIsInteracting(false);

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
        //exit to knock up
        stateMachine.ChangeState(stateMachine.stateKnockUp);
        return;
    }
    private void InteractManager_OnInteract(object sender, EventArgs e)
    {
        //exit to interacted
        stateMachine.ChangeState(stateMachine.stateInteracted);
        return;
    }
    private void InteractManager_OnTargetKnockUp(object sender, EventArgs e)
    {
        //exit to knock Up
        stateMachine.ChangeState(stateMachine.stateKnockUp);
        return;
    }   
}
