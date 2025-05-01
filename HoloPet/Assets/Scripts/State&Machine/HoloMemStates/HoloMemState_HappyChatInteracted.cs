using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_HappyChatInteracted : StateBase
{
    [SerializeField] private HoloMemStateMachine stateMachine;
    private bool interacterExit;

    public override void Enter()
    {
        Debug.Log(transform.root + "interacted");
        //can do
        stateMachine.interactManager.SetIsInteractable(false);

        //event     

        //start

        interacterExit = false;
        
        // check is there target , set face to target ,and set interacter to parent
        if (stateMachine.interactManager.GetInteracter() != null)
        {
            if (stateMachine.interactManager.GetIsInteracterRight())
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
        if (interacterExit)
        {
            // Exit to  idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
    }
    public override void StateLateUpdate()
    {  
    }
    public override void Exit()
    {
        //tell interacter that i exit interact
        stateMachine.interactManager.GetInteracter().InteractTargetExitInteract();
        // can do
        stateMachine.interactManager.SetIsInteractable(true);

        //event
    }

    // < Events >
}

