using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyChatted_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractableSM interactableSM;
    [SerializeField] private float jumpUpPower;
    [SerializeField] private int jumpCount;
    private float jumpCountLeft;
    [SerializeField] private float startJumpDelay;
    private Coroutine jumpCoroutine;
    private bool exitToIdle;

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

        interactableSM = GetComponentInParent<IInteractableSM>();
        if (interactableSM == null)
        {
            Debug.LogError($"{transform} ¡X no IInteractableSM found in parent.");
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        interactableSM.InteractableMg.GetInteracterManager().OnExitInteracting += Interacter_OnExitInteract;
        if (interactableSM.InteractableMg.GetInteracterManager() != null)
        {
            if (interactableSM.InteractableMg.GetIsInteracterRight())
            {
                basicSM.FaceDirectionMg.SetFaceRight();
            }
            else
            {
                basicSM.FaceDirectionMg.SetFaceLeft();
            }
            //start jump
            jumpCoroutine = StartCoroutine(CoStartJump());
            exitToIdle = false;
        }
        else
        {
            // exit to idle
            exitToIdle = true;
        }
    }
    public override void StateUpdate()
    {
        if (exitToIdle)
        {
            if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                stateMachine.ChangeState(basicSM.StateIdle);
                return;
            }
            else
            {
                stateMachine.ChangeState(basicSM.StateInAir);
                return;
            }
        }
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
        StopCoroutine(jumpCoroutine);
        interactableSM.InteractableMg.GetInteracterManager().OnExitInteracting -= Interacter_OnExitInteract;
        interactableSM.InteractableMg.ExitInteractedEvent();
    }
    #endregion

    private void Interacter_OnExitInteract(object sender, System.EventArgs e)
    {
        stateMachine.ChangeState(basicSM.StateInAir);
    }

    //corutine
    private IEnumerator CoStartJump()
    {
        yield return new WaitForSeconds(startJumpDelay);
        jumpCountLeft = jumpCount;
        basicSM.MovementMg.SetJump(jumpUpPower);
        basicSM.MovementMg.ResetFall();
        TriggerAni1();// happy ani
        while (jumpCountLeft > 0)
        {
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
                    jumpCountLeft--;
                    basicSM.MovementMg.SetJump(jumpUpPower);
                    basicSM.MovementMg.ResetFall();
                }
            }
            yield return null;
        }
        stateMachine.ChangeState(basicSM.StateIdle);
    }
}
