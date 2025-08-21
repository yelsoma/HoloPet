using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatSM : StateMachineBase, IMountableSM ,IInteractableSM
{
    #region Mountable Manager
    [SerializeField] private MountableManager mountableMg;
    public MountableManager MountableMg => mountableMg;
    #endregion
    #region Interactable Manager
    [SerializeField] private InteractableManager interactableMg;
    public InteractableManager InteractableMg => interactableMg;
    #endregion  
    #region SeatStates
    [SerializeField] private StateBase stateSpawn;
    public StateBase StateSpawn => stateSpawn;
    [SerializeField] private StateBase stateDestroy;
    public StateBase StateDestroy => stateDestroy;
    #endregion
    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }
}

