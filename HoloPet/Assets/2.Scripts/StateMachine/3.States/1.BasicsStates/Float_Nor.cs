using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;

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

        IBasicMod ibasicMod = GetComponentInParent<IBasicMod>();
        if (ibasicMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
        else
        {
            basicMod = ibasicMod.BasicMod;
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
        basicMod.PhysicsMg.MoveUp(speedNow);

        if (basicMod.BoundaryMg.CheckIsTopBounderyAndResetPos())
        {
            //exit to idle
            stateMachine.ChangeState(basicMod.StateIdle);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
