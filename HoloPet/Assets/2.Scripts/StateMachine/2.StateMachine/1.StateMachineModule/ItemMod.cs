using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMod : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private ItemManager itemMg;
    public ItemManager ItemMg => itemMg;

    [Header("States")]
    [SerializeField] private StateBase stateItemUse;
    public StateBase StateItemUse => stateItemUse;
    [SerializeField] private StateBase stateHold;
    public StateBase StateHold => stateHold;

    private void Awake()
    {
        if (itemMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add itemMg in ItemHolderMod");
        }

        if (stateItemUse == null)
        {
            Debug.LogError(transform.root.name + "forget to add stateItemUse in ItemHolderMod");
        }

        if (stateHold == null)
        {
            Debug.LogError(transform.root.name + "forget to add stateHold in ItemHolderMod");
        }
    }
}
