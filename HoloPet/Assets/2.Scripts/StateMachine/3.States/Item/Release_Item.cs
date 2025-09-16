using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Release_Item : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IItemHoldSM itemHoldSM;
    private IMountingAbilitySM mountingAbilitySM;
    [SerializeField] private float checkDistanceDown;

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

        itemHoldSM = GetComponentInParent<IItemHoldSM>();
        if (itemHoldSM == null)
        {
            Debug.LogError($"{transform} ¡X no itemHoldSM found in parent.");
        }
    }
    public override void Enter()
    {
    }

    public override void StateUpdate()
    {
        if (itemHoldSM.ItemHoldMg.TrySetHolder(checkDistanceDown))
        {
            Debug.Log("startCS");
            stateMachine.ChangeState(itemHoldSM.StateHold);
            return;
        }
        if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicSM.StateIdle);
        }
        else
        {
            stateMachine.ChangeState(basicSM.StateInAir);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
