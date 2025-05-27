using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Grab : StateBase
{
    [SerializeField] private EnemyStateMachine stateMachine;
    public event EventHandler OnExitGrab;
    public override void Enter()
    {
        //event       
        stateMachine.inputManager.OnMouseRelease += InputManager_OnMouseRelease;
        //start

        //can do
    }
    public override void StateUpdate()
    {
        
    }
    public override void StateLateUpdate()
    {
        stateMachine.boundaryManager.CheckAllBouderyAndResetPos();
    }
    public override void Exit()
    {
        OnExitGrab?.Invoke(this, EventArgs.Empty);
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
    // < Package Method >

    //courutine

}
