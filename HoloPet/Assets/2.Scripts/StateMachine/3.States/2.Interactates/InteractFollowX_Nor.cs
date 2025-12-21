using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractFollowX_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private InteractAbilityMod interactAbilityMod;
    private FXMod fXMod;
    [SerializeField] private float interactDistance;
    private float followSpeed = 2f;
    private InteractAbilityManager myInteractMg;
    private InteractableManager targetInteractMg;
    private bool targetIsRight;
    private bool targetIsFarX;
    private bool targetIsFarY;
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

        IFXMod iHoloMemFXMod = GetComponentInParent<IFXMod>();
        if (iHoloMemFXMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no holoMemFXMod found in parent.");
        }
        else
        {
            fXMod = iHoloMemFXMod.FXMod;
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        myInteractMg = interactAbilityMod.InteractAbilityMg;
        targetInteractMg = myInteractMg.GetTargetInteractableMg();
    }
    public override void StateUpdate()
    {
        targetIsRight = myInteractMg.GetIsTargetRight();
        targetIsFarX = myInteractMg.GetIsTargetFarX(interactDistance);
        targetIsFarY = myInteractMg.GetIsTargetFarY(interactDistance);
        // if x is far and right 
        if (targetIsFarX == true && targetIsRight == true)
        {
            basicMod.FaceDirectionMg.SetFaceRight();
            basicMod.PhysicsMg.MoveRightMultiply(followSpeed);
            if (!myInteractMg.GetIsTargetFarX(interactDistance))
            {
                if (myInteractMg.GetIsTargetFarY(interactDistance))
                {
                    stateMachine.ChangeState(interactAbilityMod.StateInteractFollowY);
                }
                else
                {
                    // X Y is ok exit to interact
                    GoToChoosenInteract();
                    return;
                }
            }
        }
        // if x is far and left 
        if (targetIsFarX == true && targetIsRight == false)
        {
            basicMod.FaceDirectionMg.SetFaceLeft();
            basicMod.PhysicsMg.MoveLeftMultiply(followSpeed);
            if (!myInteractMg.GetIsTargetFarX(interactDistance))
            {
                if (myInteractMg.GetIsTargetFarY(interactDistance))
                {
                    stateMachine.ChangeState(interactAbilityMod.StateInteractFollowY);
                }
                else
                {
                    // X Y is ok exit to interact
                    GoToChoosenInteract();
                    return;
                }
            }
        }
        // if X is close but Y is far
        if(targetIsFarX == false && targetIsFarY == true)
        {
            stateMachine.ChangeState(interactAbilityMod.StateInteractFollowY);
        }
        //if X is close but Y is close
        if(targetIsFarX == false && targetIsFarY == false)
        {
            if (targetIsRight)
            {
                basicMod.FaceDirectionMg.SetFaceLeft();
                basicMod.PhysicsMg.MoveLeftMultiply(followSpeed);
                if (myInteractMg.GetIsTargetFarX(interactDistance))
                {
                    // distance is ok exit to interact
                    GoToChoosenInteract();
                    return;
                }
            }
            else
            {
                basicMod.FaceDirectionMg.SetFaceRight();
                basicMod.PhysicsMg.MoveRightMultiply(followSpeed);
                if (myInteractMg.GetIsTargetFarX(interactDistance))
                {
                    // distance is ok exit to interact
                    GoToChoosenInteract();
                    return;
                }
            }
        }
        if (basicMod.BoundaryMg.CheckIsLeftBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicMod.StateInAir);
        }
        if (basicMod.BoundaryMg.CheckIsRightBounderyAndResetPos())
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
    #endregion

    private void GoToChoosenInteract()
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
        }
        else
        {
            //exit to idle
            stateMachine.ChangeState(interactAbilityMod.StateInteractFailed);
        }
    }    
}
