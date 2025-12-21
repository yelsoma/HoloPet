using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemType
{
    Melee,
    Shield,
    Ranged
}
public class ItemHolderManager : StateBase
{
    [SerializeField] private Transform HoldPoint;
    private ItemManager itemHold;
    private bool isHoldingItem;
    private bool isCanHoldState;
    public event EventHandler OnChangeHold;
    private StateMachineBase stateMachine;
    [SerializeField] private SpriteRenderer handBackSR;
    [SerializeField] private SpriteRenderer handFrontSR;
    private Sprite handFront;
    private Sprite handBack;

    private void Awake()
    {
        if (HoldPoint == null)
            Debug.LogError("forget to set ItemHoldPoint in " + transform.root.name);

        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
            Debug.LogError("no statemachineBase in " + transform.root.name);

        if (handFrontSR == null || handBackSR == null)
            Debug.LogError("no hand spriteRenderer in ItemHolderManager in " + transform.root.name);
        else
        {
            handFront = handFrontSR.sprite;
            handBack = handBackSR.sprite;
        }

        isCanHoldState = true;
        isHoldingItem = false;
    }
    public ItemManager GetItem()
    {
        return itemHold;
    }
    public Transform GetHoldPoint()
    {
        return HoldPoint;
    }
    public void SetItemHold(ItemManager itemHold)
    {
        this.itemHold = itemHold;
    }
    public void RemoveItem()
    {
        itemHold = null;
    }
    public void SetIsHolding(bool isHolding)
    {
        isHoldingItem = isHolding;
        handSpriteHide(isHolding);
        OnChangeHold?.Invoke(this,EventArgs.Empty);    
    }
    public bool GetIsHolding()
    {
        return isHoldingItem;
    }
    public Transform GetSMTransform()
    {
        return stateMachine.transform;
    }
    public Sprite GetHandFront()
    {
        return handFront;
    }
    public Sprite GetHandBack()
    {
        return handBack;
    }
    public void SetIsCanHoldState(bool canHold)
    {
        isCanHoldState = canHold;
    }
    public bool GetIsCanHoldState() => isCanHoldState;
    private void handSpriteHide( bool hideHand)
    {
        if (hideHand)
        {
            handFrontSR.sprite = null;
            handBackSR.sprite = null;
        }
        else
        {          
            handFrontSR.sprite = handFront;
            handBackSR.sprite = handBack;
        }
    }
}
