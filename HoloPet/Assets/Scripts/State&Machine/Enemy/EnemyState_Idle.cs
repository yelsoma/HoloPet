using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Idle : StateBase
{
    [SerializeField] private EnemyStateMachine stateMachine;
    [SerializeField] private float idleTimeMax;
    [SerializeField] private float idleTimeMin;
    private float idleTime;
    public override void Enter()
    {
        //event       

        //start
        idleTime = Random.Range(idleTimeMax, idleTimeMin);
        //can do
    }
    public override void StateUpdate()
    {
        if (!stateMachine.boundaryManager.CheckIsBotBounderyAndResetPos())
        {
            //exit to fall
            stateMachine.ChangeState(stateMachine.stateFall);
            return;
        }
        if (idleTime >= 0)
        {
            idleTime -= Time.deltaTime;
        }
        else
        {
            //exit to wander
            stateMachine.ChangeState(stateMachine.stateWander);
            return;
        }
    }
    public override void StateLateUpdate()
    {

    }
    public override void Exit()
    {

    }
    // < Events >

    // < Package Method >

    //courutine

}
