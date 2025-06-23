using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracterState_FindInteracts : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractAbilitySM interactAbilitySM;
    private bool StartFollowTarget;

    
    [SerializeField] private float interactDistance;
    [SerializeField] private float followTargetSpeed;
    private bool targetIsRight;
    private bool targetIsFar;
    private bool goRightCloseDistance;
    private bool goLeftCloseDistance;
    private bool goLeftMakeDistance;
    private bool goRightMakeDistance;   

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
        #region Check is there target
        StartFollowTarget = false;
        if (!basicSM.RaycastMg.TrySetRaycastBothSide(10))
        {
            //no Raycas hit
            stateMachine.ChangeState(basicSM.StateIdle);
            return;
        }
        if (!interactAbilitySM.InteractAbilityMg .TrySetTargetWihtRaycastHits(basicSM.RaycastMg.GetRaycastHits()))
        {
            //no Interactable
            stateMachine.ChangeState(basicSM.StateIdle);
            return;
        }
        if (!interactAbilitySM.InteractAbilityMg.TryMatchOptionsChooseWithBothChance())
        {
            //no Interact option match
            stateMachine.ChangeState(basicSM.StateIdle);
            return;
        }
        StartFollowTarget = true;
        #endregion

        #region Follow
        targetIsRight = interactAbilitySM.InteractAbilityMg.GetIsTargetRight();
        targetIsFar = interactAbilitySM.InteractAbilityMg.GetIsTargetFar(interactDistance);
        goRightCloseDistance = false;
        goLeftCloseDistance = false;
        goLeftMakeDistance = false;
        goRightMakeDistance = false;

        // if target is far and right , go right to close distance
        if (targetIsFar == true && targetIsRight == true)
        {
            basicSM.FaceDirectionMg.SetFaceRight();
            goRightCloseDistance = true;
        }
        // if target is far and left , go left to close distance
        if (targetIsFar == true && targetIsRight == false)
        {
            basicSM.FaceDirectionMg.SetFaceLeft();
            goLeftCloseDistance = true;
        }
        // if target is too close and right , go left to make distance
        if (targetIsFar == false && targetIsRight == true)
        {
            basicSM.FaceDirectionMg.SetFaceLeft();
            goLeftMakeDistance = true;
        }
        // if target is too close and left , go right to make distance
        if (targetIsFar == false && targetIsRight == false)
        {
            basicSM.FaceDirectionMg.SetFaceRight();
            goRightMakeDistance = true;
        }
        #endregion
    }
    public override void StateUpdate()
    {
        if (StartFollowTarget)
        {
            // if target is no longer Interactable  , exit to idle
            if (!interactAbilitySM.InteractAbilityMg.GetTargetIInteractable().GetIsInteractable())
            {
                //exit to idle
                stateMachine.ChangeState(basicSM.StateIdle);
                return;
            }
        }
        if (goRightCloseDistance)
        {
            if (interactAbilitySM.InteractAbilityMg.GetIsTargetFar(interactDistance))
            {
                basicSM.MovementMg.MoveRight(followTargetSpeed);
            }
            else
            {
                // distance is ok exit to interact
                GoToChoosenInteract();
                return;
            }
        }
        if (goLeftCloseDistance)
        {
            if (interactAbilitySM.InteractAbilityMg.GetIsTargetFar(interactDistance))
            {
                basicSM.MovementMg.MoveLeft(followTargetSpeed);
            }
            else
            {
                // distance is ok exit to interact
                GoToChoosenInteract();
                return;
            }
        }
        if (goLeftMakeDistance)
        {
            if (!interactAbilitySM.InteractAbilityMg.GetIsTargetFar(interactDistance))
            {
                basicSM.MovementMg.MoveLeft(followTargetSpeed);
            }
            else
            {
                // distance is ok exit to interact
                GoToChoosenInteract();
                return;
            }
        }
        if (goRightMakeDistance)
        {
            if (!interactAbilitySM.InteractAbilityMg.GetIsTargetFar(interactDistance))
            {
                basicSM.MovementMg.MoveRight(followTargetSpeed);
            }
            else
            {
                // distance is ok exit to interact
                GoToChoosenInteract();
                return;
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
        interactAbilitySM.InteractAbilityMg.GetTargetIInteractable().SetInteracter(interactAbilitySM.InteractAbilityMg);
        interactAbilitySM.InteractAbilityMg.GetTargetIInteractable().GoToChoosenInteracedState();
        stateMachine.ChangeState(interactAbilitySM.InteractAbilityMg.GetBothInteractOption().GetInteracterOption().GetOptionState);
    }
}
