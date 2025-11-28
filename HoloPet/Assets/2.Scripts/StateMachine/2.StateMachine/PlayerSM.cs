using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSM : StateMachineBase, IBasicMod, IMountableMod, IMountingAbilityMod, IRandomMoveMod, IInteractableMod, IInteractAbilityMod, IAttackableMod, IAttackAbilityMod ,IHoloMemFXMod ,IItemHolderMod ,IBattleMod
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

    [SerializeField] private HoloMemFXMod holoMemFXMod;
    public HoloMemFXMod HoloMemFXMod => holoMemFXMod;

    [SerializeField] private ItemHolderMod itemHolderMod;
    public ItemHolderMod ItemHolderMod => itemHolderMod;

    [SerializeField] private BattleMod battleMod;
    public BattleMod BattleMod => battleMod;

    protected override StateBase SetFirstState()
    {
        return BasicMod.StateSpawn;
    }
}
