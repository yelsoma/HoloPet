using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemHolderSM 
{
    //manager
    public ItemHolderManager ItemHolderMg { get; }
    //state
    public StateBase StateItemAttack { get; }
}
