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
    [SerializeField] private float checkDistanceDown;

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
        if (mountingAbilitySM.MountingAbilityMg.TrySetMountWithRaycast(Vector2.down, checkDistanceDown))
        {
            stateMachine.ChangeState(mountingAbilitySM.StateMounting);
            return;
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
