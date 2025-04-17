using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_Idle : StateBase
{
    [SerializeField] private float idleTimeMax;
    [SerializeField] private float idleTimeMin;
    [SerializeField] private HoloMemStateMachine stateMachine;
    private float idleTimer;

    // < State Base >
    public override void Enter()
    {
        //event       
        stateMachine.interactManager.OnInteractedByTarget += InteractManager_OnInteractedByTarget;
        stateMachine.mouseInput.OnDrag += MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;

        //start
        /*
        stateMachine.data.SetHpToMax();
        */
        idleTimer = UnityEngine.Random.Range(idleTimeMin, idleTimeMax);
    }

    

    public override void StateUpdate()
    {
        if (!stateMachine.boundaryManager.CheckIsBotBounderyAndResetPos())
        {
            //exit to fall
            stateMachine.ChangeState(stateMachine.stateFall);
            return;
        }
        if (IsIdleTimeUp())
        {
            //exit to choose random  
            stateMachine.ChangeState(stateMachine.stateChooseARandom);
            return;
        }
        idleTimer -= Time.deltaTime;
    }
    public override void StateLateUpdate()
    {
        //keep idle
    }
    public override void Exit()
    {
        //event
        stateMachine.mouseInput.OnDrag -= MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick -= MouseInput_OnClick;
        stateMachine.interactManager.OnInteractedByTarget -= InteractManager_OnInteractedByTarget;
    }

    // < Events >
    private void MouseInput_OnDrag(object sender, MouseInputVr2.OnDragEventArgs e)
    {
        //exit to grab
        stateMachine.ChangeState(stateMachine.stateGrab);
        return;
    }
    private void MouseInput_OnClick(object sender, EventArgs e)
    {
        //exit to KnockUp
        stateMachine.ChangeState(stateMachine.stateKnockUp);
        return;
    }
    private void InteractManager_OnInteractedByTarget(object sender, EventArgs e)
    {
        // Exit to interact
        stateMachine.ChangeState(stateMachine.stateInteracted);
        return;
    }
    // < Package Method >
    private bool IsIdleTimeUp()
    {
        if (idleTimer > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
}
