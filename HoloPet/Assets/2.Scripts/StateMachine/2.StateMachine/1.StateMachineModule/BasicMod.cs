using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMod : MonoBehaviour
{
    [Header("States")]
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

    [Header("Managers")]
    [SerializeField] private BoundaryManager boundaryMg;
    [SerializeField] private FaceDirectionManager faceDirectionMg;
    [SerializeField] private PhysicsManager physicsMg;
    [SerializeField] private RaycastManager raycastMg;
    [SerializeField] private BaseDataManager baseDataMg;
    [SerializeField] private ClickableManager clickableMg;
    [SerializeField] private MonoBehaviour layerMg;

    public BoundaryManager BoundaryMg => boundaryMg;
    public FaceDirectionManager FaceDirectionMg => faceDirectionMg;
    public PhysicsManager PhysicsMg => physicsMg;
    public RaycastManager RaycastMg => raycastMg;
    public BaseDataManager BaseDataMg => baseDataMg;
    public ClickableManager ClickableMg => clickableMg;
    public ILayerManager LayerMg => layerMg as ILayerManager;

    private void Awake()
    {
        if (stateIdle == null)
        {
            Debug.LogError(transform.root.name + "forget to add stateIdle in BasicMod");
        }
        if (stateInAir == null)
        {
            Debug.LogError(transform.root.name + "forget to add  stateInAir in BasicMod ");
        }       
        if (stateGrabbed == null)
        {
            Debug.LogError(transform.root.name + "forget to add  stateGrabbed in BasicMod ");
        }
        if (stateClicked == null)
        {
            Debug.LogError(transform.root.name + "forget to add  stateClicked in BasicMod ");
        }
        if (stateReleased == null)
        {
            Debug.LogError(transform.root.name + "forget to add  stateReleased in BasicMod ");
        }      
        if (stateSpawn == null)
        {
            Debug.LogError(transform.root.name + "forget to add  stateSpawn in BasicMod ");
        }
        if (stateDestroy == null)
        {
            Debug.LogError(transform.root.name + "forget to add   stateDestroy in BasicMod ");
        }

        if (boundaryMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add  boundaryMg in BasicMod ");
        }
        if (faceDirectionMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add  faceDirectionMg in BasicMod ");
        }
        if (physicsMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add  physicsMg in BasicMod ");
        }
        if (raycastMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add  raycastMg in BasicMod ");
        }
        if (baseDataMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add  baseDataMg in BasicMod ");
        }        
        if (clickableMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add  clickableMg in BasicMod ");
        }
        if (layerMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add  layerMg in BasicMod ");
        }
    }
}
