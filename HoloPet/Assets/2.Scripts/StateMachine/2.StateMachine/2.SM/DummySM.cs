using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySM : StateMachineBase ,IBasicMod ,IAttackableMod
{
    [SerializeField] private BasicMod basicMod;
    public BasicMod BasicMod => basicMod;

    [SerializeField] private AttackableMod attackableMod;
    public AttackableMod AttackableMod => attackableMod;

    protected override StateBase SetFirstState()
    {
        return BasicMod.StateSpawn;
    }
}
