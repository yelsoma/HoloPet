using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountingAbilityMod : MonoBehaviour
{
    [Header("States")]
    [SerializeField] private StateBase stateMounting;
    public StateBase StateMounting => stateMounting;

    [Header("Managers")]
    [SerializeField] private MountingAbilityManager mountingAbilityMg;
    public MountingAbilityManager MountingAbilityMg => mountingAbilityMg;

    private void Awake()
    {
        if(mountingAbilityMg == null)
        {
            Debug.LogError(transform.root.name  + "forget to add mountingAbilityMg in MountingAbilityMod");
        }
        if(stateMounting == null)
        {
            Debug.LogError(transform.root.name + "forget to add stateMountingin MountingAbilityMod ");
        }
    }
}
