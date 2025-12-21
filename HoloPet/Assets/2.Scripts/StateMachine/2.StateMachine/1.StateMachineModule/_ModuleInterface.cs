using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBasicMod 
{
    public BasicMod BasicMod { get; }
}
public interface IMountableMod
{
    public MountableMod MountableMod { get; }
}
public interface IMountingAbilityMod
{
    public MountingAbilityMod MountingAbilityMod { get; }
}
public interface IRandomMoveMod
{
    public RandomMoveMod RandomMoveMod { get; }
}
public interface IInteractableMod
{
    public InteractableMod InteractableMod { get; }
}
public interface IInteractAbilityMod
{
    public InteractAbilityMod InteractAbilityMod { get; }
}
public interface IAttackableMod
{
    public AttackableMod AttackableMod { get; }
}
public interface IAttackAbilityMod
{
    public AttackAbilityMod AttackAbilityMod { get; }
}
public interface IFXMod
{
    public FXMod FXMod { get; }
}
public interface IItemHolderMod
{
    public ItemHolderMod ItemHolderMod { get; }
}
public interface IItemMod
{
    public ItemMod ItemMod { get; }
}
public interface IBattleMod
{
    public BattleMod BattleMod { get; }
}
public interface IDriveMod
{
    public DriveMod DriveMod { get; }
}
