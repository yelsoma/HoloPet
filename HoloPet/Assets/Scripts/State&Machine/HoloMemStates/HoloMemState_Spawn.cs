using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_Spawn : StateBase
{
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
        stateMachine.interactManager.OnInteractedByInteracter += InteractManager_OnInteractedByInteracter;

        //start
        stateMachine.faceDirection.SetFaceRight();
    }

    public override void StateUpdate()
    {
        //exit to idle
        stateMachine.ChangeState(stateMachine.stateIdle);
        return;
    }
    public override void StateLateUpdate()
    {

    }
    public override void Exit()
    {
        //cant do
        stateMachine.interactManager.SetIsInteractable(true);
        stateMachine.mountManager.SetIsMountableState(true);

        //event
        stateMachine.mouseInput.OnDrag -= MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick -= MouseInput_OnClick;
        stateMachine.interactManager.OnInteractedByInteracter -= InteractManager_OnInteractedByInteracter;

    }

    // < Events >

    private void MouseInput_OnDrag(object sender, MouseInput.OnDragEventArgs e)
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
    private void InteractManager_OnInteractedByInteracter(object sender, EventArgs e)
    {
        // Exit to interact
        stateMachine.interactManager.OnInteractWithOption(stateMachine.interactManager.GetInteracter().GetChoosenOp());
        return;
    }
}
