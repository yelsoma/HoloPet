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
