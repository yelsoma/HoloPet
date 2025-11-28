using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse_Sword : StateBase
{
    private StateMachineBase stateMachine;
    private ItemMod itemMod;
    private LayerMask targetLayerMask;

    #region AutoSetRef
    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        }

        IItemMod iItemMod = GetComponentInParent<IItemMod>();
        if (iItemMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no iItemMod found in parent.");
        }
        else
        {
            itemMod = iItemMod.ItemMod;
        }
        itemMod.ItemMg.GetColliderScript().OnTriggerHitBox += ItemUse_Sword_OnTriggerHitBox;
    }


    #endregion

    #region StateBase
    public override void Enter()
    {
        TriggerAni1();
        itemMod.ItemMg.SetColliderActive(true);
        targetLayerMask = itemMod.ItemMg.GetTargetLayerMask();
    }
    public override void StateUpdate()
    {
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
        itemMod.ItemMg.SetColliderActive(false);
    }
    #endregion

    //event
    private void ItemUse_Sword_OnTriggerHitBox(object sender, ItemHitDetect.HitEventArgs e)
    {
        if (((1 << e.collider.gameObject.layer) & targetLayerMask.value) != 0)
        {
            // Layer is in the mask
            if (e.collider.transform.TryGetComponent<IAttackableMod>(out IAttackableMod attackableMod) &&
                attackableMod.AttackableMod.AttackableMg.GetIsAttackable())
            {
                attackableMod.AttackableMod.AttackableMg.AttackHP(10);
            }
        }
    }
}

