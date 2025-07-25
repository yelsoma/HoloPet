using System;
using UnityEngine;

public class HoloMemState_Mounting : StateBase
{
    [SerializeField] private HoloMemStateMachine stateMachine;
    public event EventHandler OnCartDashMaxSpeed;
    public event EventHandler OnCartNormal;
    public event EventHandler OnCartJump;
    public event EventHandler OnExitMounting;
    public override void Enter()
    {
        //cant do
        stateMachine.interactManager.SetIsInteractable(false);

        //event

        //start
        stateMachine.mountManager.EnterMount();
    }
    public override void StateUpdate()
    {
        if (!stateMachine.mountManager.GetMount().GetIsMountableState())
        {
            // exit to knockUp
            stateMachine.ChangeState(stateMachine.stateKnockUp);
            return;
        }
        //check is on cart and max speed state
        if(stateMachine.mountManager.GetMount().GetTransform().TryGetComponent(out CartStateMachine cartStateMachine))
        {
            if(cartStateMachine.GetStateNow() == cartStateMachine.stateDashMaxSpeed)
            {
                OnCartDashMaxSpeed?.Invoke(this, EventArgs.Empty);
            }
            else if (cartStateMachine.GetStateNow() == cartStateMachine.stateJump)
            {
                OnCartJump?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnCartNormal?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
        OnExitMounting?.Invoke(this, EventArgs.Empty);
        stateMachine.mountManager.ExitMount();
        //cant do
        stateMachine.interactManager.SetIsInteractable(true);
        //event           
    }

    // < Events >
}
