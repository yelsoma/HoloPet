using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStateMachine : StateMachineBase ,IBasicMod ,IMountableMod ,IMountingAbilityMod ,IRandomMoveMod ,IInteractableMod ,IInteractAbilityMod ,IAttackableMod ,IAttackAbilityMod
{
    [SerializeField] private BasicMod basicMod;
    public BasicMod BasicMod => basicMod;

    [SerializeField] private MountableMod mountableMod;
    public MountableMod MountableMod => mountableMod;

    [SerializeField] private MountingAbilityMod mountingAbilityMod;
    public MountingAbilityMod MountingAbilityMod => mountingAbilityMod;

    [SerializeField] private RandomMoveMod randomMoveMod;
    public RandomMoveMod RandomMoveMod => randomMoveMod;

    [SerializeField] private InteractableMod interactableMod;
    public InteractableMod InteractableMod => interactableMod;

    [SerializeField] private InteractAbilityMod interactAbilityMod;
    public InteractAbilityMod InteractAbilityMod => interactAbilityMod;

    [SerializeField] private AttackableMod attackableMod;
    public AttackableMod AttackableMod => attackableMod;

    [SerializeField] private AttackAbilityMod attackAbilityMod;
    public AttackAbilityMod AttackAbilityMod => attackAbilityMod;

    protected override StateBase SetFirstState()
    {
        return BasicMod.StateSpawn;
    }    
}
