using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class HoloMemSM : StateMachineBase, IBasicSM, IRandomMoveSM, IMountableSM, IMountingAbilitySM, IInteractableSM, IInteractAbilitySM
{
    #region Basic
    [Header("Basic States")]
    [SerializeField] private StateBase stateIdle;
    [SerializeField] private StateBase stateInAir;
    [SerializeField] private StateBase stateGrabbed;
    [SerializeField] private StateBase stateClicked;
    [SerializeField] private StateBase stateSpawn;

    public StateBase StateIdle => stateIdle;
    public StateBase StateInAir => stateInAir;
    public StateBase StateGrabbed => stateGrabbed;
    public StateBase StateClicked => stateClicked;
    public StateBase StateSpawn => stateSpawn;

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

    #region RandomMove
    [Header("RandomMove Manager")]
    [SerializeField] private RandomMoveManager myRandomMoveManager;
    public RandomMoveManager MyRandomMoveManager => myRandomMoveManager;
    #endregion

    #region Mounting
    [Header("Mounting States")]
    [SerializeField] private StateBase stateMounting;
    public StateBase StateMounting => stateMounting;

    [Header("Mounting Managers")]
    [SerializeField] private MountableManager mountableMg;
    [SerializeField] private MountingAbilityManager mountingAbilityMg;

    public MountableManager MountableMg => mountableMg;
    public MountingAbilityManager MountingAbilityMg => mountingAbilityMg;
    #endregion

    #region Interact
    [Header("Interact States")]
    [SerializeField] private StateBase stateFollowTarget;
    public StateBase StateFollowTarget => stateFollowTarget;

    [Header("Interact Managers")]
    [SerializeField] private InteractAbilityManager interactAbilityMg;
    [SerializeField] private InteractableManager interactableMg;

    public InteractAbilityManager InteractAbilityMg => interactAbilityMg;
    public InteractableManager InteractableMg => interactableMg;
    #endregion

    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }

    private void Awake()
    {
        AutoFill();
    }

    [ContextMenu("Auto-Fill Components From Children")]
    private void AutoFill()
    {
        // Basic
        boundaryMg = GetComponentInChildren<BoundaryManager>();
        faceDirectionMg = GetComponentInChildren<FaceDirectionManager>();
        movementMg = GetComponentInChildren<MovementManager>();
        raycastMg = GetComponentInChildren<RaycastManager>();
        baseDataMg = GetComponentInChildren<BaseDataManager>();
        clickableMg = GetComponentInChildren<ClickableManager>();

        layerMg = GetComponentsInChildren<MonoBehaviour>(true)
            .FirstOrDefault(mb => mb is ILayerManager);

        if (boundaryMg == null) Debug.LogError($"{name} ¡X BoundaryManager not found.");
        if (faceDirectionMg == null) Debug.LogError($"{name} ¡X FaceDirectionManager not found.");
        if (movementMg == null) Debug.LogError($"{name} ¡X MovementManager not found.");
        if (raycastMg == null) Debug.LogError($"{name} ¡X RaycastManager not found.");
        if (baseDataMg == null) Debug.LogError($"{name} ¡X BaseDataManager not found.");
        if (clickableMg == null) Debug.LogError($"{name} ¡X ClickableManager not found.");
        if (layerMg == null || !(layerMg is ILayerManager)) Debug.LogError($"{name} ¡X ILayerManager not found in children.");

        // RandomMove
        myRandomMoveManager = GetComponentInChildren<RandomMoveManager>();
        if (myRandomMoveManager == null) Debug.LogError($"{name} ¡X RandomMoveManager not found.");

        // Mounting
        mountableMg = GetComponentInChildren<MountableManager>();
        if (mountableMg == null) Debug.LogError($"{name} ¡X MountableManager not found.");

        mountingAbilityMg = GetComponentInChildren<MountingAbilityManager>();
        if (mountingAbilityMg == null) Debug.LogError($"{name} ¡X MountingAbilityManager not found.");

        // Interact
        interactAbilityMg = GetComponentInChildren<InteractAbilityManager>();
        if (interactAbilityMg == null) Debug.LogError($"{name} ¡X InteractAbilityManager not found.");

        interactableMg = GetComponentInChildren<InteractableManager>();
        if (interactableMg == null) Debug.LogError($"{name} ¡X InteractableManager not found.");
    }
}
