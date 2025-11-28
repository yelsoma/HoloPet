using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Released_HoloMem : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private MountingAbilityMod mountingAbilityMod;
    private InteractAbilityMod interactAbilityMod;
    private HoloMemFXMod holoMemFXMod;
    [SerializeField] private float checkDistanceDown;

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

        IInteractAbilityMod iInteractAbilityMod = GetComponentInParent<IInteractAbilityMod>();
        if (iInteractAbilityMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no iInteractAbilityMod found in parent.");
        }
        else
        {
            interactAbilityMod = iInteractAbilityMod.InteractAbilityMod;
        }

        IHoloMemFXMod iHoloMemFXMod = GetComponentInParent<IHoloMemFXMod>();
        if (iHoloMemFXMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no holoMemFXMod found in parent.");
        }
        else
        {
            holoMemFXMod = iHoloMemFXMod.HoloMemFXMod;
        }


    }

    public override void Enter()
    {
    }

    public override void StateUpdate()
    {
        //raycast down
        if (interactAbilityMod.InteractAbilityMg.GetIsTargetLocked())
        {
            InteractAbilityManager myInteractMg = interactAbilityMod.InteractAbilityMg;
            InteractableManager targetInteractMg = myInteractMg.GetTargetInteractableMg();
            if (interactAbilityMod.InteractAbilityMg.CheckIsTargetHit(Vector2.down, 1f))
            {
                if (targetInteractMg.GetIsInteractable())
                {
                    holoMemFXMod.HoloMemFX.StartHeartPartical();
                    myInteractMg.SetTargetLocked(false);
                    targetInteractMg.SetInteracter(myInteractMg);
                    targetInteractMg.GoToChoosenInteracedState();
                    if (myInteractMg.GetBothInteractOption().GetInteracterOption().GetOptionState != null)
                    {
                        stateMachine.ChangeState(myInteractMg.GetBothInteractOption().GetInteracterOption().GetOptionState);
                    }
                    return;
                }               
            }
        }
        if (mountingAbilityMod.MountingAbilityMg.TrySetMountWithRaycast(Vector2.down, checkDistanceDown))
        {
            stateMachine.ChangeState(mountingAbilityMod.StateMounting);
            return;
        }
        if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicMod.StateIdle);
        }
        else
        {
            stateMachine.ChangeState(basicMod.StateInAir);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
