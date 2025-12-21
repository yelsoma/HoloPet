using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartSM : StateMachineBase ,IBasicMod,IMountableMod,IAttackAbilityMod,IAttackableMod,IInteractableMod,IDriveMod
{
    #region Basic
    [SerializeField] private BasicMod basicMod;
    public BasicMod BasicMod => basicMod;
    #endregion
    #region Mountable
    [SerializeField] private MountableMod mountableMod;
    public MountableMod MountableMod => mountableMod;
    #endregion
    #region Attackable
    [SerializeField] private AttackableMod attackableMod;
    public AttackableMod AttackableMod => attackableMod;
    #endregion
    #region AttackAbility
    [SerializeField] private AttackAbilityMod attackAbilityMod;
    public AttackAbilityMod AttackAbilityMod => attackAbilityMod;
    #endregion
    #region Interactable
    [SerializeField] private InteractableMod interactableMod;
    public InteractableMod InteractableMod => interactableMod;
    #endregion
    #region Drive
    [SerializeField] private DriveMod driveMod;
    public DriveMod DriveMod => driveMod;
    #endregion
    protected override StateBase SetFirstState()
    {
        return basicMod.StateSpawn;
    }
}
