using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_Interacted : StateBase
{
    [SerializeField] private HoloMemStateMachine stateMachine;
    public override void Enter()
    {
        //can do
        stateMachine.interactManager.SetIsInteractable(false);

        //event     
        stateMachine.mouseInput.OnDrag += MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
        stateMachine.interactManager.OnTargetExitInteract += InteractManager_OnTargetExitInteract;

        //start     

        // check is there target , set face to target
        if (stateMachine.interactManager.GetTargetIInteractable() == null)
        {
            // exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
        interactOption interactedOp = stateMachine.interactManager.GetInteractedOp();
        if ( interactedOp == interactOption.Bully)
        {
            stateMachine.ChangeState(stateMachine.stateBullied);
            return;
        }
        if(interactedOp == interactOption.happyChat)
        {
            stateMachine.ChangeState(stateMachine.stateHappyChat);
            return;
        }
        if(interactedOp == interactOption.sit)
        {
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
    }
    public override void StateUpdate()
    {
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
        // can do
        stateMachine.interactManager.SetIsInteractable(true);

        //event
        stateMachine.mouseInput.OnDrag -= MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick -= MouseInput_OnClick;
        stateMachine.interactManager.OnTargetExitInteract -= InteractManager_OnTargetExitInteract;
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
        //exit to knock up
        stateMachine.ChangeState(stateMachine.stateKnockUp);
        return;
    }
    private void InteractManager_OnTargetExitInteract(object sender, EventArgs e)
    {
        //target has ExitInteract
        stateMachine.ChangeState(stateMachine.stateIdle);
        return;
    }
}
