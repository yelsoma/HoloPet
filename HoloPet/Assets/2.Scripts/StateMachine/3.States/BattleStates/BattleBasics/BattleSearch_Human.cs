using System;
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
    [SerializeField] private float moveSpeedMultiply;

    private ObjectGangEnum targetGang;
    private AttackableManager targetAttackable;
    private bool isHolding;
    private float atkDistance;
    private bool targetSet;

    #region AutoSetRef
    private void Awake()
    {
        // StateMachine ---------------------------------------------------------
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
            return;
        }

        // IBasicMod ------------------------------------------------------------
        if (stateMachine.TryGetComponent<IBasicMod>(out var iBasicMod))
            basicMod = iBasicMod.BasicMod;
        else
            Debug.LogError($"{transform.root.name} ¡X no IBasicMod found on StateMachine.");

        // IAttackAbilityMod ----------------------------------------------------
        if (stateMachine.TryGetComponent<IAttackAbilityMod>(out var iAttackAbilityMod))
            attackAbilityMod = iAttackAbilityMod.AttackAbilityMod;
        else
            Debug.LogError($"{transform.root.name} ¡X no IAttackAbilityMod found on StateMachine.");

        // IItemHolderMod -------------------------------------------------------
        if (stateMachine.TryGetComponent<IItemHolderMod>(out var iItemHolderMod))
        {
            itemHolderMod = iItemHolderMod.ItemHolderMod;
            itemHolderMod.ItemHolderMg.OnChangeHold += ItemHolderMg_OnChangeHold;
        }
        else
        {
            Debug.LogError($"{transform.root.name} ¡X no IItemHolderMod found on StateMachine.");
        }

        // IBattleMod -----------------------------------------------------------
        if (stateMachine.TryGetComponent<IBattleMod>(out var iBattleMod))
            battleMod = iBattleMod.BattleMod;
        else
            Debug.LogError($"{transform.root.name} ¡X no IBattleMod found on StateMachine.");
    }
    #endregion


    #region StateBase
    public override void Enter()
    {
        // Set attack distance depending on holding item ------------------------
        if (itemHolderMod.ItemHolderMg.GetIsHolding())
        {
            atkDistance = itemHolderMod.ItemHolderMg.GetItem().GetAttackDistance();
            isHolding = true;
        }
        else
        {
            atkDistance = attackAbilityMod.OffenceStatMg.GetBaseAtkDistance();
            isHolding = false;
        }

        targetSet = false;
        basicMod.BoundaryMg.SetToBotBoundary();
        if(basicMod.ObjectDefinition.ObjectGangEnum == ObjectGangEnum.Enemy)
        {
            targetGang = ObjectGangEnum.Player;
        }
        else
        {
            targetGang = ObjectGangEnum.Enemy;
        }
    }

    public override void StateUpdate()
    {
        if (!targetSet)
        {
            targetAttackable = attackAbilityMod.AttackAbilityMg.TryGetTargetHorizantal(targetGang);
            if (targetAttackable != null)
            {
                targetSet = true;
                TriggerAni1();// start Search
            }          
            return;
        }     
        if(targetAttackable == null || targetAttackable.GetIsAttackable() == false)
        {
            targetSet = false;
            return;
        }
        bool targetIsRight = targetAttackable.GetStateMachine().transform.position.x >= stateMachine.transform.position.x;
        if (targetIsRight)
        {
            basicMod.FaceDirectionMg.SetFaceRight();
            if (attackAbilityMod.AttackAbilityMg.TryGetAttackableFront(targetGang, atkDistance, Vector2.right))
            {
                ChangeToAttack();
                return;
            }
            basicMod.PhysicsMg.MoveRightMultiply(moveSpeedMultiply);
        }
        else
        {
            basicMod.FaceDirectionMg.SetFaceLeft();

            if (attackAbilityMod.AttackAbilityMg.TryGetAttackableFront(targetGang, atkDistance, Vector2.left))
            {
                ChangeToAttack();
                return;
            }

            basicMod.PhysicsMg.MoveLeftMultiply(moveSpeedMultiply);
        }
    }

    public override void Exit()
    {
    }
    #endregion


    #region Helpers
    public void ChangeToAttack()
    {
        if (isHolding)
        {
            stateMachine.ChangeState(battleMod.BattleItemAttack);
        }
        else
        {
            stateMachine.ChangeState(battleMod.BattleBasicAttack);
        }
    }
    #endregion


    #region Event
    private void ItemHolderMg_OnChangeHold(object sender, EventArgs e)
    {
        if (itemHolderMod.ItemHolderMg.GetIsHolding())
        {
            atkDistance = itemHolderMod.ItemHolderMg.GetItem().GetAttackDistance();
            isHolding = true;
        }
        else
        {
            atkDistance = attackAbilityMod.OffenceStatMg.GetBaseAtkDistance();
            isHolding = false;
        }
    }
    #endregion
}
