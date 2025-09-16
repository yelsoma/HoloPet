using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AniEnum;
using System;

public class AttackedKnockBack_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IAttackableSM attackableSM;
    [SerializeField] private float knockUpPower;
    private float knockBackPower;
    private bool knockBackRight;
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

        attackableSM = GetComponentInParent<IAttackableSM>();
        if (attackableSM == null)
        {
            Debug.LogError($"{transform} ¡X no attackableSM found in parent.");
        }
    }

    public override void Enter()
    {
        basicSM.MovementMg.SetJump(knockUpPower);
        basicSM.MovementMg.ResetFall();
        if (attackableSM.AttackableMg.GetIsAttackerLeft())
        {
            knockBackRight = true;
        }
        else
        {
            knockBackRight = false;
        }
        knockBackPower = attackableSM.AttackableMg.GetKnockBackPower();
        if(knockUpPower == 0f)
        {
            knockBackPower = 0f;
        }
        FallEventTriggered = false;
        basicSM.MovementMg.KeepJump();
        if(attackableSM.AttackableMg.GetHp() == 0)
        {
            attackableSM.AttackableMg.SetDeath(true);
        }
    }

    public override void StateUpdate()
    {
        if (basicSM.MovementMg.KeepJump())
        {
            if (basicSM.BoundaryMg.CheckIsTopBounderyAndResetPos())
            {
                basicSM.MovementMg.SetJump(0);
            }
        }
        else
        {
            if(FallEventTriggered == false)
            {
                TriggerAni1();// fall ani
                FallEventTriggered = true;
            }
            basicSM.MovementMg.KeepFall();
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
    public void SetKonckUp0(float power)
    {
        knockUpPower = power;
        Debug.Log("here");
    }
}
