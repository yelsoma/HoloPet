using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_HappyChat : StateBase 
{
    [SerializeField] private HoloMemStateMachine stateMachine;
    //jump
    [SerializeField] private float jumpUpPower;
    [SerializeField] private float jumpUpDecrese;
    [SerializeField] private int jumpCount;
    private float jumpUpPowerNow;
    private bool startFall;
    private float fallSpeedIncreese = 6.5f;
    private float fallSpeedMax = 9f;
    private float fallSpeedNow;
    private float jumpCountLeft;
    public override void Enter()
    {
        //can do
        stateMachine.interactManager.SetIsInteractable(false);

        //event     
        stateMachine.interactManager.GetTargetIInteractable().OnExitInteracted += HoloMemState_HappyChat_OnExitInteracted;

        //start
        fallSpeedNow = 0f;
        startFall = false;
        jumpUpPowerNow = jumpUpPower;
        jumpCountLeft = jumpCount;

        // check is there target , set face to target ,and set target to child
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
            //stateMachine.interactManager.GetBothInteractOption().GetInteractedOption.
            //stateMachine.interactManager.GetTargetIInteractable().GetTransform().SetParent(stateMachine.transform);
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
        if (jumpCountLeft <= 0)
        {
            stateMachine.boundaryManager.SetToBotBoundary();
            //exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
        if (jumpUpPowerNow >= 0f)
        {
            jumpUpPowerNow -= jumpUpDecrese * Time.deltaTime;
            if (stateMachine.boundaryManager.CheckIsTopBounderyAndResetPos())
            {
                jumpUpPowerNow = -1f;
            }
            //keep jump          
        }
        else
        {
            startFall = true;
            //max fall speed
            if (fallSpeedNow <= fallSpeedMax)
            {
                fallSpeedNow += fallSpeedIncreese * Time.deltaTime;
            }
            if (stateMachine.boundaryManager.CheckIsBotBounderyAndResetPos())
            {
                jumpCountLeft -= 1;
                jumpUpPowerNow = jumpUpPower;
                fallSpeedNow = 0;
                startFall = false;
            }
        }
    }
    public override void StateLateUpdate()
    {
        if (!startFall)
        {
            stateMachine.movement.MoveUp(jumpUpPowerNow);
        }
        if (startFall)
        {
            stateMachine.movement.MoveDown(fallSpeedNow);
        }
    }
    public override void Exit()
    {
        // set target parant to null
        //stateMachine.interactManager.GetTargetIInteractable().GetTransform().SetParent(null);
        //tell target that i exit interact
        stateMachine.interactManager.ExitInteractingEvent();
        // can do
        stateMachine.interactManager.SetIsInteractable(true);

        //event
        stateMachine.interactManager.GetTargetIInteractable().OnExitInteracted -= HoloMemState_HappyChat_OnExitInteracted;
    }

    // < Events >
    private void HoloMemState_HappyChat_OnExitInteracted(object sender, EventArgs e)
    {
        jumpCountLeft = 1;
    }
}
