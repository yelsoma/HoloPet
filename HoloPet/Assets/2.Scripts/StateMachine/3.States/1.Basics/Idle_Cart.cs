using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Cart : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IMountableSM mountableSM;
    private CartSM cartSM;

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
        if(mountableSM == null)
        {
            Debug.LogError($"{transform} ¡X no mountableSM found in parent.");
        }

        cartSM = GetComponentInParent<CartSM>();
        if (cartSM == null)
        {
            Debug.LogError($"{transform} ¡X no cartSM found in parent.");
        }
    }

    public override void Enter()
    {
    }

    public override void StateUpdate()
    {
        if (mountableSM.MountableMg.GetIsMounted())
        {
            stateMachine.ChangeState(cartSM.StateDrive);           
        }
        else
        {
            //keep idle
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
