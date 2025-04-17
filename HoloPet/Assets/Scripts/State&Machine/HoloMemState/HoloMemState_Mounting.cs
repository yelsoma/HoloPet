using System;
using UnityEngine;

public class HoloMemState_Mounting : StateBase
{
    [SerializeField] private HoloMemStateMachine stateMachine;
    public event EventHandler OnCartDashMaxSpeed;
    public event EventHandler OnCartNormal;
    public event EventHandler OnCartJump;
    public override void Enter()
    {
        //cant do
        stateMachine.interactManager.SetIsInteractable(false);
        
        //event
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;

        //start
        stateMachine.mountManager.EnterMount();
        transform.root.GetComponent<IMountable>().LayerChainUpStart();
        
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
        stateMachine.mountManager.FollowMountPoint();
    }
    public override void Exit()
    {
        stateMachine.mountManager.ExitMount();
        //cant do
        stateMachine.interactManager.SetIsInteractable(true);
        //event        
        stateMachine.mouseInput.OnDrag -= Input_OnDrag;
        stateMachine.mouseInput.OnClick -= MouseInput_OnClick;        
    }

    // < Events >
    private void Input_OnDrag(object sender, MouseInputVr2.OnDragEventArgs e)
    {
        //exit to grab
        stateMachine.ChangeState(stateMachine.stateGrab);
        return;
    }
    private void MouseInput_OnClick(object sender, EventArgs e)
    {
        //exit to knockUp
        stateMachine.ChangeState(stateMachine.stateKnockUp);
        return;
    }   
}
