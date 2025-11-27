using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackableSM 
{
    // Managers
    public AttackableManager AttackableMg { get; }

    // States
    public StateBase StateHpZero { get; }
    public StateBase StateKnockBack { get; }
}
