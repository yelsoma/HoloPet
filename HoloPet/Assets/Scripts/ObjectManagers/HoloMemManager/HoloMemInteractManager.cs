using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class HoloMemInteractManager : MonoBehaviour ,IInteractable ,IInteractAbility
{
    [SerializeField] HoloMemStateMachine stateMachine;
    // self
    private bool isInteractable;

    // target , interacter 
    private IInteractable target;
    private IInteractAbility interacter;

    //interact option
    [SerializeField] private List<InteracterOption> interacterOptionList;
    [SerializeField] private List<InteractedOption> interactedOptionList;
    private BothInteractOption choosenBothInteractOption;

    //event
    public event EventHandler OnExitInteracted;
    public event EventHandler OnExitInteracting;


    //interact Ability
    public bool TrySetTargetWihtRaycastHits(RaycastHit2D[] raycastHit2Ds)
    {
        List<RaycastHit2D> interactablelists = new List<RaycastHit2D>();
        // make  is interactable list 
        for (int i = 0; i < raycastHit2Ds.Length; i++)
        {           
            if (raycastHit2Ds[i].transform.TryGetComponent(out IInteractable interactable))
            {
                if (interactable.GetIsInteractable())
                {
                    interactablelists.Add(raycastHit2Ds[i]);
                }
            }
        }
        // find closest distance one
        int interactableArrayLength;
        interactableArrayLength = interactablelists.ToArray().Length;
        if (interactableArrayLength > 0)
        {
            int randomTargetI = UnityEngine.Random.Range(0, interactableArrayLength);
            target = interactablelists[randomTargetI].transform.GetComponent<IInteractable>();
        }
        else
        {
            //there is nothing interactable
            return false;
        }
        //there is something interactable
        return true;
    }
    public bool TryMatchOptionsChooseWithBothChance()
    {
        //creat a both side option list
        List<BothInteractOption> bothInteractOptionsList = new List<BothInteractOption>();
        //get target InteractedOption list
        List<InteractedOption> targetInteractedOpionList = target.GetInteractedOptions();
        //cycle through both side options see if option enum match ,add them to  both side op list
        foreach (InteracterOption interacterOption in interacterOptionList)
        {
            foreach(InteractedOption interactedOption in targetInteractedOpionList)
            {
                if(interacterOption.interacterOptionEnum == interactedOption.interactedOptionEnum)
                {
                    BothInteractOption bothInteracterOption = new BothInteractOption();
                    bothInteracterOption.SetInteracterOption(interacterOption);
                    bothInteracterOption.SetInteractedOption(interactedOption);
                    bothInteractOptionsList.Add(bothInteracterOption);
                }
            }           
        }
        //random one fron list
        if(bothInteractOptionsList.Count > 0)
        {
            int i = UnityEngine.Random.Range(0, bothInteractOptionsList.Count);
            choosenBothInteractOption = bothInteractOptionsList[i];
            return true;
        }
        //there is nothing in list
        return false;
    }
    public BothInteractOption GetBothInteractOption()
    {
        return choosenBothInteractOption;
    }
    public IInteractable GetTargetIInteractable()
    {
        return target;
    }
    public void ExitInteractingEvent()
    {
        OnExitInteracting?.Invoke(this, EventArgs.Empty);
    }


    //interactable
    public bool GetIsInteractable()
    {
        return isInteractable;
    }
    public void SetIsInteractable(bool isInteractable)
    {
        this.isInteractable = isInteractable;
    } 
    public void SetInteracter(IInteractAbility interactAbility)
    {
        this.interacter = interactAbility;
    }
    public IInteractAbility GetInteracter()
    {
        return interacter;
    }
    public List<InteractedOption> GetInteractedOptions()
    {
        return interactedOptionList;
    }
    public void GoToChoosenInteracedState()
    {    
        // if choosen option have state goto that state else do nothing
        if (interacter.GetBothInteractOption().GetInteractedOption().optionState != null)
        {
            stateMachine.ChangeState(interacter.GetBothInteractOption().GetInteractedOption().optionState);
        }
    }
    public void ExitInteractedEvent()
    {
        OnExitInteracted?.Invoke(this, EventArgs.Empty);
    }
    public Transform GetTransform()
    {
        return transform;
    }
    public bool GetIsTargetFar(float interactDistance)
    {
        if (Vector2.Distance(transform.position, target.GetTransform().position) > interactDistance)
        {
            return true;
        }
        return false;
    }
    public bool GetIsTargetRight()
    {
        if(target.GetTransform().position.x - stateMachine.transform.position.x >= 0)
        {
            return true;
        }
        return false;
    }
    public bool GetIsInteracterRight()
    {
        if (interacter.GetTransform().position.x - stateMachine.transform.position.x >= 0)
        {
            return true;
        }
        return false;
    }
}
