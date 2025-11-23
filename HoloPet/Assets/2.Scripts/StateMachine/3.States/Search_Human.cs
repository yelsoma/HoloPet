using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search_Human : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IAttackAbilitySM attackAbilitySM;
    private IItemHolderSM itemHolderSM;
    [SerializeField] private float searchDistance;
    [SerializeField] private float moveSpeedMultiply;
    [SerializeField] LayerMask targetLayerMask;
    private bool isOnGround;
    private bool isHolding;
    private float atkDistance;
    private bool targetSet;
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
            Debug.LogError($"{transform.root.name} ¡X no attackAbilitySM found in parent.");
        }

        itemHolderSM = GetComponentInParent<IItemHolderSM>();
        if (itemHolderSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no itemHolderSM found in parent.");
        }
    }
    #endregion
    #region StateBase
    public override void Enter()
    {
        if (itemHolderSM.ItemHolderMg.GetIsHolding())
        {
            atkDistance = itemHolderSM.ItemHolderMg.GetItem().GetAttackDistance();
            isHolding = true;
        }
        else
        {
            atkDistance = 0.5f;
            isHolding = false;
        }
        targetSet = false;
    }
    public override void StateUpdate()
    {
        if (!basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            basicSM.PhysicsMg.KeepFall();
            return;
        }

        if (targetSet == false)
        {
            if (attackAbilitySM.AttackAbilityMg.TrySetClosestAttackableHorizontal(searchDistance, targetLayerMask))
            {
                targetSet = true;
            }
        }

        if (attackAbilitySM.AttackAbilityMg.GetIsTargetRight())
        {
            basicSM.FaceDirectionMg.SetFaceRight();
            basicSM.PhysicsMg.MoveRightMultiply(moveSpeedMultiply);
            if (basicSM.RaycastMg.GetFirstHit(stateMachine.transform.position, Vector2.right, atkDistance, targetLayerMask))
            {
                ChangeToAttack();
            }
        }
        else
        {
            basicSM.FaceDirectionMg.SetFaceLeft();
            basicSM.PhysicsMg.MoveLeftMultiply(moveSpeedMultiply);
            if (basicSM.RaycastMg.GetFirstHit(stateMachine.transform.position, Vector2.right, atkDistance, targetLayerMask))
            {
                ChangeToAttack();
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
    //pack metheod
    public void ChangeToAttack()
    {
        if (isHolding)
        {
            stateMachine.ChangeState(itemHolderSM.StateItemAttack);
            return;
        }
        else
        {
            stateMachine.ChangeState(attackAbilitySM.StateBasicAttack);
            return;
        }
    }
}
