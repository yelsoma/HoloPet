using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolderMod : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private ItemHolderManager itemHolderManager;
    public ItemHolderManager ItemHolderMg => itemHolderManager;


    private void Awake()
    {
        if (itemHolderManager == null)
        {
            Debug.LogError(transform.root.name + "forget to add itemHolderManager in ItemHolderMod");
        }
    }
}
