using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_HappyChatInteracted : StateBase
{
    public event EventHandler OnStartJump;
    [SerializeField] private HoloMemStateMachine stateMachine;
    [SerializeField] private float jumpUpPower;
    [SerializeField] private float jumpUpDecrese;
    [SerializeField] private int jumpCount;
    private float jumpUpPowerNow;
    private bool startFall;
    private float fallSpeedIncreese = 6.5f;
    private float fallSpeedMax = 9f;
    private float fallSpeedNow;
    private float jumpCountLeft;
    [SerializeField] private float startJumpDelay;
    private float startJumpDelayNow;
    private bool startJump;

    public override void Enter()
    {        
        //can do
        stateMachine.interactManager.SetIsInteractable(false);

        //event     
        stateMachine.interactManager.GetInteracter().OnExitInteracting += HoloMemState_HappyChatInteracted_OnExitInteracting;
        //start
        startJump = false;
        fallSpeedNow = 0f;
        startFall = false;
        jumpUpPowerNow = jumpUpPower;
        jumpCountLeft = jumpCount;
        startJumpDelayNow = startJumpDelay;
        
        // check is there target , set face to target ,and set interacter to parent
        if (stateMachine.interactManager.GetInteracter() != null)
        {
            if (stateMachine.interactManager.GetIsInteracterRight())
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
        if(startJumpDelayNow >= 0 && startJump == false)
        {
            startJumpDelayNow -= Time.deltaTime;
        }
        else
        {
            startJump = true;
            OnStartJump?.Invoke(this, EventArgs.Empty);
        }
        if (startJump)
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
    }
    public override void StateLateUpdate()
    {
        if (startJump)
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
    }

    public override void Exit()
    {
        //tell interacter that i exit interact
        stateMachine.interactManager.ExitInteractedEvent();
        // can do
        stateMachine.interactManager.SetIsInteractable(true);

        //event
        stateMachine.interactManager.GetInteracter().OnExitInteracting -= HoloMemState_HappyChatInteracted_OnExitInteracting;
    }

    // < Events >
    private void HoloMemState_HappyChatInteracted_OnExitInteracting(object sender, EventArgs e)
    {
        jumpCountLeft = 1;
    }
}

