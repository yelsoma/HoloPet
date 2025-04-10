using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolGrab : StateBase
{
    [SerializeField] private IdolStateMachine stateMachine;    
   

    // < State Base >
    public override void Enter()
    {
        //cant do
        stateMachine.data.SetIsInteractableState(false);
        stateMachine.data.SetIsMountableState(false);

        //event
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.mouseInput.OnRelease += Input_OnRelease;
        stateMachine.interactManager.OnInteract += InteractManager_OnInteract;

        //start
        stateMachine.layerManager.ChangeAllLayer();          
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
        stateMachine.data.SetIsMountableState(true);
        //event
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
        /*
        stateMachine.raycastManager.SetRaycastHits(1.5f, Vector2.down);
        if (stateMachine.raycastManager.GetMoutableArray().Length > 0)
        {
            stateMachine.mountManager.SetMount(stateMachine.raycastManager.GetMoutableArray());
            //clear cast
            stateMachine.raycastManager.ClearLists();
            // exit to mount
            stateMachine.ChangeState(stateMachine.stateMounting);
            return;
        }            
        //clear cast
        stateMachine.raycastManager.ClearLists();
        */
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
