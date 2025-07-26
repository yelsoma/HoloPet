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
    }

    // < Events >
}
