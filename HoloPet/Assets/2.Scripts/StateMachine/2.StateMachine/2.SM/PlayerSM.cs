using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSM : StateMachineBase, IBasicMod, IMountableMod, IMountingAbilityMod, IRandomMoveMod, IInteractableMod, IInteractAbilityMod, IAttackableMod, IAttackAbilityMod ,IFXMod ,IItemHolderMod ,IBattleMod
{
    #region Basic
    [SerializeField] private BasicMod basicMod;
    public BasicMod BasicMod => basicMod;
    #endregion
    #region Mountable
    [SerializeField] private MountableMod mountableMod;
    public MountableMod MountableMod => mountableMod;
    #endregion
    #region MountingAbility
    [SerializeField] private MountingAbilityMod mountingAbilityMod;
    public MountingAbilityMod MountingAbilityMod => mountingAbilityMod;
    #endregion
    #region RandomMove
    [SerializeField] private RandomMoveMod randomMoveMod;
    public RandomMoveMod RandomMoveMod => randomMoveMod;
    #endregion
    #region Interactable
    [SerializeField] private InteractableMod interactableMod;
    public InteractableMod InteractableMod => interactableMod;
    #endregion
    #region InteractAbility
    [SerializeField] private InteractAbilityMod interactAbilityMod;
    public InteractAbilityMod InteractAbilityMod => interactAbilityMod;
    #endregion
    #region Attackable
    [SerializeField] private AttackableMod attackableMod;
    public AttackableMod AttackableMod => attackableMod;
    #endregion
    #region AttackAbility
    [SerializeField] private AttackAbilityMod attackAbilityMod;
    public AttackAbilityMod AttackAbilityMod => attackAbilityMod;
    #endregion
    #region FX
    [SerializeField] private FXMod fXMod;
    public FXMod FXMod => fXMod;
    #endregion
    #region ItemHolder
    [SerializeField] private ItemHolderMod itemHolderMod;
    public ItemHolderMod ItemHolderMod => itemHolderMod;
    #endregion
    #region Battle
    [SerializeField] private BattleMod battleMod;
    public BattleMod BattleMod => battleMod;
    #endregion
    protected override StateBase SetFirstState()
    {
        return BasicMod.StateSpawn;
    }
    protected override StateBase StateOverride(StateBase requested)
    {        
        if(attackableMod != null && attackableMod.DefenceStatMg.GetHPNow() <= 0)
        {
            if(requested != attackableMod.StateHpZero && requested != basicMod.StateDestroy)
            {
                return basicMod.StateDestroy;
            }
        }
        if (battleMod != null && battleMod.GetIsInbattle())
        {
            return battleMod.OverrideToBattleState(requested);
        }
        return requested;
    }
}
