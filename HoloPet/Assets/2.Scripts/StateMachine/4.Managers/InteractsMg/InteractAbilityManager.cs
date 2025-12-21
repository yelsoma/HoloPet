using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class InteractAbilityManager : MonoBehaviour
{
    private StateMachineBase stateMachine;
    [SerializeField] private List<InteracterOption> interacterOptionList;
    private RaycastManager raycastManager;

    private InteractableManager target;
    private BothInteractOption chosenBothInteractOption;
    private bool isTargetLocked;

    public event EventHandler OnTriggerInteracting;
    public event EventHandler OnExitInteracting;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");

        IBasicMod iBasicMod = stateMachine.transform.GetComponent<IBasicMod>();
        if (iBasicMod == null)
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        else
            raycastManager = iBasicMod.BasicMod.RaycastMg;

        if (interacterOptionList.Count == 0)
            Debug.LogWarning($"{transform.root.name}'s InteractAbilityManager has no interacter options.");
    }

    // --------------------------
    // ?? Target Handling
    // --------------------------
    public bool CheckIsTargetHit(Vector2 direction, float distance)
    {
        RaycastHit2D[] hitsAll = raycastManager.GetAllHits(direction, distance);

        if (hitsAll.Length == 0)
            return false;

        foreach (RaycastHit2D hit in hitsAll)
        {
            if (hit.transform.TryGetComponent<IInteractableMod>(out var iInteractableMod))
            {
                if (iInteractableMod.InteractableMod.InteractableMg == target)
                    return true;
            }
        }

        return false;
    }
    public bool TrySetTargetBothSide(float distance)
    {
        List<RaycastHit2D>hitsAll = new List<RaycastHit2D>();
        hitsAll.AddRange(raycastManager.GetAllHits(Vector2.left, distance));
        hitsAll.AddRange(raycastManager.GetAllHits(Vector2.right, distance));
        // remove duplicates by collider
        hitsAll = hitsAll
            .GroupBy(h => h.collider)
            .Select(g => g.First())
            .ToList();
        foreach (var hit in hitsAll)
        {
            if (hit.transform.TryGetComponent(out IInteractableMod iInteractableMod))
            {
                InteractableManager candidate = iInteractableMod.InteractableMod.InteractableMg;
                target = candidate;
                if (TryMatchOptionsWithTarget())
                {
                    return true; // found a valid target & option
                }
            }
        }

        target = null;
        return false;
    }

    private bool TryMatchOptionsWithTarget()
    {
        float totalChance = 0f;
        List<BothInteractOption> bothOptions = new List<BothInteractOption>();

        foreach (var interacterOption in interacterOptionList)
        {
            if (interacterOption.GetChance <= 0) continue;

            foreach (var interactedOption in target.GetInteractedOptions())
            {
                if (interactedOption.GetChance <= 0) continue;

                if (interacterOption.GetInteracterOptionEnum == interactedOption.GetInteractedOptionEnum)
                {
                    var both = new BothInteractOption();
                    both.SetInteracterOption(interacterOption);
                    both.SetInteractedOption(interactedOption);

                    float addedChance = interacterOption.GetChance * interactedOption.GetChance;
                    both.SetAddedChance(addedChance);

                    bothOptions.Add(both);
                    totalChance += addedChance;
                }
            }
        }

        if (totalChance <= 0) return false;

        // Weighted random pick
        float randomChance = UnityEngine.Random.Range(0, totalChance);
        float cumulative = 0f;

        foreach (var both in bothOptions)
        {
            cumulative += both.GetAddedChance();
            if (cumulative >= randomChance)
            {
                chosenBothInteractOption = both;
                return true;
            }
        }

        return false;
    }

    public InteractableManager GetTargetInteractableMg() => target;
    public BothInteractOption GetBothInteractOption() => chosenBothInteractOption;

    // --------------------------
    // ?? Events
    // --------------------------
    public void TriggerInteractingEvent() => OnTriggerInteracting?.Invoke(this, EventArgs.Empty);
    public void ExitInteractingEvent() => OnExitInteracting?.Invoke(this, EventArgs.Empty);

    // --------------------------
    // ?? Utility
    // --------------------------
    public Transform GetStateMachineTransform() => stateMachine.transform;

    public bool GetIsTargetFarX(float distance) =>
        target != null && Mathf.Abs(target.GetStateMachineTransform().position.x - stateMachine.transform.position.x) >= distance;

    public bool GetIsTargetFarY(float distance) =>
        target != null && Mathf.Abs(target.GetStateMachineTransform().position.y - stateMachine.transform.position.y) >= distance;

    public bool GetIsTargetRight() =>
        target != null && (target.GetStateMachineTransform().position.x - stateMachine.transform.position.x) >= 0;

    public void SetTargetLocked(bool locked) => isTargetLocked = locked;
    public bool GetIsTargetLocked() => isTargetLocked;
}
