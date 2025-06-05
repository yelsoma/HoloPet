using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicState_InAir : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;

    [SerializeField] private float fallSpeedIncrease;
    [SerializeField] private float fallSpeedMax;
    private float fallSpeedNow;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform} ¡X no StateMachineBase found in parent.");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform} ¡X no basicSM found in parent.");
        }
    }

    public override void Enter()
    {
        fallSpeedNow = 0f;
    }

    public override void StateUpdate()
    {
        //fall
        basicSM.Movement.MoveDown(fallSpeedNow);
        fallSpeedNow += fallSpeedIncrease * Time.deltaTime;

        if (basicSM.BoundaryManager.CheckIsBotBounderyAndResetPos())
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
