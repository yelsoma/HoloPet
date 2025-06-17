using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableManager : MonoBehaviour
{
    public event EventHandler OnExitInteracted;
    [SerializeField] private StateMachineBase stateMachine;
    private IInteractableSM interactableSM;
    [SerializeField] private List<InteractedOption> interactedOptions;
    private InteractAbilityManager interacter;
    private bool isInteractable = true;
    [SerializeField] private StateBase[] unInteractableState;

    private void Awake()
    {
        interactableSM = transform.root.GetComponent<IInteractableSM>();
        if(interactableSM == null)
        {
            Debug.Log(transform + "no IInteractableSM for InteractableManager");
        }

        foreach (StateBase unInteractableState in unInteractableState)
        {
            unInteractableState.OnEnterState += UnInteractableState_OnEnterState;
            unInteractableState.OnExitState += UnInteractableState_OnExitState;
        }
    }

    private void UnInteractableState_OnEnterState(object sender, EventArgs e)
    {
        isInteractable = false;
    }

    private void UnInteractableState_OnExitState(object sender, EventArgs e)
    {
        isInteractable = true;
    }
 
    public List<InteractedOption> GetInteractedOptions()
    {
        return interactedOptions;
    }

    public InteractAbilityManager GetInteracter()
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
    public Transform GetStateMachineTransform()
    {
        return stateMachine.transform;
    }

    public void GoToChoosenInteracedState()
    {
        if (interacter.GetBothInteractOption().GetInteractedOption().GetOptionState != null)
        {
            stateMachine.ChangeState(interacter.GetBothInteractOption().GetInteractedOption().GetOptionState);
        }
    }

    public void SetInteracter(InteractAbilityManager interactAbility)
    {
        interacter = interactAbility;
    }

    public void ExitInteractedEvent()
    {
        OnExitInteracted?.Invoke(this, EventArgs.Empty);
    }
    public bool GetIsInteracterRight()
    {
        if (interacter.GetStateMachineTransform().position.x - stateMachine.transform.position.x >= 0)
        {
            return true;
        }
        return false;
    }
}
