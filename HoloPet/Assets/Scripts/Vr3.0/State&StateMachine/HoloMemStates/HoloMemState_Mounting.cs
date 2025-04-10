using System;
using UnityEngine;

public class HoloMemState_Mounting : StateBase
{
    [SerializeField] private HoloMemStateMachine stateMachine;
    public override void Enter()
    {
        //cant do
        stateMachine.interactManager.SetIsInteractable(false);
        
        //event
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;

        //start
        stateMachine.mountManager.EnterMount(); 
        /*
        stateMachine.mountManager.LayerModifyDown();
        */   
    }
    public override void StateUpdate()
    {
        if (!stateMachine.mountManager.GetMount().GetIsMountableState())
        {
            // exit to knockUp
            stateMachine.ChangeState(stateMachine.stateKnockUp);
            return;
        }
    }
    public override void StateLateUpdate()
    {      
    }
    public override void Exit()
    {
        stateMachine.mountManager.ExitMount();
        //cant do
        stateMachine.interactManager.SetIsInteractable(true);
        //event        
        stateMachine.mouseInput.OnDrag -= Input_OnDrag;
        stateMachine.mouseInput.OnClick -= MouseInput_OnClick;        
    }

    // < Events >
    private void Input_OnDrag(object sender, MouseInputVr2.OnDragEventArgs e)
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
}
