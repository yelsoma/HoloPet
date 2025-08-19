using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMounted_Cart : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractableSM interactableSM;
    private InteractableManager myInteractableMg;
    private IMountableSM mountableSM;
    private IDriveSM driveSM;
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

        interactableSM = GetComponentInParent<IInteractableSM>();
        if (interactableSM == null)
        {
            Debug.LogError($"{transform} ¡X no IInteractableSM found in parent.");
        }

        mountableSM = GetComponentInParent < IMountableSM>();
        if(mountableSM == null)
        {
            Debug.LogError($"{transform} ¡X no mountableSM  found in parent.");
        }

        driveSM = GetComponentInParent<IDriveSM>();
        if (driveSM == null)
        {
            Debug.LogError($"{transform} ¡X no driveSM   found in parent.");
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {            
    }
    public override void StateUpdate()
    {
        myInteractableMg = interactableSM.InteractableMg;
        Transform interacterTransform = myInteractableMg.GetInteracterManager().GetStateMachineTransform();
        StateMachineBase interacterSM = interacterTransform.GetComponent<StateMachineBase>();
        IBasicSM interacterBasicSM = interacterTransform.GetComponent<IBasicSM>();
        if (interacterTransform.TryGetComponent<IMountingAbilitySM>(out IMountingAbilitySM MounterMountingAbilitySM) && MounterMountingAbilitySM.MountingAbilityMg.TrySetMount(mountableSM.MountableMg))
        {
            //sucsses set mounter
            interacterSM.ChangeState(MounterMountingAbilitySM.StateMounting);

            //check is it batan
            if (interacterBasicSM.BaseDataMg.GetObjectName() == ObjectNameEnum.Botan)
            {
                stateMachine.ChangeState(driveSM.StateDrive);
                return;
            }
        }
        //fail
        stateMachine.ChangeState(basicSM.StateInAir);
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
    }
    #endregion
}
