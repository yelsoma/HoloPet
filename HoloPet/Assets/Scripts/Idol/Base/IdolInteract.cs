using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolInteract : StateBase
{
    [SerializeField] private IdolStateMachine stateMachine;
    [SerializeField] private float happyAniTime;
    private float happyAniTimeNow;
    private bool aniIsTriggered;
    public event EventHandler OnInteractHappy;

    // < State Base >
    public override void Enter()
    {   
        //event
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
        stateMachine.interactManager.OnInteract += InteractManager_OnInteract;


        //start
        aniIsTriggered = false;
        happyAniTimeNow = 0f;
        stateMachine.interactManager.SetTagetInteracted();


        //set target happy
        stateMachine.interactManager.SetTargetHappy();

        stateMachine.data.SetIsInteracting(true);
        stateMachine.mountManager.LayerModifyUp();       
        if (stateMachine.interactManager.GetIsTargetRight())
        {
            // target is right
            stateMachine.data.SetIsFaceRight(true);           
        }
        else
        {
            stateMachine.data.SetIsFaceRight(false);
        }        
    }
    public override void StateUpdate()
    {
        if (!stateMachine.boundaryManager.CheckIsBotBoundery())
        {
            //exit to fall
            stateMachine.ChangeState(stateMachine.stateFall);
            return;
        }
        if (!stateMachine.interactManager.GetTargetIsInteractableState())
        {
            //exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
        if (stateMachine.interactManager.GetTargetIsInteracting())
        {
            //exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
        if (aniIsTriggered == false)
        {
            OnInteractHappy?.Invoke(this, EventArgs.Empty);
            aniIsTriggered = true;
        }
        if(happyAniTimeNow < happyAniTime)
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
    public override void StateLateUpdate()
    {
       

    }
    public override void Exit()
    {
        stateMachine.data.SetIsInteracting(false);
        //Event
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
