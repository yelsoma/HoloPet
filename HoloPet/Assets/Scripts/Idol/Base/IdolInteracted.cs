using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolInteracted : StateBase
{

    [SerializeField] private IdolStateMachine stateMachine;
    [SerializeField] private float happyAniTime;
    private float happyAniTimeNow;
    private bool interacterWantHappy;
    public event EventHandler OnInteractHappy;

    // < State Base >
    public override void Enter()
    { 
        //Event
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
        stateMachine.interactManager.OnInteract += InteractManager_OnInteract;
        stateMachine.interactManager.OnTargetHappy += InteractManager_OnTargetHappy;
        stateMachine.interactManager.OnTargetKnockUp += InteractManager_OnTargetKnockUp;
        //start
        stateMachine.mountManager.LayerModifyUp();
        happyAniTimeNow = 0f;
        interacterWantHappy = false;
        stateMachine.data.SetIsInteracting(true);
        if (stateMachine.interactManager.GetIsInteracterRight())
        {
            // target is right
            stateMachine.data.SetIsFaceRight(true);
        }
        else
        {
            stateMachine.data.SetIsFaceRight(false);
        }
    }

    private void InteractManager_OnTargetKnockUp(object sender, EventArgs e)
    {
        //exit to knock Up
        stateMachine.ChangeState(stateMachine.stateKnockUp);
        return;
    }

    private void InteractManager_OnTargetHappy(object sender, EventArgs e)
    {
        interacterWantHappy = true;
        OnInteractHappy?.Invoke(this, EventArgs.Empty);
    }

    public override void StateUpdate()
    {
        if (!stateMachine.boundaryManager.CheckIsBotBoundery())
        {
            stateMachine.ChangeState(stateMachine.stateFall);
            return;
        }
        if (interacterWantHappy)
        {
            if (happyAniTimeNow < happyAniTime)
            {
                happyAniTimeNow += Time.deltaTime;
            }
            else
            {
                //exit to idle
                stateMachine.ChangeState(stateMachine.stateIdle);
                return;
            }
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
}
