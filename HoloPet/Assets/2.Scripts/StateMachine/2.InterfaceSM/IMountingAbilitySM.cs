using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMountingAbilitySM 
{
    public MountingAbilityManager MountingAbilityMg { get; }
    public StateBase StateMounting { get; }
}
