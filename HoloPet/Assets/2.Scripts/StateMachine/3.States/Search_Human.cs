using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search_Human : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IAttackAbilitySM attackAbilitySM;
    private IHumanAttackSM humanAttackSM;
    [SerializeField] private float searchDistance;
    [SerializeField] private float punchDistance;
    private float startAttackDistance;
    [SerializeField] private float moveSpeedMultiply;
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] private AttackedKnockBack_Nor AttackedKnockBack_Nor;

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

        humanAttackSM = GetComponentInParent<IHumanAttackSM>();
        if(humanAttackSM == null)
        {
            Debug.LogError($"{transform} ¡X no humanAttackSM found in parent.");
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        if(basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos()== false)
        {
            basicSM.BoundaryMg.SetToBotBoundary();
        }
        if (humanAttackSM.ItemHolderMg.GetIsHolding())
        {
            ItemType itemType = humanAttackSM.ItemHolderMg.GetItem().GetItemType();
            if (itemType == ItemType.Melee)
            {
                startAttackDistance = humanAttackSM.ItemHolderMg.GetItem().GetAttackDistance();
            }
            if (itemType == ItemType.Ranged)
            {
                startAttackDistance = humanAttackSM.ItemHolderMg.GetItem().GetAttackDistance();
            }
            if (itemType == ItemType.Shield)
            {
                startAttackDistance = humanAttackSM.ItemHolderMg.GetItem().GetAttackDistance();
            }
        }       
    }
    public override void StateUpdate()
    {
        if (humanAttackSM.ItemHolderMg.GetIsHolding())
        {
            ItemType itemType = humanAttackSM.ItemHolderMg.GetItem().GetItemType();
            if (itemType == ItemType.Melee)
            {
                TriggerAni1();
            }
            if (itemType == ItemType.Ranged)
            {
                TriggerAni2();
            }
            if (itemType == ItemType.Shield)
            {
                TriggerAni3();
                AttackedKnockBack_Nor.SetKonckUp0(0f);
            }
        }
        
        if (attackAbilitySM.AttackAbilityMg.TrySetClosestAttackableHorizontal(searchDistance, targetLayerMask))
        {
            if (attackAbilitySM.AttackAbilityMg.GetIsTargetRight())
            {
                basicSM.FaceDirectionMg.SetFaceRight();
                basicSM.MovementMg.MoveRightMultiply(moveSpeedMultiply);
            }
            else
            {
                basicSM.FaceDirectionMg.SetFaceLeft();              
                basicSM.MovementMg.MoveLeftMultiply(moveSpeedMultiply);
            }
            if (attackAbilitySM.AttackAbilityMg.GetTargetDistance() <= startAttackDistance)
            {
                if (humanAttackSM.ItemHolderMg.GetIsHolding())
                {
                    ItemType itemType = humanAttackSM.ItemHolderMg.GetItem().GetItemType();
                    if (itemType == ItemType.Melee)
                    {
                        stateMachine.ChangeState(humanAttackSM.StateMeleeAttack);
                    }
                    if (itemType == ItemType.Ranged)
                    {
                        stateMachine.ChangeState(humanAttackSM.StateRangeAttack);
                    }
                    if (itemType == ItemType.Shield)
                    {
                        stateMachine.ChangeState(humanAttackSM.StateShieldAttack);
                    }
                }
                else
                {
                    stateMachine.ChangeState(attackAbilitySM.StateBasicAttack);
                    return;
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
}
