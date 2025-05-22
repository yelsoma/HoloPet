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

        //start     

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
        // exit to idle
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
    }

    // < Events >
}
