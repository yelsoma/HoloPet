using System;
using System.Collections;
using UnityEngine;

public class HoloMemState_HappyChat : StateBase 
{
    [SerializeField] private HoloMemStateMachine stateMachine;
    //jump
    [SerializeField] private float jumpUpPower;
    [SerializeField] private float jumpUpDecrese;
    [SerializeField] private int jumpCount;
    private float jumpUpPowerNow;
    private float fallSpeedIncreese = 6.5f;
    private float fallSpeedMax = 9f;
    private float fallSpeedNow;
    private float jumpCountLeft;
    private Coroutine jumpCoroutine;
    public override void Enter()
    {
        //can do
        stateMachine.interactManager.SetIsInteractable(false);

        //event     
        stateMachine.interactManager.GetTargetIInteractable().OnExitInteracted += HoloMemState_HappyChat_OnExitInteracted;

        //start

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
            //start jump
            jumpCoroutine = StartCoroutine(CoStartJump());
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

    //coroutine
    private IEnumerator CoStartJump()
    {
        jumpCountLeft = jumpCount;
        jumpUpPowerNow = jumpUpPower;
        fallSpeedNow = 0f;
        while (jumpCountLeft > 0)
        {
            if (jumpUpPowerNow > 0)
            {
                stateMachine.movement.MoveUp(jumpUpPowerNow);
                jumpUpPowerNow -= jumpUpDecrese * Time.deltaTime;
            }
            else
            {
                stateMachine.movement.MoveDown(fallSpeedNow);
                if (fallSpeedNow <= fallSpeedMax)
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
