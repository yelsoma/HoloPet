using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class HoloMemSM : StateMachineBase, IBasicSM, IRandomMoveSM, IMountableSM, IMountingAbilitySM, IInteractableSM, IInteractAbilitySM
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

        var layerMono = GetComponentInChildren<ILayerManager>(true) as MonoBehaviour;
        layerMg = layerMono;

        if (boundaryMg == null) Debug.LogError($"{name} ¡X BoundaryManager not found.");
        if (faceDirectionMg == null) Debug.LogError($"{name} ¡X FaceDirectionManager not found.");
        if (movementMg == null) Debug.LogError($"{name} ¡X MovementManager not found.");
        if (raycastMg == null) Debug.LogError($"{name} ¡X RaycastManager not found.");
        if (baseDataMg == null) Debug.LogError($"{name} ¡X BaseDataManager not found.");
        if (clickableMg == null) Debug.LogError($"{name} ¡X ClickableManager not found.");
        if (layerMg == null) Debug.LogError($"{name} ¡X ILayerManager not found in children.");

        // RandomMove
        randomMoveMg = GetComponentInChildren<RandomMoveManager>();
        if (randomMoveMg == null) Debug.LogError($"{name} ¡X RandomMoveManager not found.");

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
