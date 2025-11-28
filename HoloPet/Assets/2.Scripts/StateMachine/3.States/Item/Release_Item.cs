using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Release_Item : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private ItemMod itemMod;
    [SerializeField] private float checkDistanceDown;

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
    public override void Enter()
    {
    }

    public override void StateUpdate()
    {
        if (itemMod.ItemMg.TrySetHolderRayCast(checkDistanceDown))
        {
            stateMachine.ChangeState(itemMod.StateHold);
            return;
        }
        if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicMod.StateIdle);
        }
        else
        {
            stateMachine.ChangeState(basicMod.StateInAir);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
