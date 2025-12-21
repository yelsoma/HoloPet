using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ModulePack : MonoBehaviour
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
}
