using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack_Slime : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IAttackAbilitySM attackAbilitySM;
    LayerMask playerLayerMask;
    [SerializeField] private float jumpUpPower;
    [SerializeField] private float jumpFrontPower;
    [SerializeField] private float hitDistance;
    [SerializeField] private float hitBackSpeedMultiply;
    private bool hit;
    private bool startAttackjump;

    #region AutoSetRef
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

        attackAbilitySM = GetComponentInParent<IAttackAbilitySM>();
        if (attackAbilitySM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no IAttackAbilitySM found in parent.");
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        playerLayerMask = LayerMask.GetMask("Player");
        basicSM.PhysicsMg.ResetFall();
        basicSM.PhysicsMg.SetJump(jumpUpPower);
        hit = false;
        startAttackjump = false;
    }
    public override void StateUpdate()
    {
        if (startAttackjump)
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
                basicSM.PhysicsMg.KeepFall();
                if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
                {
                    stateMachine.ChangeState(basicSM.StateIdle);
                }
            }

            if (hit == false)
            {
                if (basicSM.FaceDirectionMg.GetIsFaceRight())
                {
                    basicSM.PhysicsMg.MoveRight(jumpFrontPower);
                }
                else
                {
                    basicSM.PhysicsMg.MoveLeft(jumpFrontPower);
                }

                //if (attackAbilitySM.AttackAbilityMg.TrySetClosestAttackableHorizontal(-0.3f, hitDistance, playerLayerMask))
                //{
                //    attackAbilitySM.AttackAbilityMg.GetTarget().SetAttacker(attackAbilitySM.AttackAbilityMg);
                //    attackAbilitySM.AttackAbilityMg.GetTarget().AttackKnockBack(0, 1);
                //    hit = true;
                //    basicSM.MovementMg.SetJump(jumpUpPower);
                //    TriggerAni1();
                //}
            }
            else
            {
                if (basicSM.FaceDirectionMg.GetIsFaceRight())
                {
                    basicSM.PhysicsMg.MoveLeft(jumpFrontPower * hitBackSpeedMultiply);
                }
                else
                {
                    basicSM.PhysicsMg.MoveRight(jumpFrontPower * hitBackSpeedMultiply);
                }
            }
        }      
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
    }
    #endregion
    public void StartAttackJump()
    {
        startAttackjump = true;
    }
}
