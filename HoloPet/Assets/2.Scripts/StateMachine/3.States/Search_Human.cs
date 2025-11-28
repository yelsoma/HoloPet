using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search_Human : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private AttackAbilityMod attackAbilityMod;
    private ItemHolderMod itemHolderMod;
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

        IItemHolderMod iItemHolderMod = GetComponentInParent<IItemHolderMod>();
        if (iItemHolderMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no itemHolderSM found in parent.");
        }
        else
        {
            itemHolderMod = iItemHolderMod.ItemHolderMod;
        }
    }
    #endregion
    #region StateBase
    public override void Enter()
    {
        if (itemHolderMod.ItemHolderMg.GetIsHolding())
        {
            atkDistance = itemHolderMod.ItemHolderMg.GetItem().GetAttackDistance();
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
        if (!basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            basicMod.PhysicsMg.KeepFall();
            return;
        }

        if (targetSet == false)
        {
            if (attackAbilityMod.AttackAbilityMg.TrySetClosestAttackableHorizontal(searchDistance, targetLayerMask))
            {
                targetSet = true;
            }
        }

        if (attackAbilityMod.AttackAbilityMg.GetIsTargetRight())
        {
            basicMod.FaceDirectionMg.SetFaceRight();
            basicMod.PhysicsMg.MoveRightMultiply(moveSpeedMultiply);
            if (basicMod.RaycastMg.GetFirstHit(stateMachine.transform.position, Vector2.right, atkDistance, targetLayerMask))
            {
                ChangeToAttack();
            }
        }
        else
        {
            basicMod.FaceDirectionMg.SetFaceLeft();
            basicMod.PhysicsMg.MoveLeftMultiply(moveSpeedMultiply);
            if (basicMod.RaycastMg.GetFirstHit(stateMachine.transform.position, Vector2.right, atkDistance, targetLayerMask))
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
            stateMachine.ChangeState(itemHolderMod.StateItemAttack);
            return;
        }
        else
        {
            stateMachine.ChangeState(attackAbilityMod.StateBasicAttack);
            return;
        }
    }
}
