using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMounted_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private InteractableMod interactableMod;
    private InteractableManager myInteractableMg;
    private MountableMod mountableMod;
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

        IInteractableMod iInteractableMod = GetComponentInParent<IInteractableMod>();
        if (iInteractableMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no IInteractableMod found in parent.");
        }
        else
        {
            interactableMod = iInteractableMod.InteractableMod;
        }

        IMountableMod imountableMod = GetComponentInParent<IMountableMod>();
        if (imountableMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no mountableMod  found in parent.");
        }
        else
        {
            mountableMod = imountableMod.MountableMod;
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {      
    }
    public override void StateUpdate()
    {
        myInteractableMg = interactableMod.InteractableMg;
        Transform interacterTransform = myInteractableMg.GetInteracterManager().GetStateMachineTransform();
        StateMachineBase interacterSM = interacterTransform.GetComponent<StateMachineBase>();
        if (interacterTransform.TryGetComponent<IMountingAbilityMod>(out IMountingAbilityMod iMounterMountingAbilityMod) && iMounterMountingAbilityMod.MountingAbilityMod.MountingAbilityMg.TrySetMount(mountableMod.MountableMg))
        {
            //sucsses set mounter
            interacterSM.ChangeState(iMounterMountingAbilityMod.MountingAbilityMod.StateMounting);
        }
        if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicMod.StateIdle);
        }
        stateMachine.ChangeState(basicMod.StateInAir);
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
    }
    #endregion
}
