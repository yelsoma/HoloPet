using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search_JumpEnemy : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private AttackAbilityMod attackAbilityMod;
    [SerializeField] private float searchDistance;
    [SerializeField] private float StartAttackDistance;
    [SerializeField] private float speedMultiply;
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpDelay;
    LayerMask playerLayerMask;
    private bool startJump;
    private bool keepSearch;

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

        IAttackAbilityMod iAttackAbilityMod = GetComponentInParent<IAttackAbilityMod>();
        if (iAttackAbilityMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no attackAbilityMod found in parent.");
        }
        else
        {
            attackAbilityMod = iAttackAbilityMod.AttackAbilityMod;
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        playerLayerMask = LayerMask.GetMask("Player");
        keepSearch = true;
        startJump = false;
        basicMod.PhysicsMg.ResetFall();
    }
    public override void StateUpdate()
    {
        if (keepSearch)
        {
            SetTarget();
        }
        else
        {
            if (startJump)
            {
                if (basicMod.FaceDirectionMg.GetIsFaceRight())
                {
                    basicMod.PhysicsMg.MoveRightMultiply(speedMultiply);
                    if (basicMod.BoundaryMg.CheckIsRightBounderyAndResetPos())
                    {
                        basicMod.FaceDirectionMg.SetFaceLeft();
                    }
                }
                else
                {
                    basicMod.PhysicsMg.MoveLeftMultiply(speedMultiply);
                    if (basicMod.BoundaryMg.CheckIsLeftBounderyAndResetPos())
                    {
                        basicMod.FaceDirectionMg.SetFaceRight();
                    }
                }
                if (basicMod.PhysicsMg.KeepJump())
                {
                    if (basicMod.BoundaryMg.CheckIsTopBounderyAndResetPos())
                    {
                        basicMod.PhysicsMg.SetJump(0);
                        basicMod.PhysicsMg.ResetFall();
                    }

                }
                else
                {
                    basicMod.PhysicsMg.KeepFall();
                    if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
                    {
                        startJump = false;
                        keepSearch = true;
                        basicMod.PhysicsMg.ResetFall();
                        TriggerAni2();//play idle Ani
                    }
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
    private void SetTarget()
    {
        if (attackAbilityMod.AttackAbilityMg.TrySetClosestAttackableHorizontal(searchDistance, playerLayerMask))
        {
            TriggerAni1();//start jump ani
            keepSearch = false;
            basicMod.PhysicsMg.SetJump(jumpPower);
            if (attackAbilityMod.AttackAbilityMg.GetIsTargetRight())
            {

                basicMod.FaceDirectionMg.SetFaceRight();
            }
            else
            {
                basicMod.FaceDirectionMg.SetFaceLeft();
                
            }
            if (attackAbilityMod.AttackAbilityMg.GetTargetDistance() <= StartAttackDistance)
            {
                stateMachine.ChangeState(attackAbilityMod.StateBasicAttack);
                return;
            }
        }
    }
    public void SearchJumpStart()
    {
        startJump = true;
    }
}
