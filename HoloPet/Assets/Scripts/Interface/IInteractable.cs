using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IInteractable 
{
    public event EventHandler OnInteractedByInteracter;
    public event EventHandler OnInteracterExitInteract;
    public void SetInteracter(IInteractAbility interactAbility);
    public IInteractAbility GetInteracter();
    public bool GetIsInteractable();
    public void OnInteractWithOption(InteractOption interactOption);
    public void InteracterExitInteract();   
    public Transform GetTransform();
}
public enum interactOptionE
{
    Bully,
    happyChat,
    sit
}
[System.Serializable]
public struct InteractOption
{
    public interactOptionE interactOptionEnum;
    [Range(0,1)] public float chance;
    public StateBase stateBase;
}
