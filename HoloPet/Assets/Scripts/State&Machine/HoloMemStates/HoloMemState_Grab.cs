using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_Grab : StateBase
{
    [SerializeField] private HoloMemStateMachine stateMachine;
    public event EventHandler OnExitGrab;


    // < State Base >
    public override void Enter()
    {

        //cant do
        stateMachine.interactManager.SetIsInteractable(false);
        stateMachine.mountManager.SetIsMountableState(false);
        

        //event
        stateMachine.mouseInput.OnDrag += MouseInput_OnDrag;
        stateMachine.mouseInput.OnRelease += Input_OnRelease;


        //start
    }

    

    public override void StateUpdate()
    {     
        // logic in event
    }
    public override void StateLateUpdate()
    {
        stateMachine.boundaryManager.CheckAllBouderyAndResetPos();
    }
    public override void Exit()
    {
        OnExitGrab?.Invoke(this, EventArgs.Empty);
        //cant do
        stateMachine.interactManager.SetIsInteractable(true);
        stateMachine.mountManager.SetIsMountableState(true);
        //event
        stateMachine.mouseInput.OnDrag -= MouseInput_OnDrag;
        stateMachine.mouseInput.OnRelease -= Input_OnRelease;        
    }

    // < Events >
    private void MouseInput_OnDrag(object sender, MouseInput.OnDragEventArgs e)
    {
        //drag  
        stateMachine.transform.position = e.mousePos;
    }
    private void Input_OnRelease(object sender, System.EventArgs e)
    {
        if(stateMachine.raycastManager.TrySetRaycast(1f, Vector2.down))
        {
            if (stateMachine.mountManager.TrySetMountWithRaycast(stateMachine.raycastManager.GetRaycastHits()))
            {
                stateMachine.raycastManager.ClearHits();
                //exit to  mounting
                stateMachine.ChangeState(stateMachine.stateMounting);
                return;
            }                  
        }
        stateMachine.raycastManager.ClearHits();
        //exit to fall
        stateMachine.ChangeState(stateMachine.stateFall);
        return;      
    }   
}
