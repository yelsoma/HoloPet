using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSearch_Human : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IAttackAbilitySM attackAbilitySM;
    private IItemHolderSM itemHolderSM;
    private BattleManager battleManager;
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

        battleManager = GetComponentInParent<BattleManager>();
        if (battleManager == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no battleManager found in parent.");
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
        basicSM.BoundaryMg.SetToBotBoundary();
    }
    public override void StateUpdate()
    {
        if (targetSet == false)
        {
            if (attackAbilitySM.AttackAbilityMg.TrySetClosestAttackableHorizontal(searchDistance, targetLayerMask))
            {
                Debug.Log("shit");
                targetSet = true;
                return;
            }
            return;
        }
        if (!attackAbilitySM.AttackAbilityMg.GetIsTargetAttackableSet()|| !attackAbilitySM.AttackAbilityMg.GetTarget().GetIsAttackable())
        {
            Debug.Log("hi");
            targetSet = false;
            return;
        }
        if (attackAbilitySM.AttackAbilityMg.GetIsTargetRight())
        {
            basicSM.FaceDirectionMg.SetFaceRight();           
            if (basicSM.RaycastMg.GetFirstHit(stateMachine.transform.position, Vector2.right, atkDistance, targetLayerMask))
            {
                ChangeToAttack();
            }
            basicSM.PhysicsMg.MoveRightMultiply(moveSpeedMultiply);
        }
        else
        {
            basicSM.FaceDirectionMg.SetFaceLeft();         
            if (basicSM.RaycastMg.GetFirstHit(stateMachine.transform.position, Vector2.left, atkDistance, targetLayerMask))
            {
                ChangeToAttack();
            }
            basicSM.PhysicsMg.MoveLeftMultiply(moveSpeedMultiply);
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
            stateMachine.ChangeState(battleManager.BattleItemAttack);
            return;
        }
        else
        {
            stateMachine.ChangeState(battleManager.BattleBasicAttack);
            return;
        }
    }
}
