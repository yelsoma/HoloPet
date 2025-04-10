using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureGrab : StateBase
{
    [SerializeField] private FurnitureStateMachine stateMachine;

    // < State Base >
    public override void Enter()
    {
        stateMachine.data.SetIsInteractableState(false);
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.mouseInput.OnRelease += Input_OnRelease;
        stateMachine.interactManager.OnInteract += InteractManager_OnInteract;
        if (stateMachine.data.GetIsMounted())
        {
            stateMachine.mountManager.LayerModifyUp();
        }
        else
        {
            stateMachine.layerManager.ChangeAllLayer();
        }       
    }
    public override void StateUpdate()
    {
        // logic in event
    }
    public override void StateLateUpdate()
    {
        // update in event
    }
    public override void Exit()
    {
        stateMachine.data.SetIsInteractableState(true);
        stateMachine.mouseInput.OnDrag -= Input_OnDrag;
        stateMachine.mouseInput.OnRelease -= Input_OnRelease;
        stateMachine.interactManager.OnInteract -= InteractManager_OnInteract;
    }

    // < Events >
    private void Input_OnDrag(object sender, MouseInput.OnDragEventArgs e)
    {
        //drag
        stateMachine.movement.MoveToVector2(e.mousePos);
    }
    private void Input_OnRelease(object sender, System.EventArgs e)
    {
        //exit to fall
        stateMachine.ChangeState(stateMachine.stateFall);
        return;
    }
    private void InteractManager_OnInteract(object sender, EventArgs e)
    {
        //exit to interacted
        stateMachine.ChangeState(stateMachine.stateInteracted);
        return;
    }
}
