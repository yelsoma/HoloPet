using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IInteractAbility 
{
    public InteractOption GetChoosenOp();
    public IInteractable GetTargetIInteractable();
    public Transform GetTransform();
    public void InteractTargetExitInteract();
}
