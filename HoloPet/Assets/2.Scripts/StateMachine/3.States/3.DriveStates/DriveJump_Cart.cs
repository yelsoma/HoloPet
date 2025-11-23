using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DriveJump_Cart : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IMountableSM mountableSM;
    private IDriveSM driveSM;
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpForward;
    private bool jumpRight;
    private bool isMounted;

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

        mountableSM = GetComponentInParent<IMountableSM>();
        if (mountableSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no mountableSM found in parent.");
        }

        driveSM = GetComponentInParent<IDriveSM>();
        if (driveSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no cartSM found in parent.");
        }
    }

    public override void Enter()
    {
        basicSM.PhysicsMg.SetJump(jumpPower);
        basicSM.PhysicsMg.ResetFall();
        if (basicSM.FaceDirectionMg.GetIsFaceRight())
        {
            jumpRight = true;
        }
        else
        {
            jumpRight = false;
        }
        isMounted = mountableSM.MountableMg.GetIsMounted();
        mountableSM.MountableMg.OnChangeMounted += MountableMg_OnChangeMounted;
    }

    public override void StateUpdate()
    {
        if(basicSM.PhysicsMg.KeepJump())
        {
            if (basicSM.BoundaryMg.CheckIsTopBounderyAndResetPos())
            {
                basicSM.PhysicsMg.SetJump(0);
            }
        }
        else
        {
            basicSM.PhysicsMg.KeepFall();
            if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                if (isMounted)
                {
                    stateMachine.ChangeState(driveSM.StateDirveMax);
                    return;
                }
                else
                {
                    stateMachine.ChangeState(basicSM.StateIdle);
                    return;
                }
            }
        }
        if (jumpRight)
        {
            basicSM.PhysicsMg.MoveRight(jumpForward);
            if (basicSM.BoundaryMg.CheckIsRightBounderyAndResetPos())
            {
                jumpRight = false;
                basicSM.FaceDirectionMg.SetFaceLeft();
            }
        }
        else
        {
            basicSM.PhysicsMg.MoveLeft(jumpForward);
            if (basicSM.BoundaryMg.CheckIsLeftBounderyAndResetPos())
            {
                jumpRight = true;
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
