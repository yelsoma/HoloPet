using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItem_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicMod basicMod;
    private IItemSM itemHoldSM;

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

        itemHoldSM = GetComponentInParent<IItemSM>();
        if (itemHoldSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no itemHoldSM found in parent.");
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        itemHoldSM.ItemMg.EnterHold();
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
