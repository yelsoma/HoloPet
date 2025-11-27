using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStateMachine : StateMachineBase ,IBasicMod ,IMountableMod ,IMountingAbilityMod
{
    [SerializeField] private BasicMod basicMod;
    public BasicMod BasicMod => basicMod;

    [SerializeField] private MountableMod mountableMod;
    public MountableMod MountableMod => mountableMod;

    [SerializeField] private MountingAbilityMod mountingAbilityMod;
    public MountingAbilityMod MountingAbilityMod => mountingAbilityMod;   

    protected override StateBase SetFirstState()
    {
        return BasicMod.StateSpawn;
    }    
}
