using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : MonoBehaviour
{
    public event EventHandler OnEnterState;
    public event EventHandler OnExitState;
    public event EventHandler OnTrigger1;
    public event EventHandler OnTrigger2;
    public virtual void Enter() { }
    public virtual void StateUpdate() { }
    public virtual void StateLateUpdate() { }
    public virtual void Exit() { }

    public void EnterStateEvent()
    {
        OnEnterState?.Invoke(this,EventArgs.Empty);
    }
    public void ExitStateEvent()
    {
        OnExitState?.Invoke(this,EventArgs.Empty);
    }
}
