using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMounted_Seat : StateBase
{
    private SeatSM stateMachine;
    private IInteractableSM interactableSM;
    private InteractableManager myInteractableMg;
    private IMountableSM mountableSM;

    #region AutoSetRef
    private void Awake()
    {
        stateMachine = GetComponentInParent<SeatSM>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform} ¡X no StateMachineBase found in parent.");
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
        if (interacterTransform.TryGetComponent<IMountingAbilitySM>(out IMountingAbilitySM MounterMountingAbilitySM) && MounterMountingAbilitySM.MountingAbilityMg.TrySetMount(mountableSM.MountableMg))
        {
            //sucsses set mounter
            interacterSM.ChangeState(MounterMountingAbilitySM.StateMounting);
        }
        stateMachine.ChangeState(stateMachine.StateSpawn);
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
    }
    #endregion
}
