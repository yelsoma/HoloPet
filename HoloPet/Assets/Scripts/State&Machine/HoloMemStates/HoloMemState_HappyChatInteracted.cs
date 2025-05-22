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
    private float fallSpeedIncreese = 6.5f;
    private float fallSpeedMax = 9f;
    private float fallSpeedNow;
    private float jumpCountLeft;
    [SerializeField] private float startJumpDelay;
    private Coroutine jumpCoroutine;

    public override void Enter()
    {        
        //can do
        stateMachine.interactManager.SetIsInteractable(false);

        //event     
        stateMachine.interactManager.GetInteracter().OnExitInteracting += HoloMemState_HappyChatInteracted_OnExitInteracting;
        //start

        
        // check is there target , set face to target
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
            jumpCoroutine = StartCoroutine(CoJumpStart());
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
    }
    public override void StateLateUpdate()
    {     
    }

    public override void Exit()
    {
        StopCoroutine(jumpCoroutine);
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

    //corutine
    private IEnumerator CoJumpStart()
    {
        yield return new WaitForSeconds(startJumpDelay);
        jumpCountLeft = jumpCount;
        jumpUpPowerNow = jumpUpPower;
        fallSpeedNow = 0f;
        OnStartJump?.Invoke(this, EventArgs.Empty);
        while (jumpCountLeft > 0)
        {
            if(jumpUpPowerNow > 0)
            {
                stateMachine.movement.MoveUp(jumpUpPowerNow);
                jumpUpPowerNow -= jumpUpDecrese * Time.deltaTime;
            }
            else
            {
                stateMachine.movement.MoveDown(fallSpeedNow);
                if(fallSpeedNow <= fallSpeedMax)
                {
                    fallSpeedNow += fallSpeedIncreese * Time.deltaTime;
                }                
                if (stateMachine.boundaryManager.CheckIsBotBounderyAndResetPos())
                {
                    jumpCountLeft--;
                    jumpUpPowerNow = jumpUpPower;
                    fallSpeedNow = 0f;
                }
            }
            yield return null;
        }
        stateMachine.ChangeState(stateMachine.stateIdle);
    }
}

