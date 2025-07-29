using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableManager : MonoBehaviour
{

    public class ChangeLayerEventArgs : EventArgs
    {
        public Transform Transform { get; }

        public ChangeLayerEventArgs(Transform transform)
        {
            Transform = transform;
        }
    }
    public event EventHandler<ChangeLayerEventArgs> OnEnterInteractedChangeLayer;
    public event EventHandler OnExitInteracted;
    [SerializeField] private StateMachineBase stateMachine;
    [SerializeField] private List<InteractedOption> interactedOptions;
    private InteractAbilityManager interacterMg;
    private bool isInteractable = true;
    [SerializeField] private StateBase[] unInteractableState;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform} ¡X no StateMachineBase found in parent.");
        }
        if(unInteractableState.Length > 0)
        {
            foreach (StateBase unInteractableState in unInteractableState)
            {
                if(unInteractableState == null)
                {
                    Debug.LogError(stateMachine.transform.name +"'s "+ "InteractableMg is not set correctly");
                }
                unInteractableState.OnEnterState += UnInteractableState_OnEnterState;
                unInteractableState.OnExitState += UnInteractableState_OnExitState;
            }
        }
        else
        {
            Debug.LogWarning(stateMachine.transform.name + "'s " + "InteractableMg unInteractableState is 0");
        }
        if (interactedOptions.Count == 0)
        {
            Debug.LogWarning(stateMachine.transform.name + "'s " + "InteractableMg interacted option is 0");
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

    public InteractAbilityManager GetInteracterManager()
    {
        return interacterMg;
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
        if (interacterMg.GetBothInteractOption().GetInteractedOption().GetOptionState != null)
        {
            ChangeLayerEventArgs args = new ChangeLayerEventArgs(interacterMg.GetStateMachineTransform()); 
            OnEnterInteractedChangeLayer?.Invoke(this, args);
            stateMachine.ChangeState(interacterMg.GetBothInteractOption().GetInteractedOption().GetOptionState);
        }
    }

    public void SetInteracter(InteractAbilityManager interactAbility)
    {
        interacterMg = interactAbility;
    }

    public void ExitInteractedEvent()
    {
        OnExitInteracted?.Invoke(this, EventArgs.Empty);
    }
    public bool GetIsInteracterRight()
    {
        if (interacterMg.GetStateMachineTransform().position.x - stateMachine.transform.position.x >= 0)
        {
            return true;
        }
        return false;
    }
}
