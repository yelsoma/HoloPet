using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMounted_Balloon : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractableSM interactableSM;
    private InteractableManager myInteractableMg;
    private IMountableSM mountableSM;
    #region AutoSetRef
    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }

        interactableSM = GetComponentInParent<IInteractableSM>();
        if (interactableSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no IInteractableSM found in parent.");
        }

        mountableSM = GetComponentInParent < IMountableSM>();
        if(mountableSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no mountableSM  found in parent.");
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
        }
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
