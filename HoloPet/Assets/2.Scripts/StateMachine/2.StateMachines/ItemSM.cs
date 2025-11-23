using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSM : StateMachineBase, IBasicSM, IItemSM
{
    #region Basic Manager
    [Header("Basic Managers")]
    [SerializeField] private BoundaryManager boundaryMg;
    [SerializeField] private FaceDirectionManager faceDirectionMg;
    [SerializeField] private PhysicsManager physicsMg;
    [SerializeField] private RaycastManager raycastMg;
    [SerializeField] private BaseDataManager baseDataMg;
    [SerializeField] private ClickableManager clickableMg;
    [SerializeField] private MonoBehaviour layerMg;
    [SerializeField] private ObjectStatManager objectStatMg;

    public BoundaryManager BoundaryMg => boundaryMg;
    public FaceDirectionManager FaceDirectionMg => faceDirectionMg;
    public PhysicsManager PhysicsMg => physicsMg;
    public RaycastManager RaycastMg => raycastMg;
    public BaseDataManager BaseDataMg => baseDataMg;
    public ClickableManager ClickableMg => clickableMg;
    public ILayerManager LayerMg => layerMg as ILayerManager;
    public ObjectStatManager ObjectStatMg => objectStatMg;
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
    #region Item Manager 
    [SerializeField] private ItemManager itemMg;
    public ItemManager ItemMg => itemMg;
    #endregion
    #region Item State
    [SerializeField] private StateBase stateItemUse;
    [SerializeField] private StateBase stateHold;
    public StateBase StateItemUse => stateItemUse;
    public StateBase StateHold => stateHold;
    #endregion
    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }
    
}
