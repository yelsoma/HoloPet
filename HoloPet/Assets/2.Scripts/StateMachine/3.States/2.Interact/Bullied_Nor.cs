using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullied_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractableSM interactableSM;
    private InteractableManager myInteractableMg;
    private InteractAbilityManager interacterMg;
    private bool isKnockUp;
    private bool isPanicRun;

    //ani
    public event EventHandler OnHit;
    public event EventHandler OnPanic;

    //KnockBack
    private float knockUpPower = 6f;
    private float knockUpPowerNow;
    private float knockBackPower = 2.5f;
    private float fallSpeedNow;
    private float knockUpDecrese = 14f;
    private float fallSpeedIncreese = 6.5f;
    private float fallSpeedMax = 9f;
    private bool knockUpRight;

    //PanicRun
    [SerializeField] private float panicRunSpeed;
    [SerializeField] private float panicTime;
    private float panicTimeNow;
    private bool panicRight;
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
        myInteractableMg = interactableSM.InteractableMg;
        interacterMg = interactableSM.InteractableMg.GetInteracterManager();
        if (interacterMg != null)
        {
            if (myInteractableMg.GetIsInteracterRight())
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
            // exit to idle
            stateMachine.ChangeState(basicSM.StateInAir);
            return;
        }
        interacterMg.OnTriggerInteracting += TriggerInteracting;
        interacterMg.OnExitInteracting += ExitInteracting;
        isKnockUp = false;
        isPanicRun = false;
}
    public override void StateUpdate()
    {
        if (isKnockUp && !isPanicRun)
        {
            OnHit?.Invoke(this, EventArgs.Empty);
            KnockBack();
        }
        if (isPanicRun)
        {
            OnPanic?.Invoke(this, EventArgs.Empty);
            PanicRun();
        }
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {        
        interacterMg.OnTriggerInteracting -= TriggerInteracting;
        interacterMg.OnExitInteracting -= ExitInteracting;
        myInteractableMg.ExitInteractedEvent();
        interacterMg = null;
    }
    #endregion
    private void ExitInteracting(object sender, System.EventArgs e)
    {
        if (isKnockUp == false)
        {
            stateMachine.ChangeState(basicSM.StateInAir);
        }       
    }

    private void TriggerInteracting(object sender, System.EventArgs e)
    {
        isKnockUp = true;
        knockUpPowerNow = knockUpPower;
        fallSpeedNow = 0f;
        ChooseKnockUpSide();
    }

    private void KnockBack()
    {
        if (knockUpPowerNow >= 0f)
        {
            knockUpPowerNow -= knockUpDecrese * Time.deltaTime;

            if (basicSM.BoundaryMg.CheckIsTopBounderyAndResetPos())
            {
                knockUpPowerNow = -1f;
            }

            basicSM.MovementMg.MoveUp(knockUpPowerNow);
        }
        else
        {
            if (fallSpeedNow <= fallSpeedMax)
            {
                fallSpeedNow += fallSpeedIncreese * Time.deltaTime;
            }

            basicSM.MovementMg.MoveDown(fallSpeedNow);

            if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                isPanicRun = true;
                StartPanicRun();
            }
        }

        if (knockUpRight)
        {
            if (basicSM.BoundaryMg.CheckIsRightBounderyAndResetPos())
            {
                knockUpRight = false;
            }

            basicSM.MovementMg.MoveRight(knockBackPower);
        }
        else
        {
            if (basicSM.BoundaryMg.CheckIsLeftBounderyAndResetPos())
            {
                knockUpRight = true;
            }

            basicSM.MovementMg.MoveLeft(knockBackPower);
        }
    }
    private void ChooseKnockUpSide()
    {
        if (myInteractableMg.GetIsInteracterRight())
        {
            knockUpRight = false;
            basicSM.FaceDirectionMg.SetFaceRight();
        }
        else
        {
            knockUpRight = true;
            basicSM.FaceDirectionMg.SetFaceLeft();
        }
    }
    private void StartPanicRun()
    {
        panicTimeNow = panicTime;
        if (myInteractableMg.GetIsInteracterRight())
        {
            panicRight = false;
            basicSM.FaceDirectionMg.SetFaceLeft();
        }
        else
        {
            panicRight = true;
            basicSM.FaceDirectionMg.SetFaceRight();
        }
    }
    private void PanicRun()
    {
        if(panicTimeNow >= 0)
        {
            panicTimeNow = panicTimeNow - Time.deltaTime;
            if (panicRight)
            {
                basicSM.MovementMg.MoveRight(panicRunSpeed);
                if (basicSM.BoundaryMg.CheckIsRightBounderyAndResetPos())
                {
                    basicSM.FaceDirectionMg.SetFaceLeft();
                    panicRight = false;
                }
            }
            else
            {
                basicSM.MovementMg.MoveLeft(panicRunSpeed);
                if (basicSM.BoundaryMg.CheckIsLeftBounderyAndResetPos())
                {
                    basicSM.FaceDirectionMg.SetFaceRight();
                    panicRight = true;
                }               
            }
        }
        else
        {
            stateMachine.ChangeState(basicSM.StateIdle);
        }
    }
}
