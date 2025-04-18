using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemState_FollowTarget : StateBase
{
    [SerializeField] private HoloMemStateMachine stateMachine;
    [SerializeField] private float followSpeed;
    [SerializeField] private float interactDistance;

    private bool targetIsRight;
    private bool targetIsFar;
    private bool goRightCloseDistance;
    private bool goLeftCloseDistance;
    private bool goLeftMakeDistance;
    private bool goRightMakeDistance;
    public override void Enter()
    {
        //can do

        //event
        stateMachine.mouseInput.OnDrag += MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;
        stateMachine.interactManager.OnInteractedByTarget += InteractManager_OnInteractedByTarget;

        //start
        
        //get target is right or left , is far or too close
        targetIsRight = stateMachine.interactManager.GetIsTargetRight();
        targetIsFar = stateMachine.interactManager.GetIsTargetFar(interactDistance);
        goRightCloseDistance = false;
        goLeftCloseDistance = false;
        goLeftMakeDistance = false;
        goRightMakeDistance = false;

        // if target is far and right , go right to close distance
        if (targetIsFar == true && targetIsRight == true)
        {
            stateMachine.faceDirection.SetFaceRight();
            goRightCloseDistance = true;
        }
        // if target is far and left , go left to close distance
        if (targetIsFar == true && targetIsRight == false)
        {
            stateMachine.faceDirection.SetFaceLeft();
            goLeftCloseDistance = true;
        }
        // if target is too close and right , go left to make distance
        if (targetIsFar == false && targetIsRight == true)
        {
            stateMachine.faceDirection.SetFaceLeft();
            goLeftMakeDistance = true;
        }
        // if target is too close and left , go right to make distance
        if (targetIsFar == false && targetIsRight == false)
        {
            stateMachine.faceDirection.SetFaceRight();
            goRightMakeDistance = true;
        }
    }
    public override void StateUpdate()
    {
        // if target is no longer Interactable  , exit to idle
        if (!stateMachine.interactManager.GetTargetIInteractable().GetIsInteractable())
        {
            //exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }


        if (goRightCloseDistance)
        {
            if (stateMachine.interactManager.GetIsTargetFar(interactDistance))
            {
                stateMachine.movement.MoveRight(followSpeed);
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
            if (stateMachine.interactManager.GetIsTargetFar(interactDistance))
            {
                stateMachine.movement.MoveLeft(followSpeed);
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
            if (!stateMachine.interactManager.GetIsTargetFar(interactDistance))
            {
                stateMachine.movement.MoveLeft(followSpeed);
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
            if (!stateMachine.interactManager.GetIsTargetFar(interactDistance))
            {
                stateMachine.movement.MoveRight(followSpeed);
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
        stateMachine.mouseInput.OnDrag -= MouseInput_OnDrag;
        stateMachine.mouseInput.OnClick -= MouseInput_OnClick;
        stateMachine.interactManager.OnInteractedByTarget -= InteractManager_OnInteractedByTarget;
    }

    // < Events >
    private void MouseInput_OnDrag(object sender, MouseInputVr2.OnDragEventArgs e)
    {
        //exit to grab
        stateMachine.ChangeState(stateMachine.stateGrab);
        return;
    }
    private void MouseInput_OnClick(object sender, EventArgs e)
    {
        stateMachine.ChangeState(stateMachine.stateKnockUp);
        return;
    }
    private void InteractManager_OnInteractedByTarget(object sender, EventArgs e)
    {
        // Exit to interact
        stateMachine.ChangeState(stateMachine.stateInteracted);
        return;
    }

    // < Package Method >
    private void GoToChoosenInteract()
    {
        interactOption choosenInteractOp = stateMachine.interactManager.GetChoosenInteractOp();
        stateMachine.interactManager.GetTargetIInteractable().SetInteractedOp(choosenInteractOp);
        if(choosenInteractOp == interactOption.Bully)
        {
            stateMachine.interactManager.GetTargetIInteractable().IsInteractedByTarget();
            stateMachine.ChangeState(stateMachine.stateBully);
            return;
        }
        if (choosenInteractOp == interactOption.happyChat)
        {
            stateMachine.interactManager.GetTargetIInteractable().IsInteractedByTarget();
            stateMachine.ChangeState(stateMachine.stateHappyChat);
            return;
        }
        if (choosenInteractOp == interactOption.sit)
        {
            stateMachine.interactManager.GetTargetIInteractable().IsInteractedByTarget();
            stateMachine.ChangeState(stateMachine.stateIdle);
            return;
        }
    }
}
