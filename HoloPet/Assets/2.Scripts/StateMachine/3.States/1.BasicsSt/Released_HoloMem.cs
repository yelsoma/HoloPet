using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Released_HoloMem : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IMountingAbilitySM mountingAbilitySM;
    private IAttackableSM attackableSM;
    private IInteractAbilitySM interactAbilitySM;
    private IHoloMemFXSM holoMemFXSM;

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

        mountingAbilitySM = GetComponentInParent<IMountingAbilitySM>();
        if (mountingAbilitySM == null)
        {
            Debug.LogError($"{transform} ¡X no mountingAbilitySM found in parent.");
        }

        attackableSM = GetComponentInParent<IAttackableSM>();
        if (attackableSM == null)
        {
            Debug.LogError($"{transform} ¡X no attackableSM found in parent.");
        }

        interactAbilitySM = GetComponentInParent<IInteractAbilitySM>();
        if (interactAbilitySM == null)
        {
            Debug.LogError($"{transform} ¡X no interactAbilitySM found in parent.");
        }

        holoMemFXSM = GetComponentInParent<IHoloMemFXSM>();
        if (holoMemFXSM == null)
        {
            Debug.LogError($"{transform} ¡X no holoMemFXSM found in parent.");
        }
    }

    public override void Enter()
    {
    }

    public override void StateUpdate()
    {
        if (attackableSM.AttackableMg.GetHp() == 0)
        {
            stateMachine.ChangeState(basicSM.StateInAir);
            return;
        }       
        //raycast down
        if (basicSM.RaycastMg.TrySetRaycast(1f, Vector2.down))
        {
            //check is there lock interact
            if (interactAbilitySM.InteractAbilityMg.GetIsTargetLocked())
            {
                InteractAbilityManager myInteractMg = interactAbilitySM.InteractAbilityMg;
                InteractableManager targetInteractMg = myInteractMg.GetTargetInteractableMg();
                if (basicSM.RaycastMg.CheckIsListMatched(targetInteractMg.GetStateMachineTransform()))
                {
                    if (targetInteractMg.GetIsInteractable())
                    {
                        holoMemFXSM.HoloMemFXMg.StartHeartPartical();
                        myInteractMg.SetIsTargetLocked(false);
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
            //check is there mount
            if (mountingAbilitySM.MountingAbilityMg.TrySetMountWithRaycast(basicSM.RaycastMg.GetRaycastHits()))
            {
                basicSM.RaycastMg.ClearHits();
                stateMachine.ChangeState(mountingAbilitySM.StateMounting);
                return;
            }
            basicSM.RaycastMg.ClearHits();
        }
        if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicSM.StateIdle);
        }
        else
        {
            stateMachine.ChangeState(basicSM.StateInAir);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
