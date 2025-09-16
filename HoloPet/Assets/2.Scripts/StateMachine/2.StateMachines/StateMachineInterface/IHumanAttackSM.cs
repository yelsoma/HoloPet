using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHumanAttackSM 
{
    //manager
    public AttackAbilityManager AttackAbilityMg { get; }
    public ItemHolderManager ItemHolderMg { get; }
    // States
    public StateBase StateSearch { get; }
    public StateBase StateBasicAttack { get; }
    public StateBase StateMeleeAttack { get; }
    public StateBase StateRangeAttack { get; }
    public StateBase StateShieldAttack { get; }
}
