using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableManager : MonoBehaviour
{
    [SerializeField] private int hpMax;
    private int hp;
    [SerializeField] private StateBase[] unAttackableState;
    private IAttackableSM attackableSM;
    private StateMachineBase stateMachine;
    private bool knockRight;
    private float knockBackPower;

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
    }
    public void HpModify(int i)
    {
        hp += i;
    }
    public int GetHp()
    {
        return hp;
    }
    public void KnockBackRight(bool knockRight, float knockBackPower)
    {
        this.knockBackPower = knockBackPower;
        this.knockRight = knockRight;
        stateMachine.ChangeState(attackableSM.StateKnockBack);
    }
    public bool GetIsKnockRight()
    {
        return knockRight;
    }
    public float GetKnockBackPower()
    {
        return knockBackPower;
    }
    public bool GetIsAttackableState()
    {
        bool isAttackable = true;
        if (unAttackableState.Length > 0)
        {
            foreach (StateBase stateBase in unAttackableState)
            {
                if(stateMachine.GetStateNow() == stateBase)
                {
                    isAttackable = false;
                    break;
                }
            }
        }
        return isAttackable;
    }
}
