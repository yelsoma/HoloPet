using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatSM : StateMachineBase, IMountableSM ,IInteractableSM ,ISeatSM
{
    #region Mountable Manager
    [SerializeField] private MountableManager mountableMg;
    public MountableManager MountableMg => mountableMg;
    #endregion
    #region Interactable Manager
    [SerializeField] private InteractableManager interactableMg;
    public InteractableManager InteractableMg => interactableMg;
    #endregion  
    #region SeatStatesManagers
    [SerializeField] private StateBase stateSpawn;
    public StateBase StateSpawn => stateSpawn;
    [SerializeField] private FaceDirectionManager faceDirectionManager;
    public FaceDirectionManager FaceDirectionManager => faceDirectionManager;
    #endregion

    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }
}

