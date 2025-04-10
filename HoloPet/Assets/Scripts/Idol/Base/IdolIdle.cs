using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolIdle : StateBase
{
    [SerializeField] private float idleTimeMax;
    [SerializeField] private float idleTimeMin;
    [SerializeField] private IdolStateMachine stateMachine;
    private float idleTimer;   

    // < State Base >
    public override void Enter()
    {
        //event
        stateMachine.interactManager.OnInteract += InteractManager_OnInteract;
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;

        //start
        stateMachine.data.SetHpToMax();
        idleTimer = UnityEngine.Random.Range(idleTimeMin, idleTimeMax);       
    }
    public override void StateUpdate()
    {
        if (!stateMachine.boundaryManager.CheckIsBotBoundery())
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
