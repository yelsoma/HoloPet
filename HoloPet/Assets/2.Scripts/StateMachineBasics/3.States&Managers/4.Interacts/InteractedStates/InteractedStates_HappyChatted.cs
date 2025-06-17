using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractedStates_HappyChatted : StateBase
{
    public event EventHandler OnStartJumpAni;
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractableSM interactableSM;
    [SerializeField] private float jumpUpPower;
    [SerializeField] private float jumpUpDecrese;
    [SerializeField] private int jumpCount;
    private float jumpUpPowerNow;
    private float fallSpeedIncreese = 6.5f;
    private float fallSpeedMax = 9f;
    private float fallSpeedNow;
    private float jumpCountLeft;
    [SerializeField] private float startJumpDelay;
    private Coroutine jumpCoroutine;

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
        interactableSM.InteractableMg.GetInteracter().OnExitInteracting += Interacter_OnExitInteract;
        if (interactableSM.InteractableMg.GetInteracter() != null)
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
        }
        else
        {
            // exit to idle
            stateMachine.ChangeState(basicSM.StateIdle);
            return;
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
        StopCoroutine(jumpCoroutine);
        interactableSM.InteractableMg.GetInteracter().OnExitInteracting -= Interacter_OnExitInteract;
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
        jumpUpPowerNow = jumpUpPower;
        fallSpeedNow = 0f;
        OnStartJumpAni?.Invoke(this, EventArgs.Empty);
        while (jumpCountLeft > 0)
        {
            if (jumpUpPowerNow > 0)
            {
                basicSM.MovementMg.MoveUp(jumpUpPowerNow);
                jumpUpPowerNow -= jumpUpDecrese * Time.deltaTime;
            }
            else
            {
                basicSM.MovementMg.MoveDown(fallSpeedNow);
                if (fallSpeedNow <= fallSpeedMax)
                {
                    fallSpeedNow += fallSpeedIncreese * Time.deltaTime;
                }
                if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
                {
                    jumpCountLeft--;
                    jumpUpPowerNow = jumpUpPower;
                    fallSpeedNow = 0f;
                }
            }
            yield return null;
        }
        stateMachine.ChangeState(basicSM.StateIdle);
    }
}
