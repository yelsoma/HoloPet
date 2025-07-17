using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicState_Grabed : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;

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
    }

    public override void Enter()
    {
        basicSM.ClickableMg.OnRelease += ClickableManager_OnRelease;
        basicSM.ClickableMg.OnGrab += ClickableManager_OnGrab;
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
        basicSM.ClickableMg.OnGrab -= ClickableManager_OnGrab;
    }

    private void ClickableManager_OnRelease(object sender, System.EventArgs e)
    {
        stateMachine.ChangeState(basicSM.StateReleased);
    }

    private void ClickableManager_OnGrab(object sender, ClickableManager.GrabEventArgs e)
    {
        stateMachine.transform.position = e.MousePosition;
    }
}

