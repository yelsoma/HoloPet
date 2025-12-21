using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DriveJump_Cart : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private MountableMod mountableMod;
    private DriveMod driveMod;
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

        IBasicMod ibasicMod = GetComponentInParent<IBasicMod>();
        if (ibasicMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
        else
        {
            basicMod = ibasicMod.BasicMod;
        }

        IMountableMod imountableMod = GetComponentInParent<IMountableMod>();
        if (imountableMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no mountableMod  found in parent.");
        }
        else
        {
            mountableMod = imountableMod.MountableMod;
        }

        IDriveMod iDriveSM = GetComponentInParent<IDriveMod>();
        if (iDriveSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no cartSM found in parent.");
        }
        else
        {
            driveMod = iDriveSM.DriveMod;
        }
    }

    public override void Enter()
    {
        basicMod.PhysicsMg.SetJump(jumpPower);
        basicMod.PhysicsMg.ResetFall();
        if (basicMod.FaceDirectionMg.GetIsFaceRight())
        {
            jumpRight = true;
        }
        else
        {
            jumpRight = false;
        }
        isMounted = mountableMod.MountableMg.GetIsMounted();
        mountableMod.MountableMg.OnChangeMounted += MountableMg_OnChangeMounted;
    }

    public override void StateUpdate()
    {
        if(basicMod.PhysicsMg.KeepJump())
        {
            if (basicMod.BoundaryMg.CheckIsTopBounderyAndResetPos())
            {
                basicMod.PhysicsMg.SetJump(0);
            }
        }
        else
        {
            basicMod.PhysicsMg.KeepFall();
            if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                if (isMounted)
                {
                    stateMachine.ChangeState(driveMod.StateDirveMax);
                    return;
                }
                else
                {
                    stateMachine.ChangeState(basicMod.StateIdle);
                    return;
                }
            }
        }
        if (jumpRight)
        {
            basicMod.PhysicsMg.MoveRight(jumpForward);
            if (basicMod.BoundaryMg.CheckIsRightBounderyAndResetPos())
            {
                jumpRight = false;
                basicMod.FaceDirectionMg.SetFaceLeft();
            }
        }
        else
        {
            basicMod.PhysicsMg.MoveLeft(jumpForward);
            if (basicMod.BoundaryMg.CheckIsLeftBounderyAndResetPos())
            {
                jumpRight = true;
                basicMod.FaceDirectionMg.SetFaceRight();
            }
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
        mountableMod.MountableMg.OnChangeMounted -= MountableMg_OnChangeMounted;
    }

    private void MountableMg_OnChangeMounted(object sender, EventArgs e)
    {
        isMounted = mountableMod.MountableMg.GetIsMounted();
    }
}
