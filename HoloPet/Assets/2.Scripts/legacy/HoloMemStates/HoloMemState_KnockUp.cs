using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_KnockUp : StateBase
{
    public event EventHandler OnKnockUpFall;
    [SerializeField] private HoloMemStateMachine stateMachine;
    private float knockUpPower;
    [SerializeField] private float knockUpDecrese;
    private float knockUpPowerNow;  
    private float knockUpFaceDir;    
    private bool knockUpRight;
    private float knockBackPower;
    private bool startFall;
    private float fallSpeedIncreese = 6.5f;
    private float fallSpeedMax = 9f;
    private float fallSpeedNow;

    // < State Base >
    public override void Enter()
    {
        //cant do       
        stateMachine.interactManager.SetIsInteractable(false);      
        //stateMachine.mountManager.SetIsMountableState(false);

        //event

        //start
        knockUpPower = UnityEngine.Random.Range(7f, 7f);
        knockBackPower = UnityEngine.Random.Range(0.7f, 2f);
        fallSpeedNow = 0f;
        startFall = false;
        knockUpFaceDir = UnityEngine.Random.Range(1f, 0f);
        if (knockUpFaceDir <= 0.5)
        {
            knockUpRight = false;
            stateMachine.faceDirection.SetFaceRight();
        }
        else
        {
            knockUpRight = true;
            stateMachine.faceDirection.SetFaceLeft();
        }
        knockUpPowerNow = knockUpPower;

    }
    public override void StateUpdate()
    {
        if(knockUpPowerNow >= 0f)
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
            OnKnockUpFall?.Invoke(this, EventArgs.Empty);
            //max fall speed
            if(fallSpeedNow <= fallSpeedMax)
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
        //cant do
        stateMachine.interactManager.SetIsInteractable(true);
        //stateMachine.mountManager.SetIsMountableState(true);
        //event    
    }

    // < Events >
}
