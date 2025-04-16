using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : MonoBehaviour
{
    public event EventHandler OnEnterState;
    public virtual void Enter() { }
    public virtual void StateUpdate() { }
    public virtual void StateLateUpdate() { }
    public virtual void Exit() { }

    public void EnterStateEvent()
    {
        OnEnterState?.Invoke(this,EventArgs.Empty);
    }
}
