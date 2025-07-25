using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartInteractManager : MonoBehaviour, IInteractable
{   
    public event EventHandler OnExitInteracted;

    [SerializeField] private CartStateMachine stateMachine;
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
        if(interacter.GetBothInteractOption().GetInteractedOption().GetOptionState != null)
        {
            stateMachine.ChangeState(interacter.GetBothInteractOption().GetInteractedOption().GetOptionState);
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
