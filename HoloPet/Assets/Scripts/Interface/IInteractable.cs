using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IInteractable
{
    public event EventHandler OnInteractedByTarget;
    public event EventHandler OnTargetExitInteract;
    public void SetTarget(IInteractable targetIInteractable);
    public bool GetIsInteractable();
    public IInteractable GetTargetIInteractable(); 
    public Vector2 GetPosition();
    public void IsInteractedByTarget();
    public void TargetExitInteract();
}
