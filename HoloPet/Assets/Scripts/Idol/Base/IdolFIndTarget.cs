using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolFIndTarget : StateBase
{
    [SerializeField] private IdolStateMachine stateMachine;
    //wander
    [SerializeField] private float wanderSpeed;  
    public event EventHandler OnFindTarget;
    private bool walkAniPlayed;
    //happy
    [SerializeField] private float happyAniTime;
    private float happyAniTimeNow;
    public event EventHandler OnInteractHappy;
    private bool happyAniPlayed;


    // < State Base >
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
        walkAniPlayed = false;
        happyAniPlayed = false;
        happyAniTimeNow = 0f;       
    }
    public override void StateUpdate()
    {

        if (!stateMachine.boundaryManager.CheckIsBotBoundery())
        {
            //exit to fall
            stateMachine.ChangeState(stateMachine.stateFall);
            return;
        }
        //target is still interactable
        if (stateMachine.interactManager.GetTargetIsInteractableState() && stateMachine.interactManager.GetTargetIsInteracting() == false)
        {
            if (walkAniPlayed == false)
            {
                OnFindTarget?.Invoke(this, EventArgs.Empty);
                walkAniPlayed = true;
            }
            // distance
            if (stateMachine.interactManager.GetIsTargetClose() == false)
            {
                
                if (stateMachine.interactManager.GetIsTargetRight())
                {
                    // target is right
                    stateMachine.data.SetIsFaceRight(true);
                    stateMachine.movement.MoveRight(wanderSpeed);
                }
                else
                {
                    // target is left
                    stateMachine.data.SetIsFaceRight(false);
                    stateMachine.movement.MoveLeft(wanderSpeed);
                }
            }
            if (stateMachine.interactManager.GetIsTargetTooClose())
            {

                if (stateMachine.interactManager.GetIsTargetRight())
                {
                    // target is right
                    stateMachine.data.SetIsFaceRight(false);
                    stateMachine.movement.MoveLeft(wanderSpeed);
                }
                else
                {
                    // target is left                   
                    stateMachine.data.SetIsFaceRight(true);
                    stateMachine.movement.MoveRight(wanderSpeed);
                }
            }
            if (stateMachine.interactManager.GetIsTargetClose() == true && stateMachine.interactManager.GetIsTargetTooClose() == false)
            {

                //start Happy
                stateMachine.data.SetIsInteracting(true);
                stateMachine.interactManager.SetTagetInteracted();
                stateMachine.interactManager.SetTargetHappy();
                stateMachine.mountManager.LayerModifyUp();
                if (happyAniPlayed == false)
                {
                    OnInteractHappy?.Invoke(this,EventArgs.Empty);
                    happyAniPlayed = true;
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
        }
        else
        {
            //enter idle
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
        //exit to KnockUp
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
