using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableManager : MonoBehaviour
{
    [SerializeField] private int hpMax;
    private int hp;
    [SerializeField] private StateBase[] unAttackableState;
    private bool isAttckable;
    private IAttackableSM attackableSM;
    private StateMachineBase stateMachine;
    private bool knockRight;
    [SerializeField] private float deathKnockBackPower;
    private float knockBackPower;
    private IBasicSM basicSM;
    private AttackAbilityManager attackerMg;
    private Coroutine StartDeath;
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
        ResetHp();
        SetIsAttackable(true);
    }
    public void ResetHp()
    {
        hp = hpMax;
    }
    public void HpModify(int i)
    {
        hp += i;
        if(hp <= 0)
        {
            hp = 0;
            knockBackPower = deathKnockBackPower;
            SetIsKnockBackRight();
            stateMachine.ChangeState(attackableSM.StateKnockBack);
        }
    }
    public int GetHp()
    {
        return hp;
    }
    public void AttackKnockBack(int damage, float knockBackPower)
    {
        this.knockBackPower = knockBackPower;
        hp += damage;
        if(hp < 0)
        {
            hp = 0;
        }
        SetIsKnockBackRight();
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
    public bool GetIsAttackable()
    {
        bool isAttackableState = true;
        if (unAttackableState.Length > 0)
        {
            foreach (StateBase stateBase in unAttackableState)
            {
                if(stateMachine.GetStateNow() == stateBase)
                {
                    isAttackableState = false;
                    break;
                }
            }
        }
        if(isAttackableState && isAttckable)
        {
            return true;
        }
        return false;
    }
    public void SetIsAttackable(bool isAttckable)
    {
        this.isAttckable = isAttckable;
    }
    private void SetIsKnockBackRight()
    {
        if(attackerMg == null)
        {
            knockRight = false;
            return;
        }
        if(attackerMg.transform.root.position.x <= transform.root.position.x)
        {
            knockRight = true;
        }
        else
        {
            knockRight = false;
        }
        
    }
    public void SetDeath(bool isDeath)
    {        
        if (isDeath)
        {
            if(StartDeath == null)
            {
                StartDeath = StartCoroutine(CoStartDeath());
            }          
        }
        else
        {
            StopCoroutine(StartDeath);
            StartDeath = null;
        }
    }
    private IEnumerator CoStartDeath()
    {
        SetIsAttackable(false);
        IInteractableSM interactableSM = GetComponentInParent<IInteractableSM>();
        if (interactableSM != null)
        {
            interactableSM.InteractableMg.SetIsInteractable(false);
        }
        while (!basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            yield return null;
        }
        stateMachine.ChangeState(attackableSM.StateHpZero);
        StartDeath = null;
    }
    public void SetAttacker(AttackAbilityManager attackAbilityManager)
    {
        attackerMg = attackAbilityManager;
    }
}
