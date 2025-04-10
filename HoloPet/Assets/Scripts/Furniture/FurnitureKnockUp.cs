using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureKnockUp : StateBase
{
    [SerializeField] private FurnitureStateMachine stateMachine;
    private float knockUpPower;
    [SerializeField] private float knockUpDecrese;
    private float knockUpPowerNow;   
    private float knockUpFaceDir;
    private bool knockUpRight;
    private float knockBackPower;
    private bool startFall;
    private float fallSpeedIncreese = 5f;
    private float fallSpeedNow;   

    // < State Base >
    public override void Enter()
    {
        stateMachine.data.SetIsInteractableState(false);
        stateMachine.data.SetIsMountableState(false);
        knockUpPower = UnityEngine.Random.Range(7f, 7f);
        knockBackPower = UnityEngine.Random.Range(0.7f, 2f);
        fallSpeedNow = 0f;
        startFall = false;
        knockUpFaceDir = UnityEngine.Random.Range(1f, 0f);
        if (knockUpFaceDir <= 0.5)
        {
            knockUpRight = false;
        }
        else
        {
            knockUpRight = true;
        }
        knockUpPowerNow = knockUpPower;
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
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

            //keep jump          
        }
        else
        {
            startFall = true;
            fallSpeedNow += fallSpeedIncreese * Time.deltaTime;

            if (stateMachine.boundaryManager.CheckIsBotBoundery())
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
            if (stateMachine.boundaryManager.CheckIsRightBoundery())
            {
                knockUpRight = false;
            }
            stateMachine.movement.MoveRight(knockBackPower);
        }
        if (!knockUpRight)
        {
            if (stateMachine.boundaryManager.CheckIsLeftBoundery())
            {
                knockUpRight = true;
            }
            stateMachine.movement.MoveLeft(knockBackPower);
        }

    }
    public override void Exit()
    {
        stateMachine.data.SetIsInteractableState(true);
        stateMachine.data.SetIsMountableState(true);
        stateMachine.mouseInput.OnDrag -= Input_OnDrag;
        stateMachine.mouseInput.OnClick -= MouseInput_OnClick;
        stateMachine.interactManager.OnInteract -= InteractManager_OnInteract;
    }

    // < Events >
    private void Input_OnDrag(object sender, MouseInput.OnDragEventArgs e)
    {
        //exit to grab
        stateMachine.ChangeState(stateMachine.stateGrab);
        return;
    }
    private void MouseInput_OnClick(object sender, EventArgs e)
    {        
        stateMachine.ChangeState(stateMachine.stateKnockUp);
        return;
    }
    private void InteractManager_OnInteract(object sender, EventArgs e)
    {
        //exit to interacted
        stateMachine.ChangeState(stateMachine.stateInteracted);
        return;
    }
}
