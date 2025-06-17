using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicState_Mounting : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IMountingAbilitySM mountingAbilitySM;

    #region AutoSetRef
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

        mountingAbilitySM = GetComponentInParent<IMountingAbilitySM>();
        if(mountingAbilitySM == null)
        {
            Debug.LogError($"{transform} ¡X no IMountingAbilitySM found in parent.");
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        mountingAbilitySM.MountingAbilityMg.EnterMount();
    }
    public override void StateUpdate()
    {    
        if(mountingAbilitySM.MountingAbilityMg.GetMount().GetIsMountableState() == false)
        {
            stateMachine.ChangeState(basicSM.StateClicked);
        }
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
        mountingAbilitySM.MountingAbilityMg.ExitMount();
    }
    #endregion
}
