using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolChooseARandom : StateBase
{
    [SerializeField] private float wanderChance;
    [SerializeField] private float randomThingChance;
    [SerializeField] private IdolStateMachine stateMachine;
    private float randomMove;
    

    // < State Base >
    public override void Enter()
    {   
        //event
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
        stateMachine.interactManager.OnInteract += InteractManager_OnInteract;
        //start
        randomMove = UnityEngine.Random.Range(0f, 1f);
    }
    public override void StateUpdate()
    {
        if (randomMove <= wanderChance)
        {
            //exit to wander
            stateMachine.ChangeState(stateMachine.stateWander);
            return;
        }
        if (wanderChance < randomMove && randomMove <= (wanderChance + randomThingChance))
        {
            
            if (stateMachine.interactManager.SetTarget(15f))
            {
                //exit to findTarget
                stateMachine.ChangeState(stateMachine.stateFindTarget);
                return;
            }
            else
            {
                //exit to idle
                stateMachine.ChangeState(stateMachine.stateIdle);
                return;
            }
        }
        if ((wanderChance + randomThingChance) < randomMove)
        {
            //exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
    }
    public override void StateLateUpdate()
    {

    }
    public override void Exit()
    {
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
        ///exit to knockUp
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
