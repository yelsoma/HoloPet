using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_Wander : StateBase
{
    [SerializeField] private float wanderMaxTime;
    [SerializeField] private float wanderMinTime;
    [SerializeField] private float wanderSpeed;
    [SerializeField] private HoloMemStateMachine stateMachine;
    private float wanderTimer;
    private float randomDir;
    private bool wanderRight;


    // < State Base >
    public override void Enter()
    {
        //event

        //start
        wanderTimer = UnityEngine.Random.Range(wanderMinTime, wanderMaxTime);
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

        //side check
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
        //time check
        if (wanderTimer <= 0f)
        {
            //exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
        // keep  wander right
        wanderTimer -= Time.deltaTime;
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
    }

    // < Events >
}
