using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMountingAbilitySM 
{
    public IMountingAbility MountingAbility { get; }
    public StateBase StateMounting { get; }
}
