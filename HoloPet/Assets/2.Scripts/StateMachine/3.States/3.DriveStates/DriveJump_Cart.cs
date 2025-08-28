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
    [SerializeField] private float jumpDecrease;
    [SerializeField] private float jumpForward;
    private float jumpPowerNow;
    private bool jumpRight;
    private float fallSpeedNow;
    private float fallSpeedIncreese = 6.5f;
    private float fallSpeedMax = 9f;
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
        jumpPowerNow = jumpPower;
        fallSpeedNow = 0f;
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
        if(jumpPowerNow >= 0)
        {
            jumpPowerNow -= jumpDecrease * Time.deltaTime;
            basicSM.MovementMg.MoveUp(jumpPowerNow);
            if (basicSM.BoundaryMg.CheckIsTopBounderyAndResetPos())
            {
                jumpPowerNow = 0f;  
            }
        }
        else
        {
            basicSM.MovementMg.MoveDown(fallSpeedNow);
            if(fallSpeedNow< fallSpeedMax)
            {
                fallSpeedNow += fallSpeedIncreese * Time.deltaTime;
            }
            else
            {
                fallSpeedNow = fallSpeedMax;
            }
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
            basicSM.MovementMg.MoveRight(jumpForward);
            if (basicSM.BoundaryMg.CheckIsRightBounderyAndResetPos())
            {
                jumpRight = false;
                basicSM.FaceDirectionMg.SetFaceLeft();
            }
        }
        else
        {
            basicSM.MovementMg.MoveLeft(jumpForward);
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
