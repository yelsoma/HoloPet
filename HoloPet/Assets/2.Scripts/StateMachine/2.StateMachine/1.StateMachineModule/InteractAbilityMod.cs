using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAbilityMod : MonoBehaviour
{
    [Header("States")]
    [SerializeField] private StateBase stateInteractThink;
    [SerializeField] private StateBase stateInteractFollowX;
    [SerializeField] private StateBase stateInteractFollowY;
    [SerializeField] private StateBase stateInteractFailed;
    public StateBase StateInteractThink => stateInteractThink;
    public StateBase StateInteractFollowX => stateInteractFollowX;
    public StateBase StateInteractFollowY => stateInteractFollowY;
    public StateBase StateInteractFailed => stateInteractFailed;

    [Header("Managers")]
    [SerializeField] private InteractAbilityManager interactAbilityMg;
    [SerializeField] private TextLogManager textLogMg;

    public InteractAbilityManager InteractAbilityMg => interactAbilityMg;
    public TextLogManager TextLogMg => textLogMg;

    private void Awake()
    {
        if (stateInteractThink == null)
        {
            Debug.LogError(transform.root.name + "forget to add stateInteractThink inInteractAbilityMod");
        }
        if (stateInteractFollowX == null)
        {
            Debug.LogError(transform.root.name + "forget to add stateInteractFollowX inInteractAbilityMod");
        }
        if (stateInteractFollowY == null)
        {
            Debug.LogError(transform.root.name + "forget to add stateInteractFollowY in InteractAbilityMod");
        }
        if (stateInteractFailed == null)
        {
            Debug.LogError(transform.root.name + "forget to add stateInteractFailed in InteractAbilityMod");
        }
        if (interactAbilityMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add interactAbilityMg in InteractAbilityMod");
        }
        if (textLogMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add textLogMg in InteractAbilityMod");
        }
    }
}
