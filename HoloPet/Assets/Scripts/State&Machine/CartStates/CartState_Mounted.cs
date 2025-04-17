using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartState_Mounted : StateBase
{
    [SerializeField] private CartStateMachine stateMachine;
    public override void Enter()
    {
        //event       
        stateMachine.mouseInput.OnDrag += MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;

        //start
        /*
        stateMachine.data.SetHpToMax();
        */
    }
    public override void StateUpdate()
    {
        stateMachine.ChangeState(stateMachine.stateDash);
    }
    public override void StateLateUpdate()
    {
        //keep idle
    }
    public override void Exit()
    {
        //event
        stateMachine.mouseInput.OnDrag -= MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick -= MouseInput_OnClick;

    }
    // < Events >
    private void MouseInput_OnDrag(object sender, MouseInputVr2.OnDragEventArgs e)
    {
        //exit to grab
        stateMachine.ChangeState(stateMachine.stateGrab);
        return;
    }
    private void MouseInput_OnClick(object sender, EventArgs e)
    {
        //exit to KnockUp
        stateMachine.ChangeState(stateMachine.stateKnockUp);
        return;
    }
}
