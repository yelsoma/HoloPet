using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HoloMemInteractManager : MonoBehaviour ,IInteractable
{
    // self
    private bool isInteractable;

    // target 
    private IInteractable targetIInteractable;

    //interact option
    [SerializeField] private List<interactOption> interactOptionList;
    private interactOption choosenInteractOp;
    private interactOption interactedOp;

    //event
    public event EventHandler OnInteractedByTarget;
    public event EventHandler OnTargetExitInteract;

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
            RaycastHit2D closestHits = interactablelists[0];
            for (int i = 0; i < interactableArrayLength; i++)
            {
                if (interactablelists[i].distance < closestHits.distance)
                {
                    closestHits = interactablelists[i];
                } 
            }
            targetIInteractable = closestHits.transform.GetComponent<IInteractable>();
        }
        else
        {
            //there is nothing interactable
            return false;
        }
        //there is something interactable
        return true;
    }
    public IInteractable GetTargetIInteractable()
    {
        return targetIInteractable;
    }
    public void SetTarget(IInteractable targetIInteractable)
    {
        this.targetIInteractable = targetIInteractable;
    }
    public bool GetIsInteractable()
    {
        return isInteractable;
    }
    public void SetIsInteractable(bool isInteractable)
    {
        this.isInteractable = isInteractable;
    }
    public bool GetIsTargetFar(float interactDistance)
    {
        if(Vector2.Distance(transform.position, targetIInteractable.GetPosition()) > interactDistance)
        {
            return true;
        }
        return false;
    }
    public bool GetIsTargetRight()
    {
        if(transform.position.x < targetIInteractable.GetPosition().x)
        {
            return true;
        }
        return false;
    }
    public Vector2 GetPosition()
    {
        return transform.position;
    }
    public void TargetExitInteract()
    {
        OnTargetExitInteract?.Invoke(this, EventArgs.Empty);
    }
    public void IsInteractedByTarget()
    {
        OnInteractedByTarget?.Invoke(this, EventArgs.Empty);      
    }  
    public interactOption[] GetInteractOptions()
    {
        return interactOptionList.ToArray();
    }
    public void SetChoosonInteractOp(interactOption interactOption)
    {
        choosenInteractOp = interactOption;
    }
    public interactOption GetChoosenInteractOp()
    {
        return choosenInteractOp;
    }

    public void SetInteractedOp(interactOption interactedOp)
    {
        this.interactedOp = interactedOp;
    }

    public interactOption GetInteractedOp()
    {
        return interactedOp;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
