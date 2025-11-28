using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceStatManager : MonoBehaviour
{
    private StateMachineBase stateMachine;
    private AttackableMod attackableMod;

    [Header("Set this in Inspector")]
    [SerializeField] private int level = 1;

    [Header("Auto Calculated")]
    [SerializeField] private float hpMax;
    [SerializeField] private float hpNow;
    [SerializeField] private float atk;
    [SerializeField] private float atkSpeed;

    private const float baseHP = 100f;
    private const float baseATK = 10f;

    private void Awake()
    {
        IAttackableMod iAttackableMod = GetComponentInParent<IAttackableMod>();
        if (iAttackableMod == null)
        {
            Debug.LogError($"{name} ¡X iAttackableMod not found in parent.");
        }
        else
        {
            attackableMod = iAttackableMod.AttackableMod;
        }

        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.Log(transform.name + "no stateMachine in parant");
        }
    }
    private void Start()
    {
        ResetHP();
    }

    private void OnValidate()
    {
        hpMax = baseHP * Mathf.Pow(1.07f, level - 1);
        atk = baseATK * Mathf.Pow(1.05f, level - 1);
    }

    public float GetHP() => hpNow;
    public float GetATK() => atk;
    public float GetAtkSpeed() => atkSpeed;

    public void ResetHP()
    {
        hpNow = hpMax;
    }

    public void HpModify(int hpPlus)
    {
        hpNow += hpPlus;
        if (hpNow > hpMax)
        {
            hpNow = hpMax;
        }
        if (hpNow <= 0)
        {
            stateMachine.ChangeState(attackableMod.StateHpZero);
        }
        Debug.Log("hpNow" + hpNow);
    }
}
