using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Cart : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private MountableMod mountableMod;
    private IDriveSM driveSM;
    private InteractableMod interactableMod;

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

        IMountableMod imountableMod = GetComponentInParent<IMountableMod>();
        if (imountableMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no mountableMod  found in parent.");
        }
        else
        {
            mountableMod = imountableMod.MountableMod;
        }

        driveSM = GetComponentInParent<IDriveSM>();
        if (driveSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no IDriveSM found in parent.");
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
    }

    public override void Enter()
    {
        mountableMod.MountableMg.OnChangeMounted += MountableMg_OnChangeMounted;
        if (mountableMod.MountableMg.GetIsMounted())
        {
            interactableMod.InteractableMg.SetIsInteractable(false);
            if (mountableMod.MountableMg.GetMounterMountAbilityMg().GetStateMachineTransform().TryGetComponent<IBasicMod>(out IBasicMod ibasicSM))
            {
                if (ibasicSM.BasicMod.BaseDataMg.GetObjectName() == ObjectNameEnum.Botan)
                {
                    stateMachine.ChangeState(driveSM.StateDrive);
                    return;
                }
            }
        }
        else
        {
            interactableMod.InteractableMg.SetIsInteractable(true);
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
        interactableMod.InteractableMg.SetIsInteractable(true);
        mountableMod.MountableMg.OnChangeMounted -= MountableMg_OnChangeMounted;
    }

    private void MountableMg_OnChangeMounted(object sender, System.EventArgs e)
    {
        if (mountableMod.MountableMg.GetIsMounted())
        {
            interactableMod.InteractableMg.SetIsInteractable(false);
            if (mountableMod.MountableMg.GetMounterMountAbilityMg().GetStateMachineTransform().TryGetComponent<IBasicMod>(out IBasicMod ibasicSM))
            {
                if (ibasicSM.BasicMod.BaseDataMg.GetObjectName() == ObjectNameEnum.Botan)
                {
                    stateMachine.ChangeState(driveSM.StateDrive);
                    return;
                }
            }
        }
        else
        {
            interactableMod.InteractableMg.SetIsInteractable(true);
        }       
    }
}
