using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Mounting : StateBase
{
    [SerializeField] private EnemyStateMachine stateMachine;
    public override void Enter()
    {
        //event       

        //start

        //can do
        stateMachine.mountManager.EnterMount();
    }
    public override void StateUpdate()
    {
        if (!stateMachine.mountManager.GetMount().GetIsMountableState())
        {
            // exit to knockUp
            stateMachine.ChangeState(stateMachine.stateKnockBack);
            return;
        }
    }
    public override void StateLateUpdate()
    {

    }
    public override void Exit()
    {
        stateMachine.mountManager.ExitMount();
    }
    // < Events >

    // < Package Method >

    //courutine

}
