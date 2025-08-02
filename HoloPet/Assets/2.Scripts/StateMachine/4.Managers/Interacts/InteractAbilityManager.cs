using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractAbilityManager : MonoBehaviour
{
    [SerializeField] StateMachineBase stateMachine;
    private InteractableManager target;
    [SerializeField] private List<InteracterOption> interacterOptionList;
    private BothInteractOption choosenBothInteractOption;
    public event EventHandler OnTriggerInteracting;
    public event EventHandler OnExitInteracting;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform} ¡X no StateMachineBase found in parent.");
        }
        if (interacterOptionList.Count == 0)
        {
            Debug.LogWarning(stateMachine.transform.name + "'s " + "InteractAbilityMg interacter option is 0");
        }
    }

    public bool TrySetTargetWihtRaycastHits(RaycastHit2D[] raycastHit2Ds)
    {
        List<RaycastHit2D> interactablelists = new List<RaycastHit2D>();
        // make  is interactable list 
        for (int i = 0; i < raycastHit2Ds.Length; i++)
        {
            if (raycastHit2Ds[i].transform.TryGetComponent(out IInteractableSM interactableSM))
            {
                if (interactableSM.InteractableMg.GetIsInteractable())
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
            target = interactablelists[randomTargetI].transform.GetComponent<IInteractableSM>().InteractableMg;
            //there is something interactable
            return true;
        }
        //there is nothing interactable
        return false;
    }
    public bool TryMatchOptionsChooseWithBothChance()
    {
        float totalChance = 0;
        //creat a both side option list
        List<BothInteractOption> bothInteractOptionsList = new List<BothInteractOption>();
        //get target InteractedOption list
        List<InteractedOption> targetInteractedOpionList = target.GetInteractedOptions();
        //cycle through both side options see if option enum match ,add them to  both side op list
        foreach (InteracterOption interacterOption in interacterOptionList)
        {
            foreach (InteractedOption interactedOption in targetInteractedOpionList)
            {
                if (interacterOption.GetInteracterOptionEnum == interactedOption.GetInteractedOptionEnum)
                {
                    BothInteractOption bothInteractOption = new BothInteractOption();
                    bothInteractOption.SetInteracterOption(interacterOption);
                    bothInteractOption.SetInteractedOption(interactedOption);
                    float addedChance = interacterOption.GetChance * interactedOption.GetChance;
                    bothInteractOption.SetAddedChance(addedChance);
                    bothInteractOptionsList.Add(bothInteractOption);
                    totalChance += addedChance;
                }
            }
        }
        //there is nothing in list
        if (totalChance == 0)
        {
            return false;
        }
        //random one fron list , add the chace of each option until it match random chance
        float randomChance = UnityEngine.Random.Range(0, totalChance);
        float chanceAddedNow = 0f;
        foreach (BothInteractOption bothInteractOption in bothInteractOptionsList)
        {
            chanceAddedNow += bothInteractOption.GetAddedChance();
            if (chanceAddedNow >= randomChance)
            {
                choosenBothInteractOption = bothInteractOption;
                return true;
            }
        }
        //there is nothing in list
        return false;
    }
    public BothInteractOption GetBothInteractOption()
    {
        return choosenBothInteractOption;
    }
    public InteractableManager GetTargetIInteractable()
    {
        return target;
    }
    public void TriggerInteractingEvent()
    {
        OnTriggerInteracting?.Invoke(this, EventArgs.Empty);
    }
    public void ExitInteractingEvent()
    {
        OnExitInteracting?.Invoke(this, EventArgs.Empty);
    }
    public Transform GetStateMachineTransform()
    {
        return stateMachine.transform;
    }
    public bool GetIsTargetFarX(float interactDistance)
    {
        if (Mathf.Abs(target.GetStateMachineTransform().position.x - stateMachine.transform.position.x)>= interactDistance)
        {
            return true;
        }
        return false;
    }
    public bool GetIsTargetFarY(float interactDistance)
    {
        if (Mathf.Abs(target.GetStateMachineTransform().position.y - stateMachine.transform.position.y) >= interactDistance)
        {
            return true;
        }
        return false;
    }
    public bool GetIsTargetRight()
    {
        if (target.GetStateMachineTransform().position.x - stateMachine.transform.position.x >= 0)
        {
            return true;
        }
        return false;
    }
}
