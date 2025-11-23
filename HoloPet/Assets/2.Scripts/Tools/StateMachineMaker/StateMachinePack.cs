using UnityEngine;

public class StateMachinePack 
{
    // Basic
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
    #region Basic Manager
    [Header("Basic Managers")]
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
    #endregion

    // RandomMove
    #region RandomMove Manager
    [SerializeField] private RandomMoveManager randomMoveMg;
    public RandomMoveManager RandomMoveMg => randomMoveMg;
    #endregion

    //CreatureState
    #region Creature State 
    [SerializeField] private StateBase stateWander;
    public StateBase StateWander => stateWander;
    #endregion

    // Mountable
    #region Mountable Manager
    [SerializeField] private MountableManager mountableMg;
    public MountableManager MountableMg => mountableMg;
    #endregion

    // MountingAbility
    #region MountingAbility States
    [SerializeField] private StateBase stateMounting;
    public StateBase StateMounting => stateMounting;
    #endregion
    #region MountingAbility Manager
    [SerializeField] private MountingAbilityManager mountingAbilityMg;
    public MountingAbilityManager MountingAbilityMg => mountingAbilityMg;
    #endregion

    // Interactable
    #region Interactable Manager
    [SerializeField] private InteractableManager interactableMg;
    public InteractableManager InteractableMg => interactableMg;
    #endregion

    // InteractAbility
    #region InteractAbility State 
    [SerializeField] private StateBase stateInteractThink;
    [SerializeField] private StateBase stateInteractFollowX;
    [SerializeField] private StateBase stateInteractFollowY;
    [SerializeField] private StateBase stateInteractFailed;
    public StateBase StateInteractThink => stateInteractThink;
    public StateBase StateInteractFollowX => stateInteractFollowX;
    public StateBase StateInteractFollowY => stateInteractFollowY;
    public StateBase StateInteractFailed => stateInteractFailed;
    #endregion
    #region InteractAbility Manager 
    [SerializeField] private InteractAbilityManager interactAbilityMg;
    [SerializeField] private TextLogManager textLogMg;

    public InteractAbilityManager InteractAbilityMg => interactAbilityMg;
    public TextLogManager TextLogMg => textLogMg;
    #endregion

    //Attackable
    #region Attackable Manager 
    [SerializeField] private AttackableManager attackableMg;
    public AttackableManager AttackableMg => attackableMg;
    #endregion
    #region Attackable State 
    [SerializeField] private StateBase stateHpZero;
    [SerializeField] private StateBase stateKnockBack;
    public StateBase StateHpZero => stateHpZero;
    public StateBase StateKnockBack => stateKnockBack;
    #endregion

    //AttackAbility
    #region AttackAbility Manager 
    [SerializeField] private AttackAbilityManager attackAbilityMg;
    public AttackAbilityManager AttackAbilityMg => attackAbilityMg;   
    #endregion
    #region AttackAbility State
    [SerializeField] private StateBase stateSearch;
    [SerializeField] private StateBase stateBasicAttack;

    public StateBase StateSearch => stateSearch;
    public StateBase StateBasicAttack => stateBasicAttack;

    #endregion

    //Drive
    #region Drive State
    [SerializeField] private StateBase stateDrive;
    [SerializeField] private StateBase stateDirveMax;
    [SerializeField] private StateBase stateDirveJump;
    [SerializeField] private StateBase stateClickedNor;

    public StateBase StateDrive => stateDrive;
    public StateBase StateDirveMax => stateDirveMax;
    public StateBase StateDirveJump => stateDirveJump;
    public StateBase StateClickedNor => stateClickedNor;
    #endregion

    //ItemHold
    #region ItemHolder Manager 
    [SerializeField] private ItemHolderManager itemHolderMg;
    public ItemHolderManager ItemHolderMg => itemHolderMg;
    #endregion
    #region ItemHolder State
    [SerializeField] private StateBase stateItemAttack;
    public StateBase StateItemAttack => stateItemAttack;
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
}
