using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_Bully : StateBase
{
    [SerializeField] private HoloMemStateMachine stateMachine;
    [SerializeField] private float waitTime;
    private Coroutine waitPunchAni;
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

        waitPunchAni = StartCoroutine(CoWaitAni());
    }
    public override void StateUpdate()
    {
        
    }
    public override void StateLateUpdate()
    {
       
    }
    public override void Exit()
    {
        StopCoroutine(waitPunchAni);
        // can do
        stateMachine.interactManager.SetIsInteractable(true);

        //event
    }

    // < Events >
    //Coroutine
    private IEnumerator CoWaitAni()
    {
        yield return new WaitForSeconds(waitTime);
        // exit to idle
        stateMachine.ChangeState(stateMachine.stateIdle);
    }
}
