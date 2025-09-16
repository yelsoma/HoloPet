using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search_JumpEnemy : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IAttackAbilitySM attackAbilitySM;
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
            Debug.LogError($"{transform} ¡X no StateMachineBase found in parent.");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform} ¡X no basicSM found in parent.");
        }

        attackAbilitySM = GetComponentInParent<IAttackAbilitySM>();
        if (attackAbilitySM == null)
        {
            Debug.LogError($"{transform} ¡X no IAttackAbilitySM found in parent.");
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        playerLayerMask = LayerMask.GetMask("Player");
        keepSearch = true;
        startJump = false;
        basicSM.MovementMg.ResetFall();
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
                if (basicSM.FaceDirectionMg.GetIsFaceRight())
                {
                    basicSM.MovementMg.MoveRightMultiply(speedMultiply);
                    if (basicSM.BoundaryMg.CheckIsRightBounderyAndResetPos())
                    {
                        basicSM.FaceDirectionMg.SetFaceLeft();
                    }
                }
                else
                {
                    basicSM.MovementMg.MoveLeftMultiply(speedMultiply);
                    if (basicSM.BoundaryMg.CheckIsLeftBounderyAndResetPos())
                    {
                        basicSM.FaceDirectionMg.SetFaceRight();
                    }
                }
                if (basicSM.MovementMg.KeepJump())
                {
                    if (basicSM.BoundaryMg.CheckIsTopBounderyAndResetPos())
                    {
                        basicSM.MovementMg.SetJump(0);
                        basicSM.MovementMg.ResetFall();
                    }

                }
                else
                {
                    basicSM.MovementMg.KeepFall();
                    if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
                    {
                        startJump = false;
                        keepSearch = true;
                        basicSM.MovementMg.ResetFall();
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
        if (attackAbilitySM.AttackAbilityMg.TrySetClosestAttackableHorizontal(searchDistance, playerLayerMask))
        {
            TriggerAni1();//start jump ani
            keepSearch = false;
            basicSM.MovementMg.SetJump(jumpPower);
            if (attackAbilitySM.AttackAbilityMg.GetIsTargetRight())
            {

                basicSM.FaceDirectionMg.SetFaceRight();
            }
            else
            {
                basicSM.FaceDirectionMg.SetFaceLeft();
                
            }
            if (attackAbilitySM.AttackAbilityMg.GetTargetDistance() <= StartAttackDistance)
            {
                stateMachine.ChangeState(attackAbilitySM.StateBasicAttack);
                return;
            }
        }
    }
    public void SearchJumpStart()
    {
        startJump = true;
    }
}
