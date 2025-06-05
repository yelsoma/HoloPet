using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSM : StateMachineBase ,IBasicSM
{
    #region Basic
    
    [Header("BasicManager")]
    [SerializeField] private BoundaryManager boundaryManager;
    [SerializeField] private FaceDirection faceDirection;
    [SerializeField] private Movement movement;
    [SerializeField] private RaycastManager raycastManager;
    [SerializeField] private ObjectBaseData baseData;
    [SerializeField] private ClickableManager clickableManager;
    private ILayerManager layerManager;
    public BoundaryManager BoundaryManager => boundaryManager;
    public FaceDirection FaceDirection => faceDirection;
    public Movement Movement => movement;
    public RaycastManager RaycastManager => raycastManager;
    public ObjectBaseData BaseData => baseData;
    public ClickableManager ClickableManager => clickableManager;
    public ILayerManager LayerManager => layerManager;

    [Header("BasicStates")]
    [SerializeField] private StateBase stateIdle;
    [SerializeField] private StateBase stateInAir;
    [SerializeField] private StateBase stateGrabed;
    [SerializeField] private StateBase stateClicked;
    [SerializeField] private StateBase stateSpawn;
    public StateBase StateIdle => stateIdle;
    public StateBase StateInAir => stateInAir;
    public StateBase StateGrabbed => stateGrabed;
    public StateBase StateClicked => stateClicked;
    public StateBase StateSpawn => stateSpawn;

    
    #endregion
    private void Awake()
    {
        layerManager = GetComponent<ILayerManager>();
    }
    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }
}
