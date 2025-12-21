using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffenceStatManager : MonoBehaviour
{
    private StateMachineBase stateMachine;
    private AFKManager afKManager;
    private ObjectDefinition def;
    private float multiplier;

   [Header("Set this in Inspector")]
    [SerializeField] private int level = 1;
    [SerializeField] private float baseATK = 5f;
    [SerializeField] private float atkSpeed;
    [SerializeField] private float damageMultiplier =1;
    [SerializeField] private float basicAtkDistance;
    [SerializeField] private float levelMultiplier;  

    [Header("Auto Calculated")]
    [SerializeField] private float atk;

    public float GetATK() => atk;
    public float GetAtkSpeed() => atkSpeed;
    public float GetBaseAtkDistance() => basicAtkDistance;
    public float GetDamageMultiplier() => damageMultiplier;
    public float GetLevelMultiplier() => levelMultiplier;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
            Debug.LogError(transform.name + " no stateMachine in parent");

        IBasicMod iBasicMod = stateMachine.transform.GetComponent<IBasicMod>();
        if (iBasicMod == null)
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        else
            def = iBasicMod.BasicMod.ObjectDefinition;
    }

    private void AfKManager_OnLevelUp(object sender, System.EventArgs e)
    {
        SetLevel();
    }

    private void Start()
    {
        multiplier = 1.08f;
        afKManager = GameController.Instance.AFKManager;
        afKManager.OnLevelUp += AfKManager_OnLevelUp;
        SetLevel();
    }

    private void OnValidate()
    {
        atk = baseATK * Mathf.Pow(multiplier, level - 1);
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
        levelMultiplier = Mathf.Pow(multiplier, level - 1);
        atk = baseATK * levelMultiplier;
    }
}
