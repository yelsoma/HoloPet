using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Released_HoloMem : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private MountingAbilityMod mountingAbilityMod;
    private InteractAbilityMod interactAbilityMod;
    private FXMod fXMod;
    [SerializeField] private float checkDistanceUp;
    private ObjectGangEnum excludeGang;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");

        IBasicMod iBasicMod = stateMachine.transform.GetComponent<IBasicMod>();
        if (iBasicMod == null)
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        else
            basicMod = iBasicMod.BasicMod;

        IMountingAbilityMod iMountingAbilityMod = stateMachine.transform.GetComponent<IMountingAbilityMod>();
        if (iMountingAbilityMod == null)
            Debug.LogError($"{transform.root.name} ¡X no imountingAbilityMod found in parent.");
        else
            mountingAbilityMod = iMountingAbilityMod.MountingAbilityMod;

        IInteractAbilityMod iInteractAbilityMod = stateMachine.transform.GetComponent<IInteractAbilityMod>();
        if (iInteractAbilityMod == null)
            Debug.LogError($"{transform.root.name} ¡X no iInteractAbilityMod found in parent.");
        else
            interactAbilityMod = iInteractAbilityMod.InteractAbilityMod;

        IFXMod iFXMod = stateMachine.transform.GetComponent<IFXMod>();
        if (iFXMod == null)
            Debug.LogError($"{transform.root.name} ¡X no holoMemFXMod found in parent.");
        else
            fXMod = iFXMod.FXMod;
    }

    public override void Enter()
    {
        if (basicMod.ObjectDefinition.ObjectGangEnum == ObjectGangEnum.Enemy)
        {
            excludeGang = ObjectGangEnum.Player;
        }
        else
        {
            excludeGang = ObjectGangEnum.Enemy;
        }
        //raycast down
        if (interactAbilityMod.InteractAbilityMg.GetIsTargetLocked())
        {
            InteractAbilityManager myInteractMg = interactAbilityMod.InteractAbilityMg;
            InteractableManager targetInteractMg = myInteractMg.GetTargetInteractableMg();
            if (interactAbilityMod.InteractAbilityMg.CheckIsTargetHit(Vector2.down, 1f))
            {
                if (targetInteractMg.GetIsInteractable())
                {
                    fXMod.HeartFX.StartHeartPartical();
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
        if (mountingAbilityMod.MountingAbilityMg.TrySetMountWithRaycast(Vector2.up, checkDistanceUp, excludeGang))
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

    public override void StateUpdate()
    {       
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
