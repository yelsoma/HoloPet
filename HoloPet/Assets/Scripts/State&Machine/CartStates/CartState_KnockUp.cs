using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartState_KnockUp : StateBase
{
    [SerializeField] private CartStateMachine stateMachine;
    private float knockUpPower;
    [SerializeField] private float knockUpDecrese;
    private float knockUpPowerNow;
    private bool knockUpRight;
    private float knockBackPower;
    private bool startFall;
    private float fallSpeedIncreese = 6.5f;
    private float fallSpeedMax = 9f;
    private float fallSpeedNow;
    private float knockUpFaceDir;
    // < State Base >
    public override void Enter()
    {
        //event
        stateMachine.mouseInput.OnDrag += MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;

        //start
        knockUpPower = UnityEngine.Random.Range(7f, 7f);
        knockBackPower = UnityEngine.Random.Range(0.7f, 2f);
        fallSpeedNow = 0f;
        startFall = false;
        knockUpPowerNow = knockUpPower;
        knockUpFaceDir = UnityEngine.Random.Range(1f, 0f);
        if (knockUpFaceDir <= 0.5)
        {
            knockUpRight = false;
            stateMachine.faceDirection.SetFaceLeft();
        }
        else
        {
            knockUpRight = true;
            stateMachine.faceDirection.SetFaceRight();
        }
    }
    public override void StateUpdate()
    {
        if (knockUpPowerNow >= 0f)
        {
            knockUpPowerNow -= knockUpDecrese * Time.deltaTime;
            if (stateMachine.boundaryManager.CheckIsTopBounderyAndResetPos())
            {
                knockUpPowerNow = -1f;
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
                //exit to idle
                stateMachine.ChangeState(stateMachine.stateIdle);
                return;
            }
        }
    }
    public override void StateLateUpdate()
    {
        if (!startFall)
        {
            stateMachine.movement.MoveUp(knockUpPowerNow);
        }
        if (startFall)
        {
            stateMachine.movement.MoveDown(fallSpeedNow);
        }
        if (knockUpRight)
        {
            if (stateMachine.boundaryManager.CheckIsRightBounderyAndResetPos())
            {
                knockUpRight = false;
            }
            stateMachine.movement.MoveRight(knockBackPower);
        }
        if (!knockUpRight)
        {
            if (stateMachine.boundaryManager.CheckIsLeftBounderyAndResetPos())
            {
                knockUpRight = true;
            }
            stateMachine.movement.MoveLeft(knockBackPower);
        }
    }
    public override void Exit()
    {
        //event
        stateMachine.mouseInput.OnDrag -= MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick -= MouseInput_OnClick;
    }

    // < Events >
    private void MouseInput_OnDrag(object sender, MouseInput.OnDragEventArgs e)
    {
        //exit to grab
        stateMachine.ChangeState(stateMachine.stateGrab);
        return;
    }
    private void MouseInput_OnClick(object sender, EventArgs e)
    {
        //exit to knockUp
        stateMachine.ChangeState(stateMachine.stateKnockUp);
        return;
    }
}
