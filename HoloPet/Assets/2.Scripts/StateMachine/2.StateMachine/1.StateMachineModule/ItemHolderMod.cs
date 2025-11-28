using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolderMod : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private ItemHolderManager itemHolderManager;
    public ItemHolderManager ItemHolderMg => itemHolderManager;

    [Header("States")]
    [SerializeField] private StateBase stateItemAttack;
    public StateBase StateItemAttack => stateItemAttack;

    private void Awake()
    {
        if (itemHolderManager == null)
        {
            Debug.LogError(transform.root.name + "forget to add itemHolderManager in ItemHolderMod");
        }

        if (stateItemAttack == null)
        {
            Debug.LogError(transform.root.name + "forget to add stateItemAttack in ItemHolderMod");
        }
    }
}
