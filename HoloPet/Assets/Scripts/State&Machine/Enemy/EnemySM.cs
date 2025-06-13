using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySM : StateMachineBase , IBasicSM ,IMountableSM, IMountingAbilitySM ,IInteractAbilitySM ,IInteractableSM ,IRandomMoveSM
{
    #region Basic
    [Header("Basic Manager")]
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
    #endregion

    #region Mounting
    [Header("Mounting")]
    private IMountable mountable;
    private IMountingAbility mountingAbility;

    [SerializeField] private StateBase stateMounting;

    public IMountable Mountable => mountable;
    public IMountingAbility MountingAbility => mountingAbility;
    public StateBase StateMounting => stateMounting;
    #endregion

    #region Interact
    [Header("Interact")]
    private IInteractAbility interactAbiliy;
    private IInteractable interactable;

    [SerializeField] private StateBase stateFollowTarget;

    public IInteractAbility InteractAbiliy => interactAbiliy;
    public IInteractable Interactable => interactable;
    public StateBase StateFollowTarget => stateFollowTarget;
    #endregion

    #region Creature
    [Header("Creature")]
    [SerializeField] private StateBase stateWander;
    [SerializeField] private StateBase stateRandomMove;

    public StateBase StateWander => stateWander;
    public StateBase StateRandomMove => stateRandomMove;
    #endregion

    // Uncomment these if implemented
    /*
    [SerializeField] private StateBase stateAttack;
    [SerializeField] private StateBase stateKnockBack;
    [SerializeField] private StateBase stateDeath;
    */

    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }

    private void Awake()
    {
        layerManager = GetComponentInChildren<ILayerManager>();
        if (layerManager == null) Debug.LogError($"{name} ¡X ILayerManager not found in children.");

        mountable = GetComponentInChildren<IMountable>();
       // if (mountable == null) Debug.LogError($"{name} ¡X IMountable not found in children.");

        mountingAbility = GetComponentInChildren<IMountingAbility>();
       // if (mountingAbility == null) Debug.LogError($"{name} ¡X IMountingAbility not found in children.");

        interactAbiliy = GetComponentInChildren<IInteractAbility>();
      //  if (interactAbiliy == null) Debug.LogError($"{name} ¡X IInteractAbility not found in children.");

        interactable = GetComponentInChildren<IInteractable>();
       // if (interactable == null) Debug.LogError($"{name} ¡X IInteractable not found in children.");
    }
}
