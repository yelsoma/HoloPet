using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbilityMod : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private AttackAbilityManager attackAbilityMg;
    public AttackAbilityManager AttackAbilityMg => attackAbilityMg;
    [SerializeField] private OffenceStatManager offenceStatMg;
    public OffenceStatManager OffenceStatMg => offenceStatMg;

    [Header("States")]
    [SerializeField] private StateBase stateBasicAttack;

    public StateBase StateBasicAttack => stateBasicAttack;

    private void Awake()
    {
        if (attackAbilityMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add attackAbilityMg in AttackAbilityMod");
        }
        if (stateBasicAttack == null)
        {
            Debug.LogError(transform.root.name + "forget to add stateBasicAttack in AttackAbilityMod");
        }
        if(offenceStatMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add offenceStatMg in AttackAbilityMod");
        }
    }
}
