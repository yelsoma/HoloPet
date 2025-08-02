using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractFollowX_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractAbilitySM interactAbilitySM; 
    [SerializeField] private float interactDistance;
    [SerializeField] private float followTargetSpeed;
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
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        myInteractMg = interactAbilitySM.InteractAbilityMg;
        targetInteractMg = myInteractMg.GetTargetIInteractable();
    }
    public override void StateUpdate()
    {
        // if target is no longer Interactable  , exit to idle
        if (!interactAbilitySM.InteractAbilityMg.GetTargetIInteractable().GetIsInteractable())
        {
            //exit to idle
            stateMachine.ChangeState(basicSM.StateInAir);
            return;
        }
        targetIsRight = myInteractMg.GetIsTargetRight();
        targetIsFarX = myInteractMg.GetIsTargetFarX(interactDistance);
        targetIsFarY = myInteractMg.GetIsTargetFarY(interactDistance);
        // if x is far and right 
        if (targetIsFarX == true && targetIsRight == true)
        {
            basicSM.FaceDirectionMg.SetFaceRight();
            basicSM.MovementMg.MoveRight(followTargetSpeed);
            if (!myInteractMg.GetIsTargetFarX(interactDistance))
            {
                if (myInteractMg.GetIsTargetFarY(interactDistance))
                {
                    stateMachine.ChangeState(interactAbilitySM.StateInteractFollowY);
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
            basicSM.FaceDirectionMg.SetFaceLeft();
            basicSM.MovementMg.MoveLeft(followTargetSpeed);
            if (!myInteractMg.GetIsTargetFarX(interactDistance))
            {
                if (myInteractMg.GetIsTargetFarY(interactDistance))
                {
                    stateMachine.ChangeState(interactAbilitySM.StateInteractFollowY);
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
            stateMachine.ChangeState(interactAbilitySM.StateInteractFollowY);
        }
        //if X is close but Y is close
        if(targetIsFarX == false && targetIsFarY == false)
        {
            if (targetIsRight)
            {
                basicSM.FaceDirectionMg.SetFaceLeft();
                basicSM.MovementMg.MoveLeft(followTargetSpeed);
                if (myInteractMg.GetIsTargetFarX(interactDistance))
                {
                    // distance is ok exit to interact
                    GoToChoosenInteract();
                    return;
                }
            }
            else
            {
                basicSM.FaceDirectionMg.SetFaceRight();
                basicSM.MovementMg.MoveRight(followTargetSpeed);
                if (myInteractMg.GetIsTargetFarX(interactDistance))
                {
                    // distance is ok exit to interact
                    GoToChoosenInteract();
                    return;
                }
            }
        }       
        if (basicSM.BoundaryMg.CheckIsLeftBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicSM.StateInAir);
        }
        if (basicSM.BoundaryMg.CheckIsRightBounderyAndResetPos())
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
    #endregion

    private void GoToChoosenInteract()
    {
        targetInteractMg.SetInteracter(myInteractMg);
        targetInteractMg.GoToChoosenInteracedState();
        stateMachine.ChangeState(myInteractMg.GetBothInteractOption().GetInteracterOption().GetOptionState);
    }    
}
