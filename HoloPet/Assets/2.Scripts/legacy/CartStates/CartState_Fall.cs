using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartState_Fall : StateBase
{
    [SerializeField] private CartStateMachine stateMachine;
    [SerializeField] float fallSpeedIncreese;
    [SerializeField] float fallSpeedMax;
    private float fallSpeedNow;
    public event EventHandler OnFallMounted;
    public event EventHandler OnFallNormal;
    // < State Base >
    public override void Enter()
    {
        //event

        //start
        fallSpeedNow = 0f;
        if (stateMachine.mountManager.GetIsMounted())
        {
            OnFallMounted?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnFallNormal?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void StateUpdate()
    {

        if (!stateMachine.boundaryManager.CheckIsBotBounderyAndResetPos() && fallSpeedNow < fallSpeedMax)
        {

            fallSpeedNow += fallSpeedIncreese * Time.deltaTime;
            //keep fall
        }
    }
    public override void StateLateUpdate()
    {
        stateMachine.movement.MoveDown(fallSpeedNow);

        if (stateMachine.boundaryManager.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.boundaryManager.CheckAllBouderyAndResetPos();
            //exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
    }
    public override void Exit()
    {
        //event
    }

    // < Events >
}
