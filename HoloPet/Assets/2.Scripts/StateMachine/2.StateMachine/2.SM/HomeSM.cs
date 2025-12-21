using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSM : StateMachineBase, IBasicMod,IMountableMod
{
    #region Basic
    [SerializeField] private BasicMod basicMod;
    public BasicMod BasicMod => basicMod;
    #endregion
    #region Mountable
    [SerializeField] private MountableMod mountableMod;
    public MountableMod MountableMod => mountableMod;
    #endregion

    protected override StateBase SetFirstState()
    {
        return BasicMod.StateSpawn;
    }
}
