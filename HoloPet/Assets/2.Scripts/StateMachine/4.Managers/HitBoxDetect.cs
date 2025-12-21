using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitBoxDetect : MonoBehaviour
{
    public event EventHandler<HitEventArgs> OnTriggerHitBox;
    public class HitEventArgs : EventArgs
    {
        public StateMachineBase stateMachine;
    }
    private LayerMask stateMachineLayerMask;
    private void Awake()
    {
        int layer = LayerMask.NameToLayer("StateMachine");
        stateMachineLayerMask = 1 << layer;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check layer
        if ((stateMachineLayerMask.value & (1 << collision.gameObject.layer)) == 0)
            return;
        // Get the state machine from the collided object
        StateMachineBase sm = collision.GetComponentInParent<StateMachineBase>();
        if (sm == null)
            return;
        // Raise event
        OnTriggerHitBox?.Invoke(
            this,
            new HitEventArgs { stateMachine = sm }
        );
    }
}
