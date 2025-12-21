using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableManager : MonoBehaviour
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private AttackableMod attackableMod;

    [SerializeField] private StateBase[] unAttackableState;
    private bool isAttckable;
    private float knockBackPower;
    private float knockUpPower;
    private bool isKnockable;
    private bool isknockRight;
    [SerializeField] private StateBase StatePanic;
    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
            Debug.LogError($"{transform.root.name} ¡X no StateMachine found in parent.");

        IBasicMod iBasicMod = stateMachine.transform.GetComponent<IBasicMod>();
        if (iBasicMod == null)
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        else
            basicMod = iBasicMod.BasicMod;

        IAttackableMod iAttackMod = stateMachine.transform.GetComponent<IAttackableMod>();
        if (iAttackMod == null)
            Debug.LogError($"{name} ¡X iAttackableMod not found in parent.");
        else
            attackableMod = iAttackMod.AttackableMod;
    }

    private void Start()
    {
        SetIsAttackable(true);
        SetIsKnockable(true);
        foreach (StateBase stateBase in unAttackableState)
        {
            stateBase.OnEnterState += StateBase_OnEnterState;
            stateBase.OnExitState += StateBase_OnExitState;
        }
    }

    public bool GetIsKnockable() => isKnockable;
    public StateMachineBase GetStateMachine() => stateMachine;
    public bool GetIsKnockRight() => isknockRight;
    public void SetIsKnockable(bool isKnockable)
    {
        this.isKnockable = isKnockable;
    }
    public void SetAttackKnockBack(float knockBackPower ,float knockUpPower, bool knockRight)
    {
        this.knockBackPower = knockBackPower;
        this.knockUpPower = knockUpPower;
        this.isknockRight = knockRight;
        stateMachine.ChangeState(attackableMod.StateKnockBack);
    }
    public void AttackPanic()
    {
        if(StatePanic!= null)
        {
            if(stateMachine.GetStateNow() == basicMod.StateIdle)
            {
                stateMachine.ChangeState(StatePanic);
            }          
        }
    }
    public void AttackHP(float damage)
    {
        attackableMod.DefenceStatMg.HpModify(-damage);
    }

    public float GetKnockBackPower() => knockBackPower;
    public float GetKnockUpPower() => knockUpPower;
    public bool GetIsAttackable() => isAttckable;

    public void SetIsAttackable(bool isAttckable)
    {
        this.isAttckable = isAttckable;
    }
    // event is attackable state 
    private void StateBase_OnExitState(object sender, System.EventArgs e)
    {
        isAttckable = true;
    }

    private void StateBase_OnEnterState(object sender, System.EventArgs e)
    {
        isAttckable = false;
    }
}
