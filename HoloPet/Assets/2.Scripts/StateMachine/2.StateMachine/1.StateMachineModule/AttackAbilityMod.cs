using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbilityMod : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private AttackAbilityManager attackAbilityMg;
    public AttackAbilityManager AttackAbilityMg => attackAbilityMg;

    [Header("States")]
    [SerializeField] private StateBase stateSearch;
    [SerializeField] private StateBase stateBasicAttack;

    public StateBase StateSearch => stateSearch;
    public StateBase StateBasicAttack => stateBasicAttack;

    private void Awake()
    {
        if (attackAbilityMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add attackAbilityMg in AttackAbilityMod");
        }
        if (stateSearch == null)
        {
            Debug.LogError(transform.root.name + "forget to add stateSearch in AttackAbilityMod");
        }
        if (stateBasicAttack == null)
        {
            Debug.LogError(transform.root.name + "forget to add stateBasicAttack in AttackAbilityMod");
        }
    }
}
