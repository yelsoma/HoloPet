using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttack_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IAttackAbilitySM attackAbilitySM;
    private IItemHolderSM itemHolderSM;

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
        Debug.Log("hi");
        itemHolderSM.ItemHolderMg.GetItem().ChangeToItemUse();
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
