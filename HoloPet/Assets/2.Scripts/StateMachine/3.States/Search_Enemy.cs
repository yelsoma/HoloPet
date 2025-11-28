using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search_Enemy : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private AttackAbilityMod attackAbilityMod;
    [SerializeField] private float searchDistance;
    [SerializeField] private float StartAttackDistance;
    [SerializeField] private float moveSpeed;
    LayerMask playerLayerMask;

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
    }
    public override void StateUpdate()
    {
        if (attackAbilityMod.AttackAbilityMg.TrySetClosestAttackableHorizontal(searchDistance, playerLayerMask))
        {
            if (attackAbilityMod.AttackAbilityMg.GetIsTargetRight())
            {
                basicMod.FaceDirectionMg.SetFaceRight();
                if (attackAbilityMod.AttackAbilityMg.GetTargetDistance() <= StartAttackDistance)
                {
                    stateMachine.ChangeState(attackAbilityMod.StateBasicAttack);
                    return;
                }
                basicMod.PhysicsMg.MoveRightMultiply(moveSpeed);
            }
            else
            {
                basicMod.FaceDirectionMg.SetFaceLeft();
                if (attackAbilityMod.AttackAbilityMg.GetTargetDistance() <= StartAttackDistance)
                {
                    stateMachine.ChangeState(attackAbilityMod.StateBasicAttack);
                    return;
                }
                basicMod.PhysicsMg.MoveLeftMultiply(moveSpeed);
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
}
