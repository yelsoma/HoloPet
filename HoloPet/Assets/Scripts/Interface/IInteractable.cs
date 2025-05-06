using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IInteractable 
{
    public event EventHandler OnExitInteracted;
    public void ExitInteractedEvent();
    public void SetInteracter(IInteractAbility interactAbility);
    public IInteractAbility GetInteracter();
    public bool GetIsInteractable();
    public void SetIsInteractable(bool isInteractable);
    public List<InteractedOption> GetInteractedOptions();
    public void GoToChoosenInteracedState();
    public Transform GetTransform();
}
