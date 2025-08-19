using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Cart : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IMountableSM mountableSM;
    private IDriveSM driveSM;
    private IInteractableSM interactableSM;

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

        driveSM = GetComponentInParent<IDriveSM>();
        if (driveSM == null)
        {
            Debug.LogError($"{transform} ¡X no IDriveSM found in parent.");
        }

        interactableSM = GetComponentInParent<IInteractableSM>();
        if (interactableSM == null)
        {
            Debug.LogError($"{transform} ¡X no IInteractableSM found in parent.");
        }
    }

    public override void Enter()
    {
        mountableSM.MountableMg.OnChangeMounted += MountableMg_OnChangeMounted;
        if (mountableSM.MountableMg.GetIsMounted())
        {
            interactableSM.InteractableMg.SetIsInteractable(false);
            if (mountableSM.MountableMg.GetMounterMountAbilityMg().GetStateMachineTransform().TryGetComponent<IBasicSM>(out IBasicSM basicSM))
            {
                if (basicSM.BaseDataMg.GetObjectName() == ObjectNameEnum.Botan)
                {
                    stateMachine.ChangeState(driveSM.StateDrive);
                    return;
                }
            }
        }
        else
        {
            interactableSM.InteractableMg.SetIsInteractable(true);
        }        
    }    

    public override void StateUpdate()
    {
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
        interactableSM.InteractableMg.SetIsInteractable(true);
        mountableSM.MountableMg.OnChangeMounted -= MountableMg_OnChangeMounted;
    }

    private void MountableMg_OnChangeMounted(object sender, System.EventArgs e)
    {
        if (mountableSM.MountableMg.GetIsMounted())
        {
            interactableSM.InteractableMg.SetIsInteractable(false);
            if (mountableSM.MountableMg.GetMounterMountAbilityMg().GetStateMachineTransform().TryGetComponent<IBasicSM>(out IBasicSM basicSM))
            {
                if (basicSM.BaseDataMg.GetObjectName() == ObjectNameEnum.Botan)
                {
                    stateMachine.ChangeState(driveSM.StateDrive);
                    return;
                }
            }
        }
        else
        {
            interactableSM.InteractableMg.SetIsInteractable(true);
        }       
    }
}
