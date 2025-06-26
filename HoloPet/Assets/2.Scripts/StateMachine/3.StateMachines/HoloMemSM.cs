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

    public BoundaryManager BoundaryMg { get; private set; }
    public FaceDirectionManager FaceDirectionMg { get; private set; }
    public MovementManager MovementMg { get; private set; }
    public RaycastManager RaycastMg { get; private set; }
    public BaseDataManager BaseDataMg { get; private set; }
    public ClickableManager ClickableMg { get; private set; }
    public ILayerManager LayerMg { get; private set; }
    #endregion

    #region RandomMove
    public RandomMoveManager RandomMoveMg { get; private set; }
    #endregion

    #region Mounting
    [Header("Mounting States")]
    [SerializeField] private StateBase stateMounting;
    public StateBase StateMounting => stateMounting;

    public MountableManager MountableMg { get; private set; }
    public MountingAbilityManager MountingAbilityMg { get; private set; }
    #endregion

    #region Interact
    [Header("Interact States")]
    [SerializeField] private StateBase stateFollowTarget;
    public StateBase StateFollowTarget => stateFollowTarget;

    public InteractAbilityManager InteractAbilityMg { get; private set; }
    public InteractableManager InteractableMg { get; private set; }
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
        // Basic Managers
        BoundaryMg = GetComponentInChildren<BoundaryManager>();
        FaceDirectionMg = GetComponentInChildren<FaceDirectionManager>();
        MovementMg = GetComponentInChildren<MovementManager>();
        RaycastMg = GetComponentInChildren<RaycastManager>();
        BaseDataMg = GetComponentInChildren<BaseDataManager>();
        ClickableMg = GetComponentInChildren<ClickableManager>();
        LayerMg = GetComponentInChildren<ILayerManager>(true);

        if (BoundaryMg == null) Debug.LogError($"{name} ¡X BoundaryManager not found.");
        if (FaceDirectionMg == null) Debug.LogError($"{name} ¡X FaceDirectionManager not found.");
        if (MovementMg == null) Debug.LogError($"{name} ¡X MovementManager not found.");
        if (RaycastMg == null) Debug.LogError($"{name} ¡X RaycastManager not found.");
        if (BaseDataMg == null) Debug.LogError($"{name} ¡X BaseDataManager not found.");
        if (ClickableMg == null) Debug.LogError($"{name} ¡X ClickableManager not found.");
        if (LayerMg == null) Debug.LogError($"{name} ¡X ILayerManager not found in children.");

        // RandomMove
        RandomMoveMg = GetComponentInChildren<RandomMoveManager>();
        if (RandomMoveMg == null) Debug.LogError($"{name} ¡X RandomMoveManager not found.");

        // Mounting
        MountableMg = GetComponentInChildren<MountableManager>();
        if (MountableMg == null) Debug.LogError($"{name} ¡X MountableManager not found.");

        MountingAbilityMg = GetComponentInChildren<MountingAbilityManager>();
        if (MountingAbilityMg == null) Debug.LogError($"{name} ¡X MountingAbilityManager not found.");

        // Interact
        InteractAbilityMg = GetComponentInChildren<InteractAbilityManager>();
        if (InteractAbilityMg == null) Debug.LogError($"{name} ¡X InteractAbilityManager not found.");

        InteractableMg = GetComponentInChildren<InteractableManager>();
        if (InteractableMg == null) Debug.LogError($"{name} ¡X InteractableManager not found.");
    }
}
