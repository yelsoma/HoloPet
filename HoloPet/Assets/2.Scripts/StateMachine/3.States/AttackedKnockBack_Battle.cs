using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AniEnum;
using System;

public class AttackedKnockBack_Battle : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private AttackableMod attackableMod;
    private BattleMod battleMod;
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

        IAttackableMod iAttackableMod = GetComponentInParent<IAttackableMod>();
        if (iAttackableMod == null)
        {
            Debug.LogError($"{name} ¡X iAttackableMod not found in parent.");
        }
        else
        {
            attackableMod = iAttackableMod.AttackableMod;
        }
        IBattleMod ibattleMod = stateMachine.GetComponent<IBattleMod>();
        if(ibattleMod == null)
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        else
            battleMod = ibattleMod.BattleMod;
    }

    public override void Enter()
    {
        if (!attackableMod.AttackableMg.GetIsKnockable())
        {
            stateMachine.ChangeState(basicMod.StateIdle);
            return;
        }
        basicMod.PhysicsMg.SetJump(attackableMod.AttackableMg.GetKnockUpPower());
        basicMod.PhysicsMg.ResetFall();      
        knockBackRight = attackableMod.AttackableMg.GetIsKnockRight();
        if (knockBackRight)
        {
            basicMod.FaceDirectionMg.SetFaceLeft();
        }
        else
        {
            basicMod.FaceDirectionMg.SetFaceRight();
        }
        knockBackPower = attackableMod.AttackableMg.GetKnockBackPower();
        attackableMod.AttackableMg.SetIsKnockable(false);
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
                if (battleMod.GetIsInbattle())
                {
                    stateMachine.ChangeState(battleMod.BattleSearch);
                    return;
                }
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
        attackableMod.AttackableMg.SetIsKnockable(true);
    }
}
