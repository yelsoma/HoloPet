using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractAbilitySM 
{
    //states
    public StateBase StateInteractThink { get; }
    public StateBase StateInteractFollowX { get; }
    public StateBase StateInteractFollowY { get; }
    public StateBase StateInteractFailed { get; }
    //managers
    public InteractAbilityManager InteractAbilityMg { get; }    
    public TextLogManager TextLogMg { get; }
}
