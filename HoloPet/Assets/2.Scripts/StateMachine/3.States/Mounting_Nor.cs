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
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");

        IBasicMod iBasicMod = stateMachine.transform.GetComponent<IBasicMod>();
        if (iBasicMod == null)
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        else
            basicMod = iBasicMod.BasicMod;

        IMountingAbilityMod iMountingAbilityMod = stateMachine.transform.GetComponent<IMountingAbilityMod>();
        if (iMountingAbilityMod == null)
            Debug.LogError($"{transform.root.name} ¡X no imountingAbilityMod found in parent.");
        else
            mountingAbilityMod = iMountingAbilityMod.MountingAbilityMod;
    }

    #endregion

    #region StateBase
    public override void Enter()
    {
        mountingAbilityMod.MountingAbilityMg.EnterMount();
        mountingAbilityMod.MountingAbilityMg.GetMount().OnEnterUnMountableState += Mounting_Nor_OnEnterUnMountableState;
    }

    private void Mounting_Nor_OnEnterUnMountableState(object sender, System.EventArgs e)
    {
        stateMachine.ChangeState(basicMod.StateClicked);
        return;
    }

    public override void StateUpdate()
    {
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
        mountingAbilityMod.MountingAbilityMg.ExitMount();
        mountingAbilityMod.MountingAbilityMg.GetMount().OnEnterUnMountableState -= Mounting_Nor_OnEnterUnMountableState;
    }
    #endregion
}
