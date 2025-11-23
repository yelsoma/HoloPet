using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemSM 
{
    //manager
    public ItemManager ItemMg { get; }
    //state
    public StateBase StateItemUse { get; }
    public StateBase StateHold { get; }
}
