using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AniEnum;
using System;

public class AttackedKnockBack_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
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

        IBasicMod ibasicMod = GetComponentInParent<IBasicMod>();
        if (ibasicMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
        else
        {
            basicMod = ibasicMod.BasicMod;
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
            stateMachine.ChangeState(basicMod.StateIdle);
            return;
        }
        basicMod.PhysicsMg.SetJump(knockUpPower);
        basicMod.PhysicsMg.ResetFall();      
        knockBackRight = attackableSM.AttackableMg.GetIsKnockRight();
        if (knockBackRight)
        {
            basicMod.FaceDirectionMg.SetFaceLeft();
        }
        else
        {
            basicMod.FaceDirectionMg.SetFaceRight();
        }
        knockBackPower = attackableSM.AttackableMg.GetKnockBackPower();
        attackableSM.AttackableMg.SetIsKnockable(false);
        FallEventTriggered = false;
    }

    public override void StateUpdate()
    {
        if (basicMod.PhysicsMg.KeepJump())
        {
            if (basicMod.BoundaryMg.CheckIsTopBounderyAndResetPos())
            {
                basicMod.PhysicsMg.SetJump(0);
            }
        }
        else
        {
            if(FallEventTriggered == false)
            {
                TriggerAni1();// fall ani
                FallEventTriggered = true;
            }
            basicMod.PhysicsMg.KeepFall();
            if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                stateMachine.ChangeState(basicMod.StateIdle);
                return;
            }
        }
        if (knockBackRight)
        {
            basicMod.PhysicsMg.MoveRight(knockBackPower);
            if (basicMod.BoundaryMg.CheckIsRightBounderyAndResetPos())
            {
                knockBackRight = false;
                basicMod.FaceDirectionMg.SetFaceLeft();
            }
        }
        else
        {
            basicMod.PhysicsMg.MoveLeft(knockBackPower);
            if (basicMod.BoundaryMg.CheckIsLeftBounderyAndResetPos())
            {
                knockBackRight = true;
                basicMod.FaceDirectionMg.SetFaceRight();
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
