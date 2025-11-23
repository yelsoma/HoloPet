using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search_Enemy : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IAttackAbilitySM attackAbilitySM;
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
    }
    public override void StateUpdate()
    {
        if (attackAbilitySM.AttackAbilityMg.TrySetClosestAttackableHorizontal(searchDistance, playerLayerMask))
        {
            if (attackAbilitySM.AttackAbilityMg.GetIsTargetRight())
            {
                basicSM.FaceDirectionMg.SetFaceRight();
                if (attackAbilitySM.AttackAbilityMg.GetTargetDistance() <= StartAttackDistance)
                {
                    stateMachine.ChangeState(attackAbilitySM.StateBasicAttack);
                    return;
                }
                basicSM.PhysicsMg.MoveRightMultiply(moveSpeed);
            }
            else
            {
                basicSM.FaceDirectionMg.SetFaceLeft();
                if (attackAbilitySM.AttackAbilityMg.GetTargetDistance() <= StartAttackDistance)
                {
                    stateMachine.ChangeState(attackAbilitySM.StateBasicAttack);
                    return;
                }
                basicSM.PhysicsMg.MoveLeftMultiply(moveSpeed);
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
