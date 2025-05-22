using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_Bullied : StateBase
{
    [SerializeField] private HoloMemStateMachine stateMachine;
    [SerializeField] private float knockUpPower;
    [SerializeField] private float KnockUpDecrease;
    [SerializeField] private float knockBackPower;
    private float fallSpeedNow;
    private float fallSpeedIncreese = 6.5f;
    private float fallSpeedMax = 9f;
    [SerializeField] private float panicTime;
    [SerializeField] private float panicRunSpeed;
    private bool isTargetRight;
    private Coroutine getHitCoroutine;

    //ani event
    public event EventHandler OnHit;
    public event EventHandler OnFall;
    public event EventHandler OnPanic;
    public override void Enter()
    {
        //can do
        stateMachine.interactManager.SetIsInteractable(false);

        //event     

        //start         
        if (stateMachine.interactManager.GetInteracter() != null)
        {
            //change face
            if (stateMachine.interactManager.GetIsInteracterRight())
            {
                isTargetRight = true;
                stateMachine.faceDirection.SetFaceRight();
            }
            else
            {
                isTargetRight = false;
                stateMachine.faceDirection.SetFaceLeft();
            }
            //get hit
            getHitCoroutine = StartCoroutine(CoGetHit());
        }
        else
        {
            // exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
    }
    public override void StateUpdate()
    {
       
    }
    public override void StateLateUpdate()
    {

    }
    public override void Exit()
    {
        // can do
        stateMachine.interactManager.SetIsInteractable(true);
        if(getHitCoroutine != null)
        {
            StopCoroutine(getHitCoroutine);
        }
        //event
    }

    // < Events >
    //coroutine
    private IEnumerator CoGetHit()
    {
        float knockUpPowerNow = knockUpPower;
        //knockup
        OnHit?.Invoke(this, EventArgs.Empty);
        while (knockUpPowerNow > 0)
        {
            stateMachine.movement.MoveUp(knockUpPowerNow);
            knockUpPowerNow -= KnockUpDecrease * Time.deltaTime;
            if (isTargetRight)
            {
                stateMachine.movement.MoveLeft(knockBackPower);
                //if hit wall change dir
                if (stateMachine.boundaryManager.CheckIsLeftBounderyAndResetPos())
                {
                    isTargetRight = false;
                }
            }
            else
            {
                stateMachine.movement.MoveRight(knockBackPower);
                //if hit wall change dir
                if (stateMachine.boundaryManager.CheckIsRightBounderyAndResetPos())
                {
                    isTargetRight = true;
                }
            }
            yield return null;
        }
        //start fall
        OnFall?.Invoke(this, EventArgs.Empty);
        fallSpeedNow = 0f; 
        while(!stateMachine.boundaryManager.CheckIsBotBounderyAndResetPos())
        {

            stateMachine.movement.MoveDown(fallSpeedNow);
            if(fallSpeedNow < fallSpeedMax)
            {
                fallSpeedNow += fallSpeedIncreese*Time.deltaTime;
            }
            if (isTargetRight)
            {
                stateMachine.movement.MoveLeft(knockBackPower);
                if (stateMachine.boundaryManager.CheckIsLeftBounderyAndResetPos())
                {
                    isTargetRight = false;
                }
            }
            else
            {
                stateMachine.movement.MoveRight(knockBackPower);
                if (stateMachine.boundaryManager.CheckIsRightBounderyAndResetPos())
                {
                    isTargetRight = true;
                }
            }
            yield return null;
        }
        //land ground set face dir
        if (isTargetRight)
        {
            stateMachine.faceDirection.SetFaceLeft();
        }
        else
        {
            stateMachine.faceDirection.SetFaceRight();
        }
        //start panic run
        OnPanic.Invoke(this, EventArgs.Empty);
        float panicTimer = panicTime;
        while (panicTimer > 0)
        {
            if (isTargetRight)
            {
                stateMachine.movement.MoveLeft(panicRunSpeed);
                if (stateMachine.boundaryManager.CheckIsLeftBounderyAndResetPos())
                {
                    stateMachine.faceDirection.SetFaceRight();
                    isTargetRight = false;
                }
            }
            else
            {
                stateMachine.movement.MoveRight(panicRunSpeed);
                if (stateMachine.boundaryManager.CheckIsRightBounderyAndResetPos())
                {
                   stateMachine.faceDirection.SetFaceLeft();
                   isTargetRight = true;
                }
            }
            panicTimer -= Time.deltaTime;
            yield return null;
        }
        //exit to idle
        stateMachine.ChangeState(stateMachine.stateIdle);
    }
}
