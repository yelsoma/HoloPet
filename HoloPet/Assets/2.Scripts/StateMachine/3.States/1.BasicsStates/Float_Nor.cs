using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;

    [SerializeField] private float floatSpeed;
    [SerializeField] private float SpeedMax;
    private float speedNow;
   

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
    }

    public override void Enter()
    {
        speedNow = 0f;
    }

    public override void StateUpdate()
    {
        if(speedNow <= SpeedMax)
        {
            speedNow += floatSpeed * Time.deltaTime;
        }
        //fall
        basicSM.PhysicsMg.MoveUp(speedNow);

        if (basicSM.BoundaryMg.CheckIsTopBounderyAndResetPos())
        {
            //exit to idle
            stateMachine.ChangeState(basicSM.StateIdle);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
