using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_HappyChat : StateBase 
{
    [SerializeField] private HoloMemStateMachine stateMachine;
    [SerializeField] float happyChatTime;
    private bool targetExit;
    private float happyChatTimer;
    public override void Enter()
    {
        Debug.Log("enter happy");
        //can do
        stateMachine.interactManager.SetIsInteractable(false);

        //event     
        stateMachine.mouseInput.OnDrag += MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
        stateMachine.interactManager.OnTargetExitInteract += InteractManager_OnTargetExitInteract;

        //start
        targetExit = false;
        happyChatTimer = happyChatTime;
        stateMachine.mountManager.LayerChainUpStart();

        // check is there target , set face to target
        if (stateMachine.interactManager.GetTargetIInteractable() != null)
        {
            if (stateMachine.interactManager.GetIsTargetRight())
            {
                stateMachine.faceDirection.SetFaceRight();
            }
            else
            {
                stateMachine.faceDirection.SetFaceLeft();
            }
        }
        else
        {
            // exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
    }
    public override void StateUpdate()
    {

        if (IsInteractTimeUp() || targetExit)
        {
            Debug.Log("i left happytalk" + stateMachine);
            //exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
        happyChatTimer -= Time.deltaTime;
    }
    public override void StateLateUpdate()
    {

    }
    public override void Exit()
    {
        //tell target that i exit interact
        stateMachine.interactManager.GetTargetIInteractable().TargetExitInteract();
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
        targetExit = true;
        return;
    }

    // < Package Method >
    private bool IsInteractTimeUp()
    {
        if (happyChatTimer > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
