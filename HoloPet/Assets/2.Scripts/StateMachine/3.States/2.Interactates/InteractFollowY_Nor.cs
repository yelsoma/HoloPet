using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractFollowY_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractAbilitySM interactAbilitySM;
    private IHoloMemFXSM holoMemFXSM;
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
            Debug.LogError($"{transform} ¡X no StateMachineBase found in parent.");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform} ¡X no basicSM found in parent.");
        }

        interactAbilitySM = GetComponentInParent<IInteractAbilitySM>();
        if (interactAbilitySM == null)
        {
            Debug.LogError($"{transform} ¡X no IInteractAbilitySM found in parent.");
        }

        holoMemFXSM = GetComponentInParent<IHoloMemFXSM>();
        if (holoMemFXSM == null)
        {
            Debug.LogError($"{transform} ¡X no holoMemFXSM found in parent.");
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        myInteractMg = interactAbilitySM.InteractAbilityMg;
        targetInteractMg = myInteractMg.GetTargetInteractableMg();
        keepJump = true;
        basicSM.MovementMg.SetJump(jumpUpPower);
        basicSM.MovementMg.ResetFall();
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
                    basicSM.FaceDirectionMg.SetFaceRight();
                }
                else
                {
                    basicSM.FaceDirectionMg.SetFaceLeft();
                }
            }
            else
            {
                GoToChoosenInteract();
            }
        }
        if (basicSM.MovementMg.KeepJump())
        {
            if (basicSM.BoundaryMg.CheckIsTopBounderyAndResetPos())
            {
                basicSM.MovementMg.SetJump(0);
            }
        }
        else
        {
            basicSM.MovementMg.KeepFall();
            if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                if (keepJump)
                {
                    basicSM.MovementMg.SetJump(jumpUpPower);
                    basicSM.MovementMg.ResetFall();
                }
                else
                {
                    stateMachine.ChangeState(interactAbilitySM.StateInteractFollowX);
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
            holoMemFXSM.HoloMemFXMg.StartHeartPartical();
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
            stateMachine.ChangeState(interactAbilitySM.StateInteractFailed);
        }
    }
}
