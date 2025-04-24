using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_Think : StateBase
{
    [SerializeField] private HoloMemStateMachine stateMachine;

    // < State Base >
    public override void Enter()
    {

        //cant do


        //event
        stateMachine.mouseInput.OnDrag += MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
        stateMachine.interactManager.OnInteractedByTarget += InteractManager_OnInteractedByTarget;

        //start

        if (!stateMachine.raycastManager.TrySetRaycastBothSide(10))
        {
            //exit to find nothing
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
        if (!stateMachine.interactManager.TrySetTargetWihtRaycastHits(stateMachine.raycastManager.GetRaycastHits()))
        {
            //exit to to find nothing
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }

        //interact option
        int interactOpLenth = stateMachine.interactManager.GetTargetIInteractable().GetInteractOptions().Length;
        interactOption[] interactOptions = new interactOption[interactOpLenth];
        int randomOpInt = UnityEngine.Random.Range(0, interactOpLenth);
        interactOption selectedInteractOption = stateMachine.interactManager.GetTargetIInteractable().GetInteractOptions()[randomOpInt];
        stateMachine.interactManager.SetChoosonInteractOp(selectedInteractOption);

        //exit to to follow target
        stateMachine.ChangeState(stateMachine.stateFollowTarget);
        return;

    }

    public override void StateUpdate()
    {
        
    }
    public override void StateLateUpdate()
    {

    }
    public override void Exit()
    {

        //event
        stateMachine.mouseInput.OnDrag -= MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick -= MouseInput_OnClick;
        stateMachine.interactManager.OnInteractedByTarget -= InteractManager_OnInteractedByTarget;

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
    private void InteractManager_OnInteractedByTarget(object sender, EventArgs e)
    {
        // Exit to interact
        stateMachine.ChangeState(stateMachine.stateInteracted);
        return;
    }
}
