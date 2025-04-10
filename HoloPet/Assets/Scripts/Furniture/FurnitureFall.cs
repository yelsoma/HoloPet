using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureFall : StateBase
{
    [SerializeField] private FurnitureStateMachine stateMachine;
    [SerializeField] float fallSpeedIncreese;
    private float fallSpeedNow;

    // < State Base >
    public override void Enter()
    {
        stateMachine.data.SetIsInteractableState(false);
        if (!stateMachine.data.GetIsMounted())
        {
            stateMachine.data.SetIsMountableState(false);
        }       
        fallSpeedNow = 0f;
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
        stateMachine.interactManager.OnInteract += InteractManager_OnInteract;
    }

    public override void StateUpdate()
    {
        if (!stateMachine.boundaryManager.CheckIsBotBoundery())
        {

            fallSpeedNow += fallSpeedIncreese * Time.deltaTime;
            //keep fall
        }
        if (stateMachine.boundaryManager.CheckIsBotBoundery())
        {
            //exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
        }
    }
    public override void StateLateUpdate()
    {
        stateMachine.movement.MoveDown(fallSpeedNow);
    }
    public override void Exit()
    {
        stateMachine.data.SetIsInteractableState(true);
        stateMachine.data.SetIsMountableState(true);
        stateMachine.mouseInput.OnDrag -= Input_OnDrag;
        stateMachine.interactManager.OnInteract -= InteractManager_OnInteract;
    }

    // < Events >
    private void Input_OnDrag(object sender, MouseInput.OnDragEventArgs e)
    {
        //exit to grab
        stateMachine.ChangeState(stateMachine.stateGrab);
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
