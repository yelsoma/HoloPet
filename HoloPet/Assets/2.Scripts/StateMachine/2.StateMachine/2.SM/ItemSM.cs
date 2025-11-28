using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSM : StateMachineBase ,IBasicMod,IItemMod
{
    [SerializeField] private BasicMod basicMod;
    public BasicMod BasicMod => basicMod;

    [SerializeField] private ItemMod itemMod;
    public ItemMod ItemMod => itemMod;

    protected override StateBase SetFirstState()
    {
        return BasicMod.StateSpawn;
    }
}
