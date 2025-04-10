using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolRage : StateBase
{
    [SerializeField] private IdolStateMachine stateMachine;
    [SerializeField] float fallSpeedIncreese;
    private float fallSpeedNow;  
    [SerializeField] float aniTime;
    private float aniTimeNow;
    public event EventHandler OnPunch;
    private bool eventOnRageCalled;
    [SerializeField] private float knockUpPower;
    private float knockUpPowerNow;
    [SerializeField] private float knockUpDecrese;
    

    // < State Base >
    public override void Enter()
    {
        //cant do
        stateMachine.data.SetIsInteractableState(false);
        stateMachine.data.SetIsMountableState(false);

        //start
        fallSpeedNow = 0f;
        aniTimeNow = aniTime;
        eventOnRageCalled = false;
        knockUpPowerNow = knockUpPower;

        //event
        stateMachine.interactManager.OnInteract += InteractManager_OnInteract;
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
            //breack ani
            if (eventOnRageCalled == false)
            {
                OnPunch?.Invoke(this, EventArgs.Empty);
                eventOnRageCalled = true;
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
        stateMachine.data.SetIsInteractableState(true);
        stateMachine.data.SetIsMountableState(true);

        //event
        stateMachine.interactManager.OnInteract -= InteractManager_OnInteract;
    }

    // < Events >

    private void InteractManager_OnInteract(object sender, EventArgs e)
    {
        //exit to interacted
        stateMachine.ChangeState(stateMachine.stateInteracted);
        return;
    }
}
