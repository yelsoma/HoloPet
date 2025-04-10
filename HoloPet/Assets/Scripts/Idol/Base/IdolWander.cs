using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class IdolWander : StateBase
{
    [SerializeField] private float wanderMaxTime;
    [SerializeField] private float wanderMinTime;
    [SerializeField] private float wanderSpeed;
    [SerializeField] private IdolStateMachine stateMachine;
    private float wanderTimer;
    private float randomDir;
    private bool wanderRight;

  
    // < State Base >
    public override void Enter()
    {
        //event
        stateMachine.interactManager.OnInteract += InteractManager_OnInteract;               
        stateMachine.mouseInput.OnDrag += Input_OnDrag;
        stateMachine.mouseInput.OnClick += MouseInput_OnClick;

        //start
        wanderTimer = UnityEngine.Random.Range(wanderMinTime, wanderMaxTime);
        randomDir = UnityEngine.Random.Range(1f, 0f);
        if (randomDir >= 0.5)
        {
            wanderRight = true;
            stateMachine.data.SetIsFaceRight(true);
        }
        else
        {
            wanderRight = false;
            stateMachine.data.SetIsFaceRight(false);
        }
    }   

    public override void StateUpdate()
    {
        if (!stateMachine.boundaryManager.CheckIsBotBoundery())
        {
            //exit to fall
            stateMachine.ChangeState(stateMachine.stateFall);
            return;
        }
        if (stateMachine.boundaryManager.CheckIsLeftBoundery())
        {
            wanderRight = true;
            stateMachine.data.SetIsFaceRight(true);
        }
        if (stateMachine.boundaryManager.CheckIsRightBoundery())
        {

            wanderRight = false;
            stateMachine.data.SetIsFaceRight(false);
        }
        if (wanderTimer <= 0f)
        {
            //exit to idle
            stateMachine.ChangeState(stateMachine.stateChooseARandom);
            return;
        }
        // keep  wander right
        wanderTimer -= Time.deltaTime;
    }
    public override void StateLateUpdate()
    {
        if (wanderRight)
        {
            stateMachine.movement.MoveRight(wanderSpeed);
        }
        else
        {
            stateMachine.movement.MoveLeft(wanderSpeed);
        }
    }
    public override void Exit()
    {
        stateMachine.mouseInput.OnDrag -= Input_OnDrag;
        stateMachine.mouseInput.OnClick -= MouseInput_OnClick;
        stateMachine.interactManager.OnInteract -= InteractManager_OnInteract;
    }

    // < Events >
    private void Input_OnDrag(object sender, MouseInput.OnDragEventArgs e)
    {
        //exit to grab
        stateMachine.ChangeState(stateMachine.stateGrab);
        return;
    }
    private void MouseInput_OnClick(object sender, EventArgs e)
    {
        //exit to KnockUp
        stateMachine.ChangeState(stateMachine.stateKnockUp);
        return;
    }
    private void InteractManager_OnInteract(object sender, EventArgs e)
    {
        //exit to interacted
        stateMachine.ChangeState(stateMachine.stateInteracted);
        return;
    }
}
