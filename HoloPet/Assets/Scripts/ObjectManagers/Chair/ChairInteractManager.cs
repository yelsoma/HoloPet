using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairInteractManager : MonoBehaviour, IInteractable
{
    public event EventHandler OnExitInteracted;

    [SerializeField] private ChairStateMachine stateMachine;
    [SerializeField] private List<InteractedOption> interactedOptions;
    private IInteractAbility interacter;
    private bool isInteractable;


    public List<InteractedOption> GetInteractedOptions()
    {
        return interactedOptions;
    }

    public IInteractAbility GetInteracter()
    {
        return interacter;
    }

    public bool GetIsInteractable()
    {
        return isInteractable;
    }

    public void SetIsInteractable(bool isInteractable)
    {
        this.isInteractable = isInteractable;
    }
    public Transform GetTransform()
    {
        return stateMachine.transform;
    }

    public void GoToChoosenInteracedState()
    {
        if (interacter.GetBothInteractOption().GetInteractedOption().optionState != null)
        {
            stateMachine.ChangeState(interacter.GetBothInteractOption().GetInteractedOption().optionState);
        }
    }

    public void SetInteracter(IInteractAbility interactAbility)
    {
        interacter = interactAbility;
    }

    public void ExitInteractedEvent()
    {
        OnExitInteracted?.Invoke(this, EventArgs.Empty);
    }
}
