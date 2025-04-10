using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdolMounting : StateBase
{
    [SerializeField] private IdolStateMachine stateMachine;
    private Vector2 mountPos;
    public override void Enter()
    {
        //cant do
        stateMachine.data.SetIsInteractableState(false);

        //event
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
        stateMachine.interactManager.OnInteract += InteractManager_OnInteract;

        //start
        stateMachine.mountManager.EnterMounting();
        stateMachine.mountManager.LayerModifyDown();       
    }
    public override void StateUpdate()
    {
        if (!stateMachine.mountManager.GetMountIsMountable())
        {
            // exit to knockUp
            stateMachine.ChangeState(stateMachine.stateKnockUp);
            return;
        }
             
    }
    public override void StateLateUpdate()
    {
        stateMachine.movement.MoveToVector2(stateMachine.mountManager.GetMountSeat());
        stateMachine.mountManager.FollowMountFaceDir();
    }
    public override void Exit()
    {
        stateMachine.mountManager.ExitMounting();
        stateMachine.data.SetIsInteractableState(true);
        //event        
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
    private void MouseInput_OnClick(object sender, EventArgs e)
    {
        //exit to knockUp
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
