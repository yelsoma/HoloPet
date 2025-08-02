using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartState_Mounted : StateBase
{
    [SerializeField] private CartStateMachine stateMachine;
    public override void Enter()
    {
        //event       

        //start
        /*
        stateMachine.data.SetHpToMax();
        */
        //if(stateMachine.mountManager.GetMounter().GetTransform().GetComponent<BaseDataManager>().GetObjectName() == ObjectNameEnum.Botan)
        //{
        //    stateMachine.ChangeState(stateMachine.stateDash);
        //}       
    }
    public override void StateUpdate()
    {
        //if (!stateMachine.mountManager.GetIsMounted())
        //{
        //    stateMachine.ChangeState(stateMachine.stateIdle);
        //}
    }
    public override void StateLateUpdate()
    {
        //keep idle
    }
    public override void Exit()
    {
        //event
    }
    // < Events >
}
