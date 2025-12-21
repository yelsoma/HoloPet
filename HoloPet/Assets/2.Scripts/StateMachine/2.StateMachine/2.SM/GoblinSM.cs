using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSM : StateMachineBase, IBasicMod, IMountableMod, IMountingAbilityMod, IRandomMoveMod, IAttackableMod, IAttackAbilityMod, IItemHolderMod, IBattleMod ,IFXMod
{
    [SerializeField] private BasicMod basicMod;
    public BasicMod BasicMod => basicMod;

    [SerializeField] private MountableMod mountableMod;
    public MountableMod MountableMod => mountableMod;

    [SerializeField] private MountingAbilityMod mountingAbilityMod;
    public MountingAbilityMod MountingAbilityMod => mountingAbilityMod;

    [SerializeField] private RandomMoveMod randomMoveMod;
    public RandomMoveMod RandomMoveMod => randomMoveMod;

    [SerializeField] private AttackableMod attackableMod;
    public AttackableMod AttackableMod => attackableMod;

    [SerializeField] private AttackAbilityMod attackAbilityMod;
    public AttackAbilityMod AttackAbilityMod => attackAbilityMod;

    [SerializeField] private ItemHolderMod itemHolderMod;
    public ItemHolderMod ItemHolderMod => itemHolderMod;

    [SerializeField] private BattleMod battleMod;
    public BattleMod BattleMod => battleMod;

    #region FX
    [SerializeField] private FXMod fXMod;
    public FXMod FXMod => fXMod;
    #endregion

    protected override StateBase SetFirstState()
    {
        return BasicMod.StateSpawn;
    }
    protected override StateBase StateOverride(StateBase requested)
    {
        if (battleMod != null && battleMod.GetIsInbattle())
        {
            return battleMod.OverrideToBattleState(requested);
        }
        if (attackableMod != null && attackableMod.DefenceStatMg.GetHPNow() <= 0)
        {
            if (requested != attackableMod.StateHpZero && requested != basicMod.StateDestroy)
            {
                return basicMod.StateDestroy;
            }
        }
        return requested;
    }
}
