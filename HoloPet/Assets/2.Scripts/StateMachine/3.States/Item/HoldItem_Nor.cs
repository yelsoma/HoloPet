using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItem_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private ItemMod itemMod;

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
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        itemMod.ItemMg.EnterHold();
        TriggerAni1();
    }
    public override void StateUpdate()
    {
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
    }
    #endregion
}
