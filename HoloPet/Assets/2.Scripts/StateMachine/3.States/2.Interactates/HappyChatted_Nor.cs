using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyChatted_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
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

        interactableSM = GetComponentInParent<IInteractableSM>();
        if (interactableSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no IInteractableSM found in parent.");
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
                basicMod.FaceDirectionMg.SetFaceRight();
            }
            else
            {
                basicMod.FaceDirectionMg.SetFaceLeft();
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
            if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                stateMachine.ChangeState(basicMod.StateIdle);
                return;
            }
            else
            {
                stateMachine.ChangeState(basicMod.StateInAir);
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
        stateMachine.ChangeState(basicMod.StateInAir);
    }

    //corutine
    private IEnumerator CoStartJump()
    {
        yield return new WaitForSeconds(startJumpDelay);
        jumpCountLeft = jumpCount;
        basicMod.PhysicsMg.SetJump(jumpUpPower);
        basicMod.PhysicsMg.ResetFall();
        TriggerAni1();// happy ani
        while (jumpCountLeft > 0)
        {
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
                    jumpCountLeft--;
                    basicMod.PhysicsMg.SetJump(jumpUpPower);
                    basicMod.PhysicsMg.ResetFall();
                }
            }
            yield return null;
        }
        stateMachine.ChangeState(basicMod.StateIdle);
    }
}
