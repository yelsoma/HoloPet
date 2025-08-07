using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grabbed_Cart : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IMountableSM mountableSM;

    public event EventHandler OnMountedGrabbed;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform} ¡X no StateMachineBase found in parent.");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform} ¡X no basicSM found in parent.");
        }

        mountableSM = GetComponentInParent<IMountableSM>();
        if (mountableSM == null)
        {
            Debug.LogError($"{transform} ¡X no mountableSM found in parent.");
        }
    }

    public override void Enter()
    {
        basicSM.ClickableMg.OnRelease += ClickableManager_OnRelease;
        basicSM.ClickableMg.OnGrabMousePos += ClickableManager_OnGrabMousePos;
        if (mountableSM.MountableMg.GetIsMounted())
        {
            OnMountedGrabbed?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void StateUpdate()
    {
    }

    public override void StateLateUpdate()
    {
        basicSM.BoundaryMg.CheckAllBouderyAndResetPos();
    }

    public override void Exit()
    {
        basicSM.ClickableMg.OnRelease -= ClickableManager_OnRelease;
        basicSM.ClickableMg.OnGrabMousePos -= ClickableManager_OnGrabMousePos;
    }

    private void ClickableManager_OnRelease(object sender, System.EventArgs e)
    {
        stateMachine.ChangeState(basicSM.StateReleased);
    }

    private void ClickableManager_OnGrabMousePos(object sender, ClickableManager.GrabEventArgs e)
    {
        stateMachine.transform.position = e.MousePosition;
    }
}
