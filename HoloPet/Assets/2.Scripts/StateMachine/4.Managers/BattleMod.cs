using System.Collections.Generic;
using UnityEngine;

public class BattleMod : MonoBehaviour
{
    [SerializeField] private StateBase battleSpawn;
    [SerializeField] private StateBase battleFall;
    [SerializeField] private StateBase battleStart;
    [SerializeField] private StateBase battleGrab;
    [SerializeField] private StateBase battleKnockBack;
    [SerializeField] private StateBase battleClicked;
    [SerializeField] private StateBase battleRelease;
    [SerializeField] private StateBase battleSearch;
    [SerializeField] private StateBase battleBasicAttack;
    [SerializeField] private StateBase battleItemAttack;
    [SerializeField] private StateBase hpZero;
    [SerializeField] private StateBase destroy;
    [SerializeField] private StateBase battleMounting;

    //not a inBattle state
    [SerializeField] private StateBase battleWin;
    public StateBase BattleWin => battleWin;

    public StateBase BattleFall => battleFall;
    public StateBase BattleStart => battleStart;
    public StateBase BattleKnockBack => battleKnockBack;
    public StateBase BattleClicked => battleClicked;
    public StateBase BattleRelease => battleRelease;
    public StateBase BattleSearch => battleSearch;
    public StateBase BattleBasicAttack => battleBasicAttack;
    public StateBase BattleItemAttack => battleItemAttack;
    public StateBase BattleMounting => battleMounting;
    private StateBase[] battleStates;
    private HashSet<StateBase> battleStateSet;

    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private DefenceStatManager defenceStatManager;
    private void Awake()
    {
        battleStates = new[]
        {
        battleSpawn,
        battleFall,
        battleStart,
        battleGrab,
        battleKnockBack,
        battleClicked,
        battleRelease,
        battleSearch,
        battleBasicAttack,
        battleItemAttack,
        hpZero,
        destroy,
        battleMounting
        };
        battleStateSet = new HashSet<StateBase>(battleStates);

        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError(transform.root.name + " no  StateMachine in parenet");
        }
        IBasicMod iBasicMod= stateMachine.GetComponent<IBasicMod>();
        if (iBasicMod == null)
            Debug.LogError(transform.root.name + " no  basicMod in parenet");
        else
            basicMod = iBasicMod.BasicMod;
        IAttackableMod iAttackableMod = stateMachine.GetComponent<IAttackableMod>();
        if (iAttackableMod == null)
            Debug.LogError(transform.root.name + " no  iAttackableMod in parenet");
        else
            defenceStatManager = iAttackableMod.AttackableMod.DefenceStatMg;
    }   
    
    public StateBase OverrideToBattleState(StateBase stateToCheck)
    {
        if (battleStateSet.Contains(stateToCheck))
            return stateToCheck;
        return battleStart;
    }

    public bool GetIsInbattle() => GameController.Instance.IsBattleActive;
    public void SetIsInBattle(bool isInBattle)
    {
        if (isInBattle)
        {
            if (stateMachine.GetStateNow() == battleMounting)
                return;
            stateMachine.ChangeState(BattleStart);
        }
        else
        {
            stateMachine.ChangeState(battleWin);
            defenceStatManager.ResetHP();
        }
    }
}
