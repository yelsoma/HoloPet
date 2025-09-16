using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class HoloMemSM : StateMachineBase, IBasicSM, IRandomMoveSM, IMountableSM, IMountingAbilitySM, IInteractableSM, IInteractAbilitySM ,IAttackAbilitySM, IAttackableSM, ICreatureSM , IHoloMemFXSM, IHumanAttackSM
{
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
    [SerializeField] private TextLogManager textLogMg;

    public InteractAbilityManager InteractAbilityMg => interactAbilityMg;
    public TextLogManager TextLogMg => textLogMg;
    #endregion
    #region Attackable Manager 
    [SerializeField] private AttackableManager attackableMg;
    public AttackableManager AttackableMg => attackableMg;
    #endregion
    #region AttackAbility Manager 
    [SerializeField] private AttackAbilityManager attackAbilityMg;
    public AttackAbilityManager AttackAbilityMg => attackAbilityMg;
    #endregion
    #region AttackAbility Manager 
    [SerializeField] private HoloMemFX holoMemFXMg;
    public HoloMemFX HoloMemFXMg => holoMemFXMg;
    #endregion
    [SerializeField] private ItemHolderManager itemHolderMg;
    public ItemHolderManager ItemHolderMg => itemHolderMg;
    [Header(" States")]

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
    #region MountingAbility States
    [SerializeField] private StateBase stateMounting;
    public StateBase StateMounting => stateMounting;
    #endregion
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
    #region Attackable State 
    [SerializeField] private StateBase stateHpZero;
    [SerializeField] private StateBase stateKnockBack;
    public StateBase StateHpZero => stateHpZero;
    public StateBase StateKnockBack => stateKnockBack;
    #endregion
    #region Creature State 
    [SerializeField] private StateBase stateWander;
    public StateBase StateWander => stateWander;
    #endregion
    #region AttackAbility State
    [SerializeField] private StateBase stateSearch;
    [SerializeField] private StateBase stateBasicAttack;
    public StateBase StateSearch => stateSearch;
    public StateBase StateBasicAttack => stateBasicAttack;
    #endregion
    [SerializeField] private StateBase stateMeleeAttack;
    public StateBase StateMeleeAttack => stateMeleeAttack;
    [SerializeField] private StateBase stateRangeAttack;
    public StateBase StateRangeAttack => stateRangeAttack;
    [SerializeField] private StateBase stateShieldAttack;
    public StateBase StateShieldAttack => stateShieldAttack;

    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }
}
