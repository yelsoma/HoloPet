using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartState_Jump : StateBase
{
    [SerializeField] private CartStateMachine stateMachine;
    [SerializeField] private float jumpUpPower;
    [SerializeField] private float jumpForthPower;
    [SerializeField] private float jumpFallDecrese;
    private float jumpUpPowerNow;
    private bool jumpUpRight;
    private bool startFall;
    private float fallSpeedIncreese = 6.5f;
    private float fallSpeedMax = 9f;
    private float fallSpeedNow;
    // < State Base >
    public override void Enter()
    {
        //event

        //start
        fallSpeedNow = 0f;
        startFall = false;
        jumpUpPowerNow = jumpUpPower;
        jumpUpRight = stateMachine.faceDirection.GetIsFaceRight();
    }
    public override void StateUpdate()
    {
        //if (!stateMachine.mountManager.GetIsMounted())
        //{
        //    //exit to fall
        //    stateMachine.ChangeState(stateMachine.stateFall);
        //    return;
        //}
        if (jumpUpPowerNow >= 0f)
        {
            jumpUpPowerNow -= jumpFallDecrese * Time.deltaTime;
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
                //exit to  dash maxSpeed
                stateMachine.ChangeState(stateMachine.stateDashMaxSpeed);
                return;
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
        if (jumpUpRight)
        {
            if (stateMachine.boundaryManager.CheckIsRightBounderyAndResetPos())
            {
                stateMachine.faceDirection.SetFaceLeft();
                jumpUpRight = false;
            }
            stateMachine.movement.MoveRight(jumpForthPower);
        }
        if (!jumpUpRight)
        {
            if (stateMachine.boundaryManager.CheckIsLeftBounderyAndResetPos())
            {
                stateMachine.faceDirection.SetFaceRight();
                jumpUpRight = true;
            }
            stateMachine.movement.MoveLeft(jumpForthPower);
        }
    }
    public override void Exit()
    {
        //event
    }

    // < Events >
}
