using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_Grab : StateBase
{
    [SerializeField] private HoloMemStateMachine stateMachine;


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
        
        stateMachine.layerManager.ResetLayerAll();
        LayerCenter.ResetAllLayer();
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
        //cant do
        stateMachine.interactManager.SetIsInteractable(true);
        stateMachine.mountManager.SetIsMountableState(true);
        //event
        stateMachine.mouseInput.OnDrag -= MouseInput_OnDrag;
        stateMachine.mouseInput.OnRelease -= Input_OnRelease;        
    }

    // < Events >
    private void MouseInput_OnDrag(object sender, MouseInputVr2.OnDragEventArgs e)
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
