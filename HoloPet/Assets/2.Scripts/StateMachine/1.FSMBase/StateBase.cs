using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : MonoBehaviour
{
    public event EventHandler OnEnterState;
    public event EventHandler OnExitState;
    public event EventHandler OnTriggerAni1;
    public event EventHandler OnTrigger1;
    protected void TriggerAni1()
    {
        OnTriggerAni1?.Invoke(this, EventArgs.Empty);
    }
    public event EventHandler OnTriggerAni2;
    protected void TriggerAni2()
    {
        OnTriggerAni2?.Invoke(this, EventArgs.Empty);
    }
    public event EventHandler OnTriggerAni3;
    protected void TriggerAni3()
    {
        OnTriggerAni3?.Invoke(this, EventArgs.Empty);
    }

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
