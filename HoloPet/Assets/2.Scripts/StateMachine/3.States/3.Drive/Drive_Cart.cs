using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Drive_Cart : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IMountableSM mountableSM;
    private IDriveSM driveSM;

    [SerializeField] private float speedMax;
    [SerializeField] private float speedPlus;
    private float speedNow;
    private bool isMounted;

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

        mountableSM = GetComponentInParent<IMountableSM>();
        if (mountableSM == null)
        {
            Debug.LogError($"{transform} ¡X no mountableSM found in parent.");
        }

        driveSM = GetComponentInParent<IDriveSM>();
        if (driveSM == null)
        {
            Debug.LogError($"{transform} ¡X no cartSM found in parent.");
        }
    }

    public override void Enter()
    {
        speedNow = 0f;
        isMounted = mountableSM.MountableMg.GetIsMounted();
        mountableSM.MountableMg.OnChangeMounted += MountableMg_OnChangeMounted;
    }

    public override void StateUpdate()
    {
        if (isMounted)
        {
            speedNow += speedPlus * Time.deltaTime;
        }
        else
        {
            speedNow -= speedPlus * 2f * Time.deltaTime;
        }

        if(speedNow <= 0)
        {
            stateMachine.ChangeState(basicSM.StateIdle);
        }

        if(speedNow >= speedMax)
        {
            stateMachine.ChangeState(driveSM.StateDirveMax);
        }

        if (basicSM.FaceDirectionMg.GetIsFaceRight())
        {
            basicSM.MovementMg.MoveRight(speedNow);
            if (basicSM.BoundaryMg.CheckIsRightBounderyAndResetPos())
            {
                basicSM.FaceDirectionMg.SetFaceLeft();
            }
        }
        else
        {
            basicSM.MovementMg.MoveLeft(speedNow);
            if (basicSM.BoundaryMg.CheckIsLeftBounderyAndResetPos())
            {
                basicSM.FaceDirectionMg.SetFaceRight();
            }
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
        mountableSM.MountableMg.OnChangeMounted -= MountableMg_OnChangeMounted;
    }

    private void MountableMg_OnChangeMounted(object sender, EventArgs e)
    {
        isMounted = mountableSM.MountableMg.GetIsMounted();
    }
}
