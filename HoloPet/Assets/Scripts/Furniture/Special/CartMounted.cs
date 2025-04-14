using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMounted : StateBase
{
    [SerializeField] private FurnitureStateMachineOld stateMachine;
    [SerializeField] float speedIncreese;
    [SerializeField] float speedBreak;
    [SerializeField] float speedMax;
    [SerializeField] RaycastManager raycastManager;
    private float speedNow;

    // < State Base >
    public override void Enter()
    {
        speedNow = 0f;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.interactManager.OnInteract += InteractManager_OnInteract;
        if (stateMachine.mountManager.GetMounterName() != "Botan")
        {
            //exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
    }

    public override void StateUpdate()
    {        
        if (!stateMachine.data.GetIsMounted())
        {
            speedNow -= speedBreak * Time.deltaTime;
        }
        if (speedNow < 0)
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
        if (stateMachine.data.GetIsFaceRight())
        {
            stateMachine.movement.MoveRight(speedNow);
            if (speedNow >= speedMax)
            {
                raycastManager.SetForceInteractRaycastHits(0.5f, Vector2.right);
                if (raycastManager.GetIsThereForceInteract())
                {
                    if (raycastManager.GetForceInteract().transform.TryGetComponent<ObjectData>(out ObjectData objectData))
                    {
                        if (objectData.GetIsInteractableState())
                        {
                            stateMachine.interactManager.SetTarget(raycastManager.GetForceInteract());
                            stateMachine.interactManager.SetTagetInteracted();
                            stateMachine.interactManager.SetTargetKnockUp();
                        }
                    }
                }
                raycastManager.ClearLists();
            }
        }
        if (!stateMachine.data.GetIsFaceRight())
        {
            stateMachine.movement.MoveLeft(speedNow);
            if (speedNow >= speedMax)
            {
                raycastManager.SetForceInteractRaycastHits(0.5f, Vector2.left);
                if (raycastManager.GetIsThereForceInteract())
                {
                    if (raycastManager.GetForceInteract().transform.TryGetComponent<ObjectData>(out ObjectData objectData))
                    {
                        if (objectData.GetIsInteractableState())
                        {                     
                            stateMachine.interactManager.SetTarget(raycastManager.GetForceInteract());
                            stateMachine.interactManager.SetTagetInteracted();
                            stateMachine.interactManager.SetTargetKnockUp();
                        }
                    }
                }
                raycastManager.ClearLists();
            }
        }
        if (stateMachine.boundaryManager.CheckIsRightBoundery())
        {
            stateMachine.data.SetIsFaceRight(false);
        }
        if (stateMachine.boundaryManager.CheckIsLeftBoundery())
        {
            stateMachine.data.SetIsFaceRight(true);
        }
        if (speedNow < speedMax)
        {
            speedNow += speedIncreese * Time.deltaTime;
        }       
    }
    public override void StateLateUpdate()
    {

    }
    public override void Exit()
    {
        stateMachine.interactManager.OnInteract -= InteractManager_OnInteract;
        stateMachine.mouseInput.OnClick -= MouseInput_OnClick;
        stateMachine.mouseInput.OnDrag -= Input_OnDrag;
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
