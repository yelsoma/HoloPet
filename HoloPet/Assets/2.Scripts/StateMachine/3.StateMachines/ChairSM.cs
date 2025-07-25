using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairSM : StateMachineBase ,IBasicSM ,IMountableSM
{
    #region Basic
    [Header("Basic States")]
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

    //need managers
    public BoundaryManager BoundaryMg { get; private set; }
    public FaceDirectionManager FaceDirectionMg { get; private set; }
    public MovementManager MovementMg { get; private set; }
    public RaycastManager RaycastMg { get; private set; }
    public BaseDataManager BaseDataMg { get; private set; }
    public ClickableManager ClickableMg { get; private set; }
    public ILayerManager LayerMg { get; private set; }
    #endregion

    #region Mounting
    public MountableManager MountableMg { get; private set; }
    #endregion

    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }
    private void Awake()
    {
        #region Basic Managers Set
        BoundaryMg = GetComponentInChildren<BoundaryManager>();
        FaceDirectionMg = GetComponentInChildren<FaceDirectionManager>();
        MovementMg = GetComponentInChildren<MovementManager>();
        RaycastMg = GetComponentInChildren<RaycastManager>();
        BaseDataMg = GetComponentInChildren<BaseDataManager>();
        ClickableMg = GetComponentInChildren<ClickableManager>();
        LayerMg = GetComponentInChildren<ILayerManager>();

        if (BoundaryMg == null) Debug.LogError($"{name} �X BoundaryManager not found in children.");
        if (FaceDirectionMg == null) Debug.LogError($"{name} �X FaceDirection not found.");
        if (MovementMg == null) Debug.LogError($"{name} �X Movement not found.");
        if (RaycastMg == null) Debug.LogError($"{name} �X RaycastManager not found.");
        if (BaseDataMg == null) Debug.LogError($"{name} �X ObjectBaseData not found.");
        if (ClickableMg == null) Debug.LogError($"{name} �X ClickableManager not found.");
        if (LayerMg == null) Debug.LogError($"{name} �X ILayerManager not found in children.");
        #endregion

        #region Mounting Managers Set
        MountableMg = GetComponentInChildren<MountableManager>();
        if (MountableMg == null) Debug.LogError($"{name} �X IMountable not found in children.");
        #endregion
    }
}
