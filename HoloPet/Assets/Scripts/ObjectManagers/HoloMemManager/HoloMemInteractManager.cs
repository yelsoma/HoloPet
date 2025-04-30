using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HoloMemInteractManager : MonoBehaviour ,IInteractable ,IInteractAbility
{
    [SerializeField] HoloMemStateMachine stateMachine;
    // self
    private bool isInteractable;

    // target , interacter 
    private IInteractable target;
    private IInteractAbility interacter;

    //interact option
    [SerializeField] private List<InteractOption> interactOptionList;
    private InteractOption choosenInteractOp;

    //event
    public event EventHandler OnInteractedByInteracter;
    public event EventHandler OnInteracterExitInteract;
    public event EventHandler OnTargetExitInteract;


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
    public void ChooseAnOption()
    {
        float chance = UnityEngine.Random.Range(0f, 1f);
        float chanceAdd = 0;
        foreach (InteractOption interactOp in interactOptionList)
        {
            chanceAdd += interactOp.chance;
            if(chanceAdd >= chance)
            {
                choosenInteractOp = interactOp;
            }
        }
    }
    public InteractOption GetChoosenOp()
    {
        return choosenInteractOp;
    }
    public IInteractable GetTargetIInteractable()
    {
        return target;
    }
    public void InteractTargetExitInteract()
    {
        OnTargetExitInteract?.Invoke(this, EventArgs.Empty);
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
    public void OnInteractWithOption(InteractOption interactOption)
    {
        if (stateMachine.interactManager.GetInteracter() == null)
        {
            // exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
        InteractOption interactedOp = stateMachine.interactManager.GetInteracter().GetChoosenOp();
        if (interactedOp.interactOptionEnum == interactOptionE.Bully)
        {
            stateMachine.ChangeState(stateMachine.stateBullied);
            return;
        }
        if (interactedOp.interactOptionEnum == interactOptionE.happyChat)
        {
            stateMachine.ChangeState(stateMachine.stateHappyChatInteracted);
            return;
        }
        if (interactedOp.interactOptionEnum == interactOptionE.sit)
        {
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
        OnInteractedByInteracter?.Invoke(this, EventArgs.Empty);
    }
    public void InteracterExitInteract()
    {
        OnInteracterExitInteract?.Invoke(this, EventArgs.Empty);
    }
    public Vector2 GetPosition()
    {
        return transform.position;
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
