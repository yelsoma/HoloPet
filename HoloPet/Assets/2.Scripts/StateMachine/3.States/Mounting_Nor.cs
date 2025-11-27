using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mounting_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private MountingAbilityMod mountingAbilityMod;

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
    }
    public override void StateUpdate()
    {    
        if(mountingAbilityMod.MountingAbilityMg.GetMount().GetIsMountableState() == false)
        {
            stateMachine.ChangeState(basicMod.StateClicked);
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
