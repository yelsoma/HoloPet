using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseInventory : MonoBehaviour
{
    [SerializeField] private List<ObjectDefinition> itemList;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private ObjectDefinition startObject;
    public event EventHandler OnItemListChange;


    public List<ObjectDefinition> GetItemList()
    {
        return itemList;
    }
    public void AddItemToInventoryList(ObjectDefinition inventoryItemData)
    {
        itemList.Add(inventoryItemData);
        OnItemListChange?.Invoke(this, EventArgs.Empty);
    }
    public void RemoveItemFromInventoryList(ObjectDefinition inventoryItemData)
    {
        if (itemList.Contains(inventoryItemData))
        {
            itemList.Remove(inventoryItemData);
        }
        OnItemListChange?.Invoke(this, EventArgs.Empty);
    }
    public void SetUIActive(bool isActive)
    {
        inventoryUI.gameObject.SetActive(isActive);
        inventoryUI.RefreshInventoryItem();
    }
    public void ClearItemList()
    {
        itemList.Clear();
        OnItemListChange?.Invoke(this, EventArgs.Empty);
    }
    public void InitializeInventory()
    {
        itemList.Clear();
        itemList.Add(startObject);
    }
}
