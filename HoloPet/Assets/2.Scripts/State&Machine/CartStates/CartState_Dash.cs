using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartState_Dash : StateBase
{
    [SerializeField] private CartStateMachine stateMachine;
    [SerializeField] private float wanderSpeedMax;
    [SerializeField] private float waderSpeedIncrease;
    private float wanderSpeedNow;
    private float randomDir;
    private bool wanderRight;
    public override void Enter()
    {
        //event       

        //start
        wanderSpeedNow = 0f;
        randomDir = UnityEngine.Random.Range(1f, 0f);
        if (randomDir >= 0.5)
        {
            wanderRight = true;
            stateMachine.faceDirection.SetFaceRight();
        }
        else
        {
            wanderRight = false;
            stateMachine.faceDirection.SetFaceLeft();
        }
    }
    public override void StateUpdate()
    {
        //ground check
        if (!stateMachine.boundaryManager.CheckIsBotBounderyAndResetPos())
        {
            //exit to fall
            stateMachine.ChangeState(stateMachine.stateFall);
            return;
        }
        if (!stateMachine.mountManager.GetIsMounted())
        {
            stateMachine.ChangeState(stateMachine.stateIdle);
        }
        //side ckeck
        if (stateMachine.boundaryManager.CheckIsLeftBounderyAndResetPos())
        {
            wanderRight = true;
            stateMachine.faceDirection.SetFaceRight();
        }
        if (stateMachine.boundaryManager.CheckIsRightBounderyAndResetPos())
        {

            wanderRight = false;
            stateMachine.faceDirection.SetFaceLeft();
        }
        //speed up
        if(wanderSpeedMax > wanderSpeedNow)
        {
            wanderSpeedNow += waderSpeedIncrease* Time.deltaTime;
        }
        else
        {
            stateMachine.ChangeState(stateMachine.stateDashMaxSpeed);
        }
    }
    public override void StateLateUpdate()
    {
        if (wanderRight)
        {
            stateMachine.movement.MoveRight(wanderSpeedNow);
        }
        else
        {
            stateMachine.movement.MoveLeft(wanderSpeedNow);
        }
    }
    public override void Exit()
    {
        //event
    }
    // < Events >
}
