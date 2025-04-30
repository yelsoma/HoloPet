using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HoloMemEventManager : MonoBehaviour
{
    [SerializeField] HoloMemStateMachine HoloMemStateMachine;
    public event EventHandler<OnChangeStateArgs> OnChangeState;  
    public class OnChangeStateArgs : EventArgs
    {
        public StateBase stateBase;
    }
    public event EventHandler OnMouseRelease;
    //Mouse events
    public void KnockUp()
    {
        OnChangeState?.Invoke(this, new OnChangeStateArgs { stateBase = HoloMemStateMachine.stateKnockUp });
    }
    public void Drag()
    {
        OnChangeState?.Invoke(this, new OnChangeStateArgs { stateBase = HoloMemStateMachine.stateGrab });
    }
    public void ReleaseEvent()
    {
        OnMouseRelease?.Invoke(this, EventArgs.Empty);
    }
}
