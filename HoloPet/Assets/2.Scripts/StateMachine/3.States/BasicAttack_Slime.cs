using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack_Slime : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
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

        IBasicMod ibasicMod = GetComponentInParent<IBasicMod>();
        if (ibasicMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
        else
        {
            basicMod = ibasicMod.BasicMod;
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
        basicMod.PhysicsMg.ResetFall();
        basicMod.PhysicsMg.SetJump(jumpUpPower);
        hit = false;
        startAttackjump = false;
    }
    public override void StateUpdate()
    {
        if (startAttackjump)
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
                basicMod.PhysicsMg.KeepFall();
                if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
                {
                    stateMachine.ChangeState(basicMod.StateIdle);
                }
            }

            if (hit == false)
            {
                if (basicMod.FaceDirectionMg.GetIsFaceRight())
                {
                    basicMod.PhysicsMg.MoveRight(jumpFrontPower);
                }
                else
                {
                    basicMod.PhysicsMg.MoveLeft(jumpFrontPower);
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
                if (basicMod.FaceDirectionMg.GetIsFaceRight())
                {
                    basicMod.PhysicsMg.MoveLeft(jumpFrontPower * hitBackSpeedMultiply);
                }
                else
                {
                    basicMod.PhysicsMg.MoveRight(jumpFrontPower * hitBackSpeedMultiply);
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
