using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolHappyTalk : StateBase
{
    
    [SerializeField] private IdolStateMachine stateMachine;
    [SerializeField] private float happyAniTime;
    private float happyAniTimeNow;
    public event EventHandler OnInteractHappy;


    public override void Enter()
    {
        // cant do
        stateMachine.data.SetIsInteractableState(true);
        stateMachine.data.SetIsMountableState(true);

        //event
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
        stateMachine.interactManager.OnInteract += InteractManager_OnInteract;

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
         if (stateMachine.data.GetHaveHp())
         {
             if (stateMachine.data.GetHpNow() > 0)
             {
                 stateMachine.data.HpModify(-1);
             }
             if (stateMachine.data.GetHpNow() <= 0)
             {
                 stateMachine.ChangeState(stateMachine.stateRage);
                 return;
             }
         }   
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
