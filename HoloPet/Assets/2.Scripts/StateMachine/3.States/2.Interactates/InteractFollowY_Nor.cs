using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractFollowY_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private InteractAbilityMod interactAbilityMod;
    private HoloMemFXMod holoMemFXMod;
    [SerializeField] private float interactDistance;
    private InteractAbilityManager myInteractMg;
    private InteractableManager targetInteractMg;
    private bool targetIsRight;
    private bool targetIsFarX;
    private bool targetIsFarY;

    // jump
    [SerializeField] private float jumpUpPower;
    private bool keepJump;

    #region AutoSetRef
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
        if(iHoloMemFXMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no holoMemFXMod found in parent.");
        }
        else
        {
            holoMemFXMod = iHoloMemFXMod.HoloMemFXMod;
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        myInteractMg = interactAbilityMod.InteractAbilityMg;
        targetInteractMg = myInteractMg.GetTargetInteractableMg();
        keepJump = true;
        basicMod.PhysicsMg.SetJump(jumpUpPower);
        basicMod.PhysicsMg.ResetFall();
    }
    public override void StateUpdate()
    {
        targetIsRight = myInteractMg.GetIsTargetRight();
        targetIsFarX = myInteractMg.GetIsTargetFarX(interactDistance);
        targetIsFarY = myInteractMg.GetIsTargetFarY(interactDistance);
        if (targetIsFarX)
        {
            keepJump = false;
        }
        else
        {
            if (targetIsFarY)
            {
                keepJump = true;
                if (targetIsRight)
                {
                    basicMod.FaceDirectionMg.SetFaceRight();
                }
                else
                {
                    basicMod.FaceDirectionMg.SetFaceLeft();
                }
            }
            else
            {
                GoToChoosenInteract();
            }
        }
        if (basicMod.PhysicsMg.KeepJump())
        {
            if (basicMod.BoundaryMg.CheckIsTopBounderyAndResetPos())
            {
                basicMod.PhysicsMg.SetJump(0);
            }
        }
        else
        {
            basicMod.PhysicsMg.KeepFall();
            if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                if (keepJump)
                {
                    basicMod.PhysicsMg.SetJump(jumpUpPower);
                    basicMod.PhysicsMg.ResetFall();
                }
                else
                {
                    stateMachine.ChangeState(interactAbilityMod.StateInteractFollowX);
                }
            }
        }
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
    }
    #endregion

    private void GoToChoosenInteract()
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
        }
        else
        {
            //exit to idle
            stateMachine.ChangeState(interactAbilityMod.StateInteractFailed);
        }
    }
}
