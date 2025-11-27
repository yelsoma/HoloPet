using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse_Sword : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicMod basicMod;
    private IItemSM itemSM;
    private LayerMask targetLayerMask;

    #region AutoSetRef
    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        }

        basicMod = GetComponentInParent<IBasicMod>();
        if (basicMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }

        itemSM = GetComponentInParent<IItemSM>();
        if (itemSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no itemHoldSM found in parent.");
        }
        itemSM.ItemMg.GetColliderScript().OnTriggerHitBox += ItemUse_Sword_OnTriggerHitBox;
    }


    #endregion

    #region StateBase
    public override void Enter()
    {
        TriggerAni1();
        itemSM.ItemMg.SetColliderActive(true);
        targetLayerMask = itemSM.ItemMg.GetTargetLayerMask();
    }
    public override void StateUpdate()
    {
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
        itemSM.ItemMg.SetColliderActive(false);
    }
    #endregion

    //event
    private void ItemUse_Sword_OnTriggerHitBox(object sender, ItemHitDetect.HitEventArgs e)
    {
        if (((1 << e.collider.gameObject.layer) & targetLayerMask.value) != 0)
        {
            // Layer is in the mask
            if (e.collider.transform.TryGetComponent<IAttackableSM>(out IAttackableSM attackableSM) &&
                attackableSM.AttackableMg.GetIsAttackable())
            {
                attackableSM.AttackableMg.AttackHP(10);
            }
        }
    }
}

