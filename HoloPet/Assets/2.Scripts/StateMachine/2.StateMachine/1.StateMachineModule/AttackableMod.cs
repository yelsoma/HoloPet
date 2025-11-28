using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableMod : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private AttackableManager attackableMg;
    public AttackableManager AttackableMg => attackableMg;
    [SerializeField] private DefenceStatManager defenceStatMg;
    public DefenceStatManager DefenceStatMg => defenceStatMg;

    [Header("States")]
    [SerializeField] private StateBase stateHpZero;
    [SerializeField] private StateBase stateKnockBack;
    public StateBase StateHpZero => stateHpZero;
    public StateBase StateKnockBack => stateKnockBack;

    private void Awake()
    {
        if (attackableMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add attackableMg in AttackableMod");
        }
        if (stateHpZero == null)
        {
            Debug.LogError(transform.root.name + "forget to add stateHpZero in AttackableMod");
        }
        if (stateKnockBack == null)
        {
            Debug.LogError(transform.root.name + "forget to add stateKnockBack in AttackableMod");
        }
        if (defenceStatMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add defenceStatMg in AttackableMod");
        }
    }
}
