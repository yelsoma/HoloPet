using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartState_DashMaxSpeed : StateBase
{

    [SerializeField] private CartStateMachine stateMachine;
    [SerializeField] private float wanderSpeed;
    private bool wanderRight;
    public override void Enter()
    {
        //event       

        //start
        wanderRight = stateMachine.faceDirection.GetIsFaceRight();
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
        //if (!stateMachine.mountManager.GetIsMounted())
        //{
        //    stateMachine.ChangeState(stateMachine.stateIdle);
        //}
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
    }
    public override void StateLateUpdate()
    {
        if (wanderRight)
        {
            stateMachine.movement.MoveRight(wanderSpeed);
        }
        else
        {
            stateMachine.movement.MoveLeft(wanderSpeed);
        }
    }
    public override void Exit()
    {
        //event
    }
    // < Events >
}
