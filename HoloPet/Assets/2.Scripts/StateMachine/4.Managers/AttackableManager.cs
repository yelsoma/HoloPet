using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableManager : MonoBehaviour
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IAttackableSM attackableSM;

    [SerializeField] private StateBase[] unAttackableState;
    private bool isAttckable;
    private float knockBackPower;
    private bool isKnockable;
    private bool isknockRight;
    [SerializeField] private StateBase StatePanic;
    private void Awake()
    {
        attackableSM = GetComponentInParent<IAttackableSM>();
        if(attackableSM == null)
        {
            Debug.Log(transform.name + "no IAttackableSM in parant");
        }

        stateMachine = GetComponentInParent<StateMachineBase>();
        if(stateMachine == null)
        {
            Debug.Log(transform.name + "no stateMachine in parant");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.Log(transform.name + "no IBasicSM in parant");
        }
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
    public void SetAttackKnockBack(float knockBackPower , bool knockRight)
    {
        this.knockBackPower = knockBackPower;
        this.isknockRight = knockRight;
        stateMachine.ChangeState(attackableSM.StateKnockBack);
    }
    public void AttackPanic()
    {
        if(StatePanic!= null)
        {
            if(stateMachine.GetStateNow() == basicSM.StateIdle)
            {
                stateMachine.ChangeState(StatePanic);
            }          
        }
    }
    public void AttackHP(int damage)
    {
        basicSM.ObjectStatMg.HpModify(- damage);
    }

    public float GetKnockBackPower()
    {
        return knockBackPower;
    }
    public bool GetIsAttackable()
    {
        return isAttckable;
    }
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
