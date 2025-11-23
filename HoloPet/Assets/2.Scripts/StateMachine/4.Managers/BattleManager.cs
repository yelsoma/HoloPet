using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private StateBase battleFall;
    [SerializeField] private StateBase battleStart;
    [SerializeField] private StateBase battleKnockBack;
    [SerializeField] private StateBase battleClicked;
    [SerializeField] private StateBase battleRelease;
    [SerializeField] private StateBase battleSearch;
    [SerializeField] private StateBase battleBasicAttack;
    [SerializeField] private StateBase battleItemAttack;

    public StateBase BattleFall => battleFall;
    public StateBase BattleStart => battleStart;
    public StateBase BattleKnockBack => battleKnockBack;
    public StateBase BattleClicked => battleClicked;
    public StateBase BattleRelease => battleRelease;
    public StateBase BattleSearch => battleSearch;
    public StateBase BattleBasicAttack => battleBasicAttack;
    public StateBase BattleItemAttack => battleItemAttack;
    private StateBase[] battleStates;
    private bool isInBattle;
    private StateMachineBase stateMachine;
    private void Awake()
    {
        battleStates = new[]
        {
        battleFall,
        battleStart,
        battleKnockBack,
        battleClicked,
        battleRelease,
        battleSearch,
        battleBasicAttack,
        battleItemAttack
        };

        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError(transform.root.name + " no  StateMachine in parenet");
        }
    }    
    public void CheckAndEnterBattleState()
    {
        StateBase stateNow = stateMachine.GetStateNow();
        foreach(var state in battleStates)
        {
            if(state == stateNow)
            {
                return;
            }
        }
        stateMachine.ChangeState(battleStart);
    }

    public bool GetIsInbattle() => isInBattle;
    public void SetIsInBattle(bool isInBattle)
    {
        this.isInBattle = isInBattle;
        stateMachine.ChangeState(BattleStart);
    }
    private void Start()
    {
        isInBattle = false;
    }
}
