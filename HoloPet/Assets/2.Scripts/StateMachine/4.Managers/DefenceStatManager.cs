using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceStatManager : MonoBehaviour
{
    private StateMachineBase stateMachine;
    private AttackableMod attackableMod;
    private AFKManager afKManager;
    private ObjectDefinition def;
    private float multiplier;

    [Header("Set this in Inspector")]
    [SerializeField] private int level = 1;
    [SerializeField] private float baseHP = 100f;

    [Header("Auto Calculated")]
    [SerializeField] private float hpMax;
    [SerializeField] private float hpNow;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
            Debug.LogError($"{transform.root.name} ¡X no StateMachine found in parent.");

        IAttackableMod iAttackMod = stateMachine.transform.GetComponent<IAttackableMod>();
        if (iAttackMod == null)
            Debug.LogError($"{name} ¡X iAttackableMod not found in parent.");
        else
            attackableMod = iAttackMod.AttackableMod;

        IBasicMod iBasicMod = stateMachine.transform.GetComponent<IBasicMod>();
        if (iBasicMod == null)
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        else
            def = iBasicMod.BasicMod.ObjectDefinition;
    }

    private void AfKManager_OnLevelUp(object sender, System.EventArgs e)
    {
        SetLevel();
        ResetHP();
    }
    private void Start()
    {
        multiplier = 1.09f;
        afKManager = GameController.Instance.AFKManager;
        afKManager.OnLevelUp += AfKManager_OnLevelUp;
        SetLevel();
        ResetHP();
    }

    private void OnValidate()
    {
        hpMax = baseHP * Mathf.Pow(multiplier, level - 1);
    }

    public float GetHPNow() => hpNow;
    public float GetHPMax() => hpMax;

    public void ResetHP()
    {
        HpModify(hpMax);
    }

    public void HpModify(float hpPlus)
    {
        hpNow += hpPlus;
        if (hpNow > hpMax)
        {
            hpNow = hpMax;
        }
        if (hpNow <= 0)
        {
            hpNow = 0;
            stateMachine.ChangeState(attackableMod.StateHpZero);
        }
        attackableMod.HeathBarMg.SetHealtBar(hpNow / hpMax);
    }    
    private void SetLevel()
    {
        if (def.ObjectGangEnum == ObjectGangEnum.Player)
        {
            level = afKManager.GetHomeLevel;
        }
        else if (def.ObjectGangEnum == ObjectGangEnum.Enemy)
        {           
            int worldLevel = afKManager.GetWorldLevel;
            level = worldLevel - ((worldLevel - 1) % 10);
        }
        UpdateLevelMultiplier();
    }
    private void UpdateLevelMultiplier()
    {
        hpMax = baseHP * Mathf.Pow(multiplier, level - 1);
    }
}
