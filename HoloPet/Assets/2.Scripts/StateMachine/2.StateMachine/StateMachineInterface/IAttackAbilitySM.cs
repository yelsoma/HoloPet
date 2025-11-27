using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackAbilitySM 
{
    //manager
    public AttackAbilityManager AttackAbilityMg { get; }
    // States
    public StateBase StateSearch { get; }
    public StateBase StateBasicAttack { get; }
}
