using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IInteractAbility 
{
    public event EventHandler OnExitInteracting;   
    public void ExitInteractingEvent();
    public BothInteractOption GetBothInteractOption();
    public IInteractable GetTargetIInteractable();
    public Transform GetTransform();
}
