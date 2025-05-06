using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_MountingInteract : StateBase
{

    [SerializeField] private HoloMemStateMachine stateMachine;

    public override void Enter()
    {
        //cant do

        //event

        //start
        if (stateMachine.mountManager.TrySetMount(stateMachine.interactManager.GetTargetIInteractable().GetTransform().GetComponent<IMountable>()))
        {
            //exit to Mounting
            stateMachine.ChangeState(stateMachine.stateMounting);
            return;
        }
        //exit to idle
        stateMachine.ChangeState(stateMachine.stateIdle);
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
        //cant do

        //event           
    }

    // < Events >
}
