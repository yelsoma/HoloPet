using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Fall : StateBase
{
    [SerializeField] private EnemyStateMachine stateMachine;
    [SerializeField] private float fallSpeedIncrease;
    [SerializeField] private float fallSpeedMax;
    private float fallSpeedNow;
    public override void Enter()
    {
        //event       

        //start
        fallSpeedNow = 0;
        //can do
    }
    public override void StateUpdate()
    {
        if(fallSpeedNow <= fallSpeedMax)
        {
            fallSpeedNow += fallSpeedIncrease * Time.deltaTime;
        }
        if (!stateMachine.boundaryManager.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.movement.MoveDown(fallSpeedNow);
        }
        else
        {
            //Exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
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
