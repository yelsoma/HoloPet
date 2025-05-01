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
        stateMachine.inputManager.OnMouseRelease += InputManager_OnMouseRelease;

        //start
    }

    public override void StateUpdate()
    {
        stateMachine.transform.position = stateMachine.inputManager.GetMouseVetor2();
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
        stateMachine.inputManager.OnMouseRelease -= InputManager_OnMouseRelease;
    }

    // < Events >
    private void InputManager_OnMouseRelease(object sender, EventArgs e)
    {
        if (stateMachine.raycastManager.TrySetRaycast(1f, Vector2.down))
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
