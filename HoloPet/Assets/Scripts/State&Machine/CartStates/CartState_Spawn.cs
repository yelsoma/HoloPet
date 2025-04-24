using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartState_Spawn : StateBase
{
    [SerializeField] private CartStateMachine stateMachine;
    // < State Base >
    public override void Enter()
    {

        //cant do
        stateMachine.mountManager.SetIsMountableState(false);


        //event
        stateMachine.mouseInput.OnDrag += MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;

        //start
        stateMachine.faceDirection.SetFaceRight();
        stateMachine.layerManager.SetNewLayer();
        LayerCenter.ResetAllLayer();
    }

    public override void StateUpdate()
    {
        //exit to idle
        stateMachine.ChangeState(stateMachine.stateIdle);
        return;
    }
    public override void StateLateUpdate()
    {

    }
    public override void Exit()
    {
        //cant do
        stateMachine.mountManager.SetIsMountableState(true);

        //event
        stateMachine.mouseInput.OnDrag -= MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick -= MouseInput_OnClick;

    }

    // < Events >

    private void MouseInput_OnDrag(object sender, MouseInput.OnDragEventArgs e)
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
