using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemHoldSM 
{
    //manager
    public ItemHoldManager ItemHoldMg { get; }
    //state
    public StateBase StateUse { get; }
    public StateBase StateHold { get; }
}
