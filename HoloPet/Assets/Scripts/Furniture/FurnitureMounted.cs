using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureMounted : StateBase
{
    [SerializeField] private FurnitureStateMachineOld stateMachine; 
    // < State Base >
    public override void Enter()
    {
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.interactManager.OnInteract += InteractManager_OnInteract;
    }

    public override void StateUpdate()
    {
        if (!stateMachine.data.GetIsMounted())
        {
            //exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }       
        if (!stateMachine.boundaryManager.CheckIsBotBoundery())
        {
            //exit to fall
            stateMachine.ChangeState(stateMachine.stateFall);
            return;
        }           
    }
    public override void StateLateUpdate()
    {
               
    }
    public override void Exit()
    {
        stateMachine.mouseInput.OnClick -= MouseInput_OnClick;
        stateMachine.mouseInput.OnDrag -= Input_OnDrag;
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
