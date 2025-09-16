using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Melee,
    Shield,
    Ranged
}
public class ItemHolderManager : StateBase
{
    [SerializeField] Transform HoldPoint;
    StateMachineBase stateMachine;
    private ItemHoldManager itemHold;
    private bool isHoldingItem;
    private IHumanAttackSM humanAttackSM;
    [SerializeField] private StateBase knockUp;
    [SerializeField] private StateBase grab;
    [SerializeField] private StateBase inAir;
    [SerializeField] private StateBase click;
    [SerializeField] private StateBase spawn;
    [SerializeField] private StateBase release;
    [SerializeField] private StateBase melee;
    [SerializeField] private StateBase ranged;
    [SerializeField] private StateBase sheild;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError("no statemachinebase in " + transform);
        }
        if (HoldPoint == null)
        {
            Debug.LogError("forget to set MeleeholdPoint in " + transform);
        }
        humanAttackSM = GetComponentInParent<IHumanAttackSM>();
    }
    public ItemHoldManager GetItem()
    {
        return itemHold;
    }
    public Transform GetHoldPoint()
    {
        return HoldPoint;
    }
    public void SetItemHold(ItemHoldManager itemHold)
    {
        this.itemHold = itemHold;
    }
    public void RemoveItem()
    {
        itemHold = null;
    }
    public void SetIsHolding(bool isholding)
    {
        isHoldingItem = isholding;
    }
    public bool GetIsHolding()
    {
        return isHoldingItem;
    }
    public void GoToAttack()
    {
        if(stateMachine.GetStateNow() != humanAttackSM.StateSearch && stateMachine.GetStateNow() != knockUp && stateMachine.GetStateNow() != grab && stateMachine.GetStateNow() != inAir && stateMachine.GetStateNow() != click && stateMachine.GetStateNow() != spawn && stateMachine.GetStateNow() != release && stateMachine.GetStateNow() != melee && stateMachine.GetStateNow() != ranged && stateMachine.GetStateNow() != sheild)
        {
            stateMachine.ChangeState(humanAttackSM.StateSearch);
        }
    }
}
