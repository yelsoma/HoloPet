using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Released_HoloMem : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private MountingAbilityMod mountingAbilityMod;
    private IInteractAbilitySM interactAbilitySM;
    private IHoloMemFXSM holoMemFXSM;
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

        interactAbilitySM = GetComponentInParent<IInteractAbilitySM>();
        if (interactAbilitySM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no interactAbilitySM found in parent.");
        }

        holoMemFXSM = GetComponentInParent<IHoloMemFXSM>();
        if (holoMemFXSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no holoMemFXSM found in parent.");
        }

        
    }

    public override void Enter()
    {
    }

    public override void StateUpdate()
    {
        //raycast down
        if (interactAbilitySM.InteractAbilityMg.GetIsTargetLocked())
        {
            InteractAbilityManager myInteractMg = interactAbilitySM.InteractAbilityMg;
            InteractableManager targetInteractMg = myInteractMg.GetTargetInteractableMg();
            if (interactAbilitySM.InteractAbilityMg.CheckIsTargetHit(Vector2.down, 1f))
            {
                if (targetInteractMg.GetIsInteractable())
                {
                    holoMemFXSM.HoloMemFXMg.StartHeartPartical();
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
