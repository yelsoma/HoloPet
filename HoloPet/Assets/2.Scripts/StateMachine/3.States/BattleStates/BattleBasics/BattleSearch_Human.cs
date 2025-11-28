using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSearch_Human : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private AttackAbilityMod attackAbilityMod;
    private ItemHolderMod itemHolderMod;
    private BattleMod battleMod;
    [SerializeField] private float searchDistance;
    [SerializeField] private float moveSpeedMultiply;
    [SerializeField] LayerMask targetLayerMask;
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
        if(iAttackAbilityMod == null)
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

        IBattleMod iBattleMod = GetComponentInParent<IBattleMod>();
        if (iBattleMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no battleMod found in parent.");
        }
        else
        {
            battleMod = iBattleMod.BattleMod;
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
        basicMod.BoundaryMg.SetToBotBoundary();
    }
    public override void StateUpdate()
    {
        if (targetSet == false)
        {
            if (attackAbilityMod.AttackAbilityMg.TrySetClosestAttackableHorizontal(searchDistance, targetLayerMask))
            {
                Debug.Log("shit");
                targetSet = true;
                return;
            }
            return;
        }
        if (!attackAbilityMod.AttackAbilityMg.GetIsTargetAttackableSet()|| !attackAbilityMod.AttackAbilityMg.GetTarget().GetIsAttackable())
        {
            Debug.Log("hi");
            targetSet = false;
            return;
        }
        if (attackAbilityMod.AttackAbilityMg.GetIsTargetRight())
        {
            basicMod.FaceDirectionMg.SetFaceRight();           
            if (basicMod.RaycastMg.GetFirstHit(stateMachine.transform.position, Vector2.right, atkDistance, targetLayerMask))
            {
                ChangeToAttack();
            }
            basicMod.PhysicsMg.MoveRightMultiply(moveSpeedMultiply);
        }
        else
        {
            basicMod.FaceDirectionMg.SetFaceLeft();         
            if (basicMod.RaycastMg.GetFirstHit(stateMachine.transform.position, Vector2.left, atkDistance, targetLayerMask))
            {
                ChangeToAttack();
            }
            basicMod.PhysicsMg.MoveLeftMultiply(moveSpeedMultiply);
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
            stateMachine.ChangeState(battleMod.BattleItemAttack);
            return;
        }
        else
        {
            stateMachine.ChangeState(battleMod.BattleBasicAttack);
            return;
        }
    }
}
