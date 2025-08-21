using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchForVSM : StateMachineBase ,IBasicSM ,IAttackableSM,IMountingAbilitySM
{
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
    #region Attackable Manager 
    [SerializeField] private AttackableManager attackableMg;
    public AttackableManager AttackableMg => attackableMg;
    #endregion
    #region MountingAbility Manager
    [SerializeField] private MountingAbilityManager mountingAbilityMg;
    public MountingAbilityManager MountingAbilityMg => mountingAbilityMg;
    #endregion
    #region Basic State
    [SerializeField] private StateBase stateIdle;
    [SerializeField] private StateBase stateInAir;
    [SerializeField] private StateBase stateGrabbed;
    [SerializeField] private StateBase stateClicked;
    [SerializeField] private StateBase stateReleased;
    [SerializeField] private StateBase stateSpawn;
    [SerializeField] private StateBase stateDestroy;


    public StateBase StateIdle => stateIdle;
    public StateBase StateInAir => stateInAir;
    public StateBase StateGrabbed => stateGrabbed;
    public StateBase StateClicked => stateClicked;
    public StateBase StateReleased => stateReleased;
    public StateBase StateSpawn => stateSpawn;
    public StateBase StateDestroy => stateDestroy;
    #endregion
    #region Attackable State 
    [SerializeField] private StateBase stateHpZero;
    [SerializeField] private StateBase stateKnockBack;
    public StateBase StateHpZero => stateHpZero;
    public StateBase StateKnockBack => stateKnockBack;
    #endregion
    #region MountingAbility States
    [SerializeField] private StateBase stateMounting;
    public StateBase StateMounting => stateMounting;
    #endregion
    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }
}
