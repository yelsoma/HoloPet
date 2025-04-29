using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_ChooseRandom : StateBase
{
    [SerializeField] private HoloMemStateMachine stateMachine;    
    [SerializeField] private StateBase[] randomStates;
    private int randomNum;
    // < State Base >
    public override void Enter()
    {
        //can do

        //event
        stateMachine.mouseInput.OnDrag += MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
        stateMachine.interactManager.OnInteractedByInteracter += InteractManager_OnInteractedByInteracter;
        //start
        randomNum = UnityEngine.Random.Range(0, randomStates.Length);       
    }
    public override void StateUpdate()
    {
        //exit to a random state

        stateMachine.ChangeState(randomStates[randomNum]);
        return;
       
    }
    public override void StateLateUpdate()
    {

    }
    public override void Exit()
    {   
        //can do

        //event
        stateMachine.mouseInput.OnDrag -= MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick -= MouseInput_OnClick;
        stateMachine.interactManager.OnInteractedByInteracter -= InteractManager_OnInteractedByInteracter;
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
        ///exit to knockUp
        stateMachine.ChangeState(stateMachine.stateKnockUp);
        return;
    }
    private void InteractManager_OnInteractedByInteracter(object sender, EventArgs e)
    {
        // Exit to interact
        stateMachine.interactManager.OnInteractWithOption(stateMachine.interactManager.GetInteracter().GetChoosenOp());
        return;
    }
}
