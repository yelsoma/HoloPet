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
    [SerializeField] Transform HoldPoint;
    private ItemManager itemHold;
    private bool isHoldingItem;
    public event EventHandler OnChangeHold;

    private void Awake()
    {
        if (HoldPoint == null)
        {
            Debug.LogError("forget to set ItemHoldPoint in " + transform.root.name);
        }
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
    public void SetIsHolding(bool isholding)
    {
        isHoldingItem = isholding;
        OnChangeHold?.Invoke(this,EventArgs.Empty);    
    }
    public bool GetIsHolding()
    {
        return isHoldingItem;
    }
}
