using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mounting_Botan : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private MountingAbilityMod mountingAbilityMod;
    private MountableManager mountMg;

    #region AutoSetRef
    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        }

        IBasicMod ibasicMod = GetComponentInParent<IBasicMod>();
        if (ibasicMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
        else
        {
            basicMod = ibasicMod.BasicMod;
        }

        IMountingAbilityMod iMountingAbilityMod = GetComponentInParent<IMountingAbilityMod>();
        if (iMountingAbilityMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no imountingAbilityMod found in parent.");
        }
        else
        {
            mountingAbilityMod = iMountingAbilityMod.MountingAbilityMod;
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        mountingAbilityMod.MountingAbilityMg.EnterMount();
        mountMg = mountingAbilityMod.MountingAbilityMg.GetMount();
    }
    public override void StateUpdate()
    {
        if (mountMg.GetIsMountableState() == false)
        {
            stateMachine.ChangeState(basicMod.StateClicked);
        }
        if (mountMg.GetStateMachineTransform().TryGetComponent<CartSM>(out CartSM cartSM))
        {
            if (cartSM.GetStateNow() == cartSM.StateDrive)
            {
                TriggerAni1();
                return;
            }
            if (cartSM.GetStateNow() == cartSM.StateDirveMax)
            {
                TriggerAni2();
                return;
            }
        }
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
        mountingAbilityMod.MountingAbilityMg.ExitMount();
    }
    #endregion
}
