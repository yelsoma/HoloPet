using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanataRage : StateBase
{
    [SerializeField] private IdolStateMachine stateMachine;
    //Fall
    [SerializeField] float fallSpeedIncreese;
    private float fallSpeedNow;
    //OnPunchAni
    [SerializeField] float punchAniTime;
    private float aniTimeNow;
    public event EventHandler OnPunch;
    private bool eventOnPunchCalled;
    //knockUp
    [SerializeField] private float knockUpPower;
    private float knockUpPowerNow;
    [SerializeField] private float knockUpDecrese;


    // < State Base >
    public override void Enter()
    {
        stateMachine.data.SetIsInteractableState(false);
        stateMachine.data.SetIsMountableState(false);
        fallSpeedNow = 0f;
        aniTimeNow = punchAniTime;
        eventOnPunchCalled = false;
        knockUpPowerNow = knockUpPower;
    }

    public override void StateUpdate()
    {
        if (knockUpPowerNow >= 0f)
        {
            knockUpPowerNow -= knockUpDecrese * Time.deltaTime;
            if (stateMachine.boundaryManager.CheckIsTopBoundery())
            {
                knockUpPowerNow = -1f;
            }
            stateMachine.movement.MoveUp(knockUpPowerNow);
            //keep jump          
        }
        if (knockUpPowerNow < 0f)
        {

            fallSpeedNow += fallSpeedIncreese * Time.deltaTime;

            stateMachine.movement.MoveDown(fallSpeedNow);
            //keep fall
        }
        if (stateMachine.boundaryManager.CheckIsBotBoundery())
        {
            //punch ani
            if (eventOnPunchCalled == false)
            {
                OnPunch?.Invoke(this, EventArgs.Empty);
                eventOnPunchCalled = true;
            }
            aniTimeNow -= Time.deltaTime;
            if (aniTimeNow <= 0f)
            {
                //exit to idle
                stateMachine.ChangeState(stateMachine.stateIdle);
                return;
            }
        }
    }
    public override void StateLateUpdate()
    {
        //update in logic

    }
    public override void Exit()
    {

    }

    // < Events >
}
