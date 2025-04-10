using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolKnockup : StateBase
{
    private float knockUpPower;
    [SerializeField] private float knockUpDecrese;
    private float knockUpPowerNow;
    [SerializeField] private IdolStateMachine stateMachine;
    private float KnockUpFaceDir;
    private bool knockUpRight;
    private float knockBackPower;
    private bool startFall;
    private float fallSpeedIncreese = 5f;
    private float fallSpeedNow;
   
    public override void Enter()
    {
        // cant do
        stateMachine.data.SetIsInteractableState(false);
        stateMachine.data.SetIsMountableState(false);

        //event
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
        stateMachine.interactManager.OnInteract += InteractManager_OnInteract;

        //start
        stateMachine.layerManager.ChangeAllLayer();
        knockUpPower = UnityEngine.Random.Range(7f, 7f);
        knockBackPower = UnityEngine.Random.Range(0.7f, 2f);
        fallSpeedNow = 0f;
        startFall = false;
        KnockUpFaceDir = UnityEngine.Random.Range(1f, 0f);
        if (KnockUpFaceDir <= 0.5)
        {
            knockUpRight = false;
            stateMachine.data.SetIsFaceRight(true);
        }
        else
        {
            knockUpRight = true;
            stateMachine.data.SetIsFaceRight(false);
        }
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

        //event
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
         if (stateMachine.data.GetHaveHp())
         {
             if (stateMachine.data.GetHpNow() > 0)
             {
                 stateMachine.data.HpModify(-1);
             }
             if (stateMachine.data.GetHpNow() <= 0)
             {
                 stateMachine.ChangeState(stateMachine.stateRage);
                 return;
             }
         }   
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
