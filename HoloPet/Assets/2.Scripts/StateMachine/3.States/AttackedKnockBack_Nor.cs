using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AniEnum;
using System;

public class AttackedKnockBack_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IMountableSM mountableSM;
    private IAttackableSM attackableSM;
    [SerializeField] private float knockUpPower;
    [SerializeField] private float knockUpDecrease;
    private float knockBackPower;
    private float knockUpPowerNow;
    private bool knockBackRight;
    private float fallSpeedNow;
    private float fallSpeedIncreese = 6.5f;
    private float fallSpeedMax = 9f;
    public event EventHandler OnKnockUpFall;
    private bool FallEventTriggered;
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

        attackableSM = GetComponentInParent<IAttackableSM>();
        if (attackableSM == null)
        {
            Debug.LogError($"{transform} ¡X no attackableSM found in parent.");
        }
    }

    public override void Enter()
    {
        knockUpPowerNow = knockUpPower;
        fallSpeedNow = 0f;
        if (attackableSM.AttackableMg.GetIsKnockRight())
        {
            knockBackRight = true;
        }
        else
        {
            knockBackRight = false;
        }
        knockBackPower = attackableSM.AttackableMg.GetKnockBackPower();
        FallEventTriggered = false;
    }

    public override void StateUpdate()
    {
        if (knockUpPowerNow >= 0)
        {
            knockUpPowerNow -= knockUpDecrease * Time.deltaTime;
            basicSM.MovementMg.MoveUp(knockUpPowerNow);
            if (basicSM.BoundaryMg.CheckIsTopBounderyAndResetPos())
            {
                knockUpPowerNow = 0f;
            }
        }
        else
        {
            if(FallEventTriggered == false)
            {
                OnKnockUpFall?.Invoke(this, EventArgs.Empty);
                FallEventTriggered = true;
            }          
            basicSM.MovementMg.MoveDown(fallSpeedNow);
            if (fallSpeedNow < fallSpeedMax)
            {
                fallSpeedNow += fallSpeedIncreese * Time.deltaTime;
            }
            else
            {
                fallSpeedNow = fallSpeedMax;
            }
            if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                stateMachine.ChangeState(basicSM.StateIdle);
                return;
            }
        }
        if (knockBackRight)
        {
            basicSM.MovementMg.MoveRight(knockBackPower);
            if (basicSM.BoundaryMg.CheckIsRightBounderyAndResetPos())
            {
                knockBackRight = false;
                basicSM.FaceDirectionMg.SetFaceLeft();
            }
        }
        else
        {
            basicSM.MovementMg.MoveLeft(knockBackPower);
            if (basicSM.BoundaryMg.CheckIsLeftBounderyAndResetPos())
            {
                knockBackRight = true;
                basicSM.FaceDirectionMg.SetFaceRight();
            }
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
