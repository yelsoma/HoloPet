using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSM : StateMachineBase, IBasicSM, IRandomMoveSM, IMountableSM, IMountingAbilitySM, IInteractableSM, IInteractAbilitySM
{
    [Header(" States")]

    #region Basic State
    [SerializeField] private StateBase stateIdle;
    [SerializeField] private StateBase stateInAir;
    [SerializeField] private StateBase stateGrabbed;
    [SerializeField] private StateBase stateClicked;
    [SerializeField] private StateBase stateReleased;
    [SerializeField] private StateBase stateSpawn;


    public StateBase StateIdle => stateIdle;
    public StateBase StateInAir => stateInAir;
    public StateBase StateGrabbed => stateGrabbed;
    public StateBase StateClicked => stateClicked;
    public StateBase StateReleased => stateReleased;
    public StateBase StateSpawn => stateSpawn;
    #endregion

    #region MountingAbility States
    [SerializeField] private StateBase stateMounting;
    public StateBase StateMounting => stateMounting;
    #endregion

    #region InteractAbility State 
    [SerializeField] private StateBase stateFollowTarget;
    public StateBase StateFollowTarget => stateFollowTarget;
    #endregion

    [Header(" Managers")]

    #region Basic Manager
    [Header("Basic Managers")]
    [SerializeField] private BoundaryManager boundaryMg;
    [SerializeField] private FaceDirectionManager faceDirectionMg;
    [SerializeField] private MovementManager movementMg;
    [SerializeField] private RaycastManager raycastMg;
    [SerializeField] private BaseDataManager baseDataMg;
    [SerializeField] private ClickableManager clickableMg;
    [SerializeField] private MonoBehaviour layerMg;

    public BoundaryManager BoundaryMg => boundaryMg;
    public FaceDirectionManager FaceDirectionMg => faceDirectionMg;
    public MovementManager MovementMg => movementMg;
    public RaycastManager RaycastMg => raycastMg;
    public BaseDataManager BaseDataMg => baseDataMg;
    public ClickableManager ClickableMg => clickableMg;
    public ILayerManager LayerMg => layerMg as ILayerManager;
    #endregion

    #region RandomMove Manager
    [SerializeField] private RandomMoveManager randomMoveMg;
    public RandomMoveManager RandomMoveMg => randomMoveMg;
    #endregion

    #region Mountable Manager
    [SerializeField] private MountableManager mountableMg;
    public MountableManager MountableMg => mountableMg;
    #endregion

    #region MountingAbility Manager
    [SerializeField] private MountingAbilityManager mountingAbilityMg;
    public MountingAbilityManager MountingAbilityMg => mountingAbilityMg;
    #endregion

    #region Interactable Manager
    [SerializeField] private InteractableManager interactableMg;
    public InteractableManager InteractableMg => interactableMg;
    #endregion

    #region InteractAbility Manager 
    [SerializeField] private InteractAbilityManager interactAbilityMg;
    public InteractAbilityManager InteractAbilityMg => interactAbilityMg;
    #endregion

    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }
}
