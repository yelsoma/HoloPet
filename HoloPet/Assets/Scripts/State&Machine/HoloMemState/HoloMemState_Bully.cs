using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_Bully : StateBase
{

    [SerializeField] private HoloMemStateMachine stateMachine;
    public override void Enter()
    {
        //can do
        stateMachine.interactManager.SetIsInteractable(false);

        //event     
        stateMachine.mouseInput.OnDrag += MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;

        //start     
        stateMachine.mountManager.LayerChainUpStart();

        // check is there target , set face to target
        if (stateMachine.interactManager.GetTargetIInteractable() != null)
        {
            if (stateMachine.interactManager.GetIsTargetRight())
            {
                stateMachine.faceDirection.SetFaceRight();
            }
            else
            {
                stateMachine.faceDirection.SetFaceLeft();
            }
        }
        else
        {
            // exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
    }
    public override void StateUpdate()
    {
        //hit target
        Debug.Log("hit" + stateMachine.interactManager.GetTargetIInteractable());
        stateMachine.ChangeState(stateMachine.stateIdle);
        return;
     
    }
    public override void StateLateUpdate()
    {

    }
    public override void Exit()
    {
        // can do
        stateMachine.interactManager.SetIsInteractable(true);

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
        //exit to knock up
        stateMachine.ChangeState(stateMachine.stateKnockUp);
        return;
    }
}
