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
    private bool FallEventTriggered;
    private bool knockBackRight;
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

        attackableSM = GetComponentInParent<IAttackableSM>();
        if (attackableSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no attackableSM found in parent.");
        }
    }

    public override void Enter()
    {
        if (!attackableSM.AttackableMg.GetIsKnockable())
        {
            stateMachine.ChangeState(basicSM.StateIdle);
            return;
        }
        basicSM.PhysicsMg.SetJump(knockUpPower);
        basicSM.PhysicsMg.ResetFall();      
        knockBackRight = attackableSM.AttackableMg.GetIsKnockRight();
        if (knockBackRight)
        {
            basicSM.FaceDirectionMg.SetFaceLeft();
        }
        else
        {
            basicSM.FaceDirectionMg.SetFaceRight();
        }
        knockBackPower = attackableSM.AttackableMg.GetKnockBackPower();
        attackableSM.AttackableMg.SetIsKnockable(false);
        FallEventTriggered = false;
    }

    public override void StateUpdate()
    {
        if (basicSM.PhysicsMg.KeepJump())
        {
            if (basicSM.BoundaryMg.CheckIsTopBounderyAndResetPos())
            {
                basicSM.PhysicsMg.SetJump(0);
            }
        }
        else
        {
            if(FallEventTriggered == false)
            {
                TriggerAni1();// fall ani
                FallEventTriggered = true;
            }
            basicSM.PhysicsMg.KeepFall();
            if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                stateMachine.ChangeState(basicSM.StateIdle);
                return;
            }
        }
        if (knockBackRight)
        {
            basicSM.PhysicsMg.MoveRight(knockBackPower);
            if (basicSM.BoundaryMg.CheckIsRightBounderyAndResetPos())
            {
                knockBackRight = false;
                basicSM.FaceDirectionMg.SetFaceLeft();
            }
        }
        else
        {
            basicSM.PhysicsMg.MoveLeft(knockBackPower);
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
        attackableSM.AttackableMg.SetIsKnockable(true);
    }
}
